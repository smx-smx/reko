using Reko.ApiServer.RestApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Reko.ApiServer
{
    public delegate void ApiMethod(Dictionary<string, string> parameters);
    class UrlRequest : Dictionary<HttpMethod, ApiMethod> { }

    public class ApiModule
    {
        protected readonly Client Client;

        private readonly Dictionary<Regex, UrlRequest> methods = new Dictionary<Regex, UrlRequest>();

        private Regex CompileTemplate(string url)
        {
            url = Regex.Replace(url, @"\{(.*?)\}", m =>
            {
                string name = m.Groups[1].Value;
                return $@"(?<{name}>.*?)(?:\/|$)";
            });
            return new Regex(url, RegexOptions.Compiled);
        }

        private void AddRoute(string url, HttpMethod method, ApiMethod handler)
        {
            var key = methods.Where(m => m.Key.ToString() == url).Select(m => m.Key).FirstOrDefault();
            if(key == null)
            {
                Regex r = CompileTemplate(url);
                methods.Add(r, new UrlRequest() { { method, handler } });
            } else
            {
                methods[key].Add(method, handler);
            }

        }

        protected void Get(string url, ApiMethod handler) => AddRoute(url, HttpMethod.GET, handler);
        protected void Post(string url, ApiMethod handler) => AddRoute(url, HttpMethod.POST, handler);
        protected void Put(string url, ApiMethod handler) => AddRoute(url, HttpMethod.PUT, handler);
        protected void Patch(string url, ApiMethod handler) => AddRoute(url, HttpMethod.PATCH, handler);
        protected void Delete(string url, ApiMethod handler) => AddRoute(url, HttpMethod.DELETE, handler);

        public ApiModule(Client client)
        {
            this.Client = client;
        }

        public void Serve(HttpMethod method, ApiRequestMessage req)
        {
            KeyValuePair<Regex, UrlRequest>? match = null;
            foreach (var m in methods)
            {
                if (m.Key.IsMatch(req.url)) {
                    match = m;
                    break;
                }
            }

            if(match == null)
            {
                Client.SendReply(null, HttpStatusCode.NotFound, $"No handler found for {req.url}");
                return;
            }

            var handlers = match.Value.Value;
            if (!handlers.ContainsKey(method))
            {
                Client.SendReply(null, HttpStatusCode.MethodNotAllowed, $"No handler found for {req.url}:{req.method}");
                return;
            }

            var parameters = match.Value.Key
                .Matches(req.url)
                .Cast<Match>()
                .SelectMany(m => m.Groups.Cast<Group>().Skip(1))
                .ToDictionary(g => g.Name, g => g.Value);

            handlers[method].Invoke(parameters);
        }
    }
}
