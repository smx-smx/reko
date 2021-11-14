using Newtonsoft.Json.Linq;
using Reko.CmdLineServer.Renderers;
using Reko.Core;
using Reko.Core.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Reko.CmdLineServer
{
    public class CmdLineServer
    {
        private IServiceProvider sc;
        private TextReader inputChannel;
        private TextWriter outputChannel;
        private TextWriter eventChannel;

        private Thread inputReader;

        private IDecompiler decompiler;
        private IDecompilerService decompilerSvc;

        private ProcedureListRenderer procListRenderer;

        public void SendEventMessage(string message)
        {
            eventChannel.WriteLine(message);
        }

        public CmdLineServer(IServiceProvider sc,
            TextReader inputChannel,
            TextWriter outputChannel,
            TextWriter eventChannel)
        {
            this.sc = sc;
            this.inputChannel = inputChannel;
            this.outputChannel = outputChannel;
            this.eventChannel = eventChannel;

            inputReader = new Thread(() => CommandProcessor());
        }
        
        public void Start()
        {
            decompilerSvc = sc.RequireService<IDecompilerService>();
            decompiler = decompilerSvc.Decompiler;
            procListRenderer = new ProcedureListRenderer(decompiler);

            inputReader.Start();
        }

        public void Join()
        {
            inputReader.Join();
        }

        public void SendDefaultReply()
        {
            outputChannel.WriteLine("{ result: \"OK\"}");
        }

        public void CommandProcessor()
        {
            bool stopRequested = false;
            while (!stopRequested)
            {
                var msg = inputChannel.ReadLine();
                if (msg == null) continue;

                dynamic obj = JObject.Parse(msg);

                string cmd = (string)obj["cmd"];
                switch (cmd)
                {
                case "openFile":
                    decompiler.Load((string)obj["filePath"], null, null);
                    SendDefaultReply();
                    break;
                case "scanPrograms":
                    decompiler.ScanPrograms();
                    SendDefaultReply();
                    break;
                case "getProcedureList":
                    outputChannel.WriteLine(procListRenderer.Render(""));
                    break;
                case "exit":
                    SendDefaultReply();
                    stopRequested = true;
                    break;
                }
            }
        }
    }
}
