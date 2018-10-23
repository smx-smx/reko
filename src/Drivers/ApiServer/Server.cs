using Reko.ApiServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reko.ApiServer
{
    class Server
    {
        public const string PREFIX = "/reko/v1/";
        private readonly HttpListener listener;

        public Server(string baseUrl)
        {
            listener = new HttpListener();
            listener.Prefixes.Add(baseUrl);
        }

        public void Serve()
        {
            listener.Start();
            while (true)
            {
                HttpListenerContext ctx = listener.GetContext();
                ctx.Response.KeepAlive = true;

                Console.WriteLine($"Incoming connection: {ctx.Request.UserHostAddress}");
                Client c = new Client(ctx);
            }
        }

        public static void Main(string[] args)
        {
            new Server("http://127.0.0.1:8080" + PREFIX).Serve();
        }
    }
}
