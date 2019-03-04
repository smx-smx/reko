using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reko.Drivers.WebDecompiler.Hubs
{
    public class DecompilerHub : Hub
    {
        public async Task Load(string fileName)
        {
            await Task.Run(() =>
            {
                Startup.Decompiler.Load(fileName);
            });

            await Clients.Caller.SendAsync("StatusUpdate", "Project Loaded");
        }
    }
}
