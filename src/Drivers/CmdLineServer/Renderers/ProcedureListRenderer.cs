using Reko.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Reko.CmdLineServer.Renderers
{
    public class ProcedureListRenderer
    {
        private readonly IDecompiler decompiler;
        public ProcedureListRenderer(IDecompiler decompiler)
        {
            this.decompiler = decompiler;
        }

        public string Render(string filter)
        {
            var project = decompiler.Project;
            if (project is null)
                return "[]";
            var viewModel = project.Programs
                .Select(program => program.Procedures.Values
                    .Select(proc => new ProcedureListItem
                    {
                        sProgram = program.Filename,
                        sAddress = proc.EntryAddress.ToString(),
                        name = proc.Name
                    }))
                .SelectMany(pp => pp);
            if (!string.IsNullOrEmpty(filter))
            {
                viewModel = viewModel.Where(item => ItemMatchesFilter(item, filter));
            }
            return JsonSerializer.Serialize(viewModel);
        }

        private static bool ItemMatchesFilter(ProcedureListItem item, string filter)
        {
            return
                item.sAddress != null && item.sAddress.Contains(filter, StringComparison.InvariantCultureIgnoreCase) ||
                item.name != null && item.name.Contains(filter, StringComparison.InvariantCultureIgnoreCase);
        }

        public class ProcedureListItem
        {
            public string? sProgram { get; set; }
            public string? sAddress { get; set; }
            public string? name { get; set; }
        }
    }
}
