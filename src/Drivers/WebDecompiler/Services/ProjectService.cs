using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Session;

namespace Reko.Drivers.WebDecompiler.Services
{
    public class ProjectService
    {
        public void Load(string projectPath)
        {
            Startup.Decompiler.Load(projectPath);
        }

        public void Scan()
        {
            Task.Run(() =>
            {
                Startup.Decompiler.ScanPrograms();
            });
        }

        public ProjectService() {
            
        }

        public ProjectService(IDecompiler decompiler)
        {

        }
    }
}
