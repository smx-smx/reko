using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reko.ApiServer.RestApi
{
    public class ApiRequestMessage
    {
        public string method { get; set; }
        public string url { get; set; }
        public object data { get; set; }
    }
}
