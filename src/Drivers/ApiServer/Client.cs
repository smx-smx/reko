using Newtonsoft.Json;
using Reko.ApiServer.RestApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Reko.ApiServer
{
    public class SessionResourceMap : Dictionary<int, object> { }
    public class SessionVarGroup : Dictionary<Type, object> { }

    public class GenericVarStorage
    {
        public readonly SessionResourceMap Resources = new SessionResourceMap();
    }

    public class Client
    {
        private const string PREFIX = "/reko/v1";

        private readonly HttpListenerContext ctx;
        private readonly WebSocketContext wsCtx;
        private WebSocket ws => wsCtx.WebSocket;

        protected WebSocket WebSocket => wsCtx.WebSocket;

        public SessionVarGroup session = new SessionVarGroup() { };

        public T EnsureVariableStorage<T>(ApiModule module)
        {
            Type t = module.GetType();
            if (!session.ContainsKey(t))
            {
                session[t] = Activator.CreateInstance<T>();
            }
            return (T)session[t];
        }

        private static Dictionary<string, Type> modules = new Dictionary<string, Type>()
        {
            { "/projects", typeof(ProjectApi) }
        };

        private void RouteRequest(HttpMethod method, ApiRequestMessage req)
        {
            if (!req.url.StartsWith(PREFIX))
            {
                SendReply(null, HttpStatusCode.BadRequest);
                return;
            }
            req.url = req.url.Substring(PREFIX.Length);

            Type module = modules.Where(m => req.url.StartsWith(m.Key)).Select(m => m.Value).FirstOrDefault();
            if(module == null)
            {
                SendReply(null, HttpStatusCode.NotFound);
                return;
            }

            ApiModule api = (ApiModule)Activator.CreateInstance(module, this);
            api.Serve(method, req);
        }

        public void SendReply(object data = null, HttpStatusCode statusCode = HttpStatusCode.OK, string description = null)
        {
            SendMessageStrict(JsonConvert.SerializeObject(new ApiReplyMessage()
            {
                errcode = statusCode,
                errstr = description,
                data = data
            }));
        }

        private async Task<string> ReceiveMessage()
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            // read message size
            uint messageSize;
            ArraySegment<byte> bufHeader = new ArraySegment<byte>(new byte[sizeof(uint)]);
            {
                WebSocketReceiveResult resHeader = await ws.ReceiveAsync(bufHeader, cts.Token);

                if(resHeader.Count == 0 || resHeader.CloseStatus != null)
                {
                    return null;
                }

                if (resHeader.Count != sizeof(uint))
                {
                    throw new InvalidDataException();
                }

                messageSize = new BinaryReader(new MemoryStream(bufHeader.Array)).ReadUInt32();
            }

            // read message body
            ArraySegment<byte> buf = new ArraySegment<byte>(new byte[messageSize]);
            WebSocketReceiveResult res = await ws.ReceiveAsync(buf, cts.Token);
            if (res.MessageType == WebSocketMessageType.Close)
            {
                throw new Exception($"Connection Closed: {res.CloseStatus}, {res.CloseStatusDescription}");
            }

            return Encoding.UTF8.GetString(buf.Array);
        }

        protected async void SendMessage(string message)
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            byte[] data = Encoding.UTF8.GetBytes(message);
            await ws.SendAsync(new ArraySegment<byte>(data), WebSocketMessageType.Text, true, cts.Token);
        }

        private async Task<string> TryReceiveMessage()
        {
            string data = null;
            try
            {
                data = await ReceiveMessage();
            } catch (WebSocketException)
            {
                return null;
            }
            return data;
        }

        private async void SendMessageStrict(string message)
        {
            try
            {
                SendMessage(message);
            } catch (WebSocketException)
            {
                await ws.CloseAsync(WebSocketCloseStatus.ProtocolError, "Failed to send message", new CancellationTokenSource().Token);
            }
        }

        private async void ClientHandler(object arg)
        {
            WebSocket ws = (WebSocket) arg;
            CancellationTokenSource cts = new CancellationTokenSource(Timeout.InfiniteTimeSpan);

            while (ws.State == WebSocketState.Open)
            {
                string data = await TryReceiveMessage();
                if (data == null)
                    continue;

                ApiRequestMessage req = JsonConvert.DeserializeObject<ApiRequestMessage>(data);
                switch (req.method)
                {
                    case "GET":
                        RouteRequest(HttpMethod.GET, req);
                        break;
                    case "POST":
                        RouteRequest(HttpMethod.POST, req);
                        break;
                    case "PUT":
                        RouteRequest(HttpMethod.PUT, req);
                        break;
                    case "PATCH":
                        RouteRequest(HttpMethod.PATCH, req);
                        break;
                    case "DELETE":
                        RouteRequest(HttpMethod.DELETE, req);
                        break;
                    default:
                        SendReply(null, HttpStatusCode.MethodNotAllowed);
                        break;
                }
            }
        }

        public Client(HttpListenerContext ctx)
        {
            this.ctx = ctx;
            this.wsCtx = ctx.AcceptWebSocketAsync("reko", Timeout.InfiniteTimeSpan).Result;

            new Thread(new ParameterizedThreadStart(ClientHandler)).Start(ws);
        }
    }
}
