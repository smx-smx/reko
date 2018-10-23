using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Reko.ApiServer.RestApi
{
    class ProjectApiVariables : GenericVarStorage
    {
        public int NumberOfProjects { get; set; }
    }

    public class ProjectApi : ApiModule
    {
        private readonly ProjectApiVariables vars;

        public ProjectApi(Client client) : base(client)
        {
            vars = client.EnsureVariableStorage<ProjectApiVariables>(this);

            Get("/projects", (_) =>
            {
                Client.SendReply(vars.Resources);
            });

            Post("/projects", (_) =>
            {
                vars.Resources.Add(++vars.NumberOfProjects, new
                {
                    test = 1234
                });
            });
        }
    }
}
