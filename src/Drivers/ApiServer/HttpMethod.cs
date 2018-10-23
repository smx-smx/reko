using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reko.ApiServer
{
    public enum HttpMethod
    {
        /// <summary>
        /// A readonly, reproducible request to fetch a resource
        /// </summary>
        /// <remarks>Can have side effects, but the client shouldn't see them</remarks>
        GET,
        /// <summary>
        /// A request to update a resource, usually to create a new one
        /// </summary>
        POST,
        /// <summary>
        /// A request to replace a resource with a different one
        /// </summary>
        PUT,
        /// <summary>
        /// A request to update certain fields of a resource
        /// </summary>
        PATCH,
        /// <summary>
        /// A request to delete a resource
        /// </summary>
        DELETE
    }
}
