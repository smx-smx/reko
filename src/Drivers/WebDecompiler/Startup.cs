using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reko.Core.Configuration;
using Reko.Core.Services;
using Reko.Core;
using Reko.Loading;
using Reko.Drivers.WebDecompiler.Hubs;
using System.IO;
using System.Reflection;

namespace Reko.Drivers.WebDecompiler
{
    public class Startup
    {
        public static IDecompiler Decompiler { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IDecompiler CreateDecompilerDriver(string configPath)
        {
            var services = new ServiceContainer();
            var listener = new NullDecompilerEventListener();


            var config = RekoConfigurationService.LoadFromFile(configPath);
            var diagnosticSvc = new WebDiagnosticsService();
            services.AddService<DecompilerEventListener>(listener);
            services.AddService<IConfigurationService>(config);
            services.AddService<ITypeLibraryLoaderService>(new TypeLibraryLoaderServiceImpl(services));
            services.AddService<IDiagnosticsService>(diagnosticSvc);
            services.AddService<IFileSystemService>(new FileSystemServiceImpl());
            services.AddService<DecompilerHost>(new WebDecompilerHost());
            var ldr = new Loader(services);
            var decompiler = new DecompilerDriver(ldr, services);

            return decompiler;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSignalR();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSignalR(routes =>
            {
                routes.MapHub<DecompilerHub>("/events");
            });

            string configPath = Path.Combine(env.ContentRootPath, "reko.config");
            Decompiler = CreateDecompilerDriver(configPath);
        }
    }
}
