using Reko.Core;
using Reko.Core.Configuration;
using Reko.Core.Services;
using Reko.Loading;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reko.ApiServer
{
    public class DriverInstance
    {
        private readonly IServiceProvider services = new ServiceContainer();
        public readonly ILoader ldr;
        public readonly IDecompiler Decompiler;
        public readonly IConfigurationService config;
        public readonly IDiagnosticsService diagnosticSvc;
        //private CmdLineListener listener;
        //private Timer timer;

        public DriverInstance(/*CmdLineListener listener*/)
        {
            this.config = RekoConfigurationService.Load();
            // load services
            {
                ServiceContainer sc = services as ServiceContainer;
                sc.AddService<DecompilerEventListener>(new ApiDecompilerListener());
                sc.AddService<IConfigurationService>(config);
                sc.AddService<ITypeLibraryLoaderService>(new TypeLibraryLoaderServiceImpl(services));
                sc.AddService<IDiagnosticsService>(new ApiDecompilerDiagnostics());
                sc.AddService<IFileSystemService>(new FileSystemServiceImpl());
                sc.AddService<DecompilerHost>(new ApiDecompilerHost());
                //services.AddService<DecompilerHost>(new CmdLineHost());
            }

            this.ldr = new Loader(services);
            this.Decompiler = new DecompilerDriver(ldr, services);

            //this.listener = listener;
            this.config = services.RequireService<IConfigurationService>();
            this.diagnosticSvc = services.RequireService<IDiagnosticsService>();
        }
    }
}
