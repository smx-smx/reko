using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Reko.ApiServer.RestApi
{
    public class ApiReplyMessage
    {
        public HttpStatusCode errcode { get; set; }
        public string errstr { get; set; }
        public object data { get; set; }
    }
}
