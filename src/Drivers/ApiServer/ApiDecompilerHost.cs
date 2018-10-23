using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reko.Core;
using Reko.Core.Configuration;
using Reko.Core.Output;

namespace Reko.ApiServer
{
    class ApiDecompilerHost : DecompilerHost
    {
        public IConfigurationService Configuration
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void WriteDecompiledCode(Program program, Action<TextWriter> writer)
        {
        }

        public void WriteDisassembly(Program program, Action<Formatter> writer)
        {
        }

        public void WriteGlobals(Program program, Action<TextWriter> writer)
        {
        }

        public void WriteIntermediateCode(Program program, Action<TextWriter> writer)
        {
        }

        public void WriteTypes(Program program, Action<TextWriter> writer)
        {
        }
    }
}
