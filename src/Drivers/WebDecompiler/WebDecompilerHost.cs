using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Reko.Core;
using Reko.Core.Configuration;
using Reko.Core.Output;

namespace Reko.Drivers.WebDecompiler
{
    public class WebDecompilerHost : DecompilerHost
    {
        public IConfigurationService Configuration
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void WriteDecompiledCode(Core.Program program, Action<TextWriter> writer)
        {
            throw new NotImplementedException();
        }

        public void WriteDisassembly(Core.Program program, Action<Formatter> writer)
        {
            throw new NotImplementedException();
        }

        public void WriteGlobals(Core.Program program, Action<TextWriter> writer)
        {
            throw new NotImplementedException();
        }

        public void WriteIntermediateCode(Core.Program program, Action<TextWriter> writer)
        {
            throw new NotImplementedException();
        }

        public void WriteTypes(Core.Program program, Action<TextWriter> writer)
        {
            throw new NotImplementedException();
        }
    }
}
