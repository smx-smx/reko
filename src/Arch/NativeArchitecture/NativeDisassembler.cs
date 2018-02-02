using Reko.Core;
using Reko.Core.NativeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reko.Arch.NativeArchitecture
{
    public class NativeDisassembler : DisassemblerBase<NativeInstruction>
    {
        private INativeDisassembler nativeDasm;
        public NativeDisassembler(INativeDisassembler nativeDasm)
        {
            this.nativeDasm = nativeDasm;
        }

        public override NativeInstruction DisassembleInstruction()
        {
            return new NativeInstruction(nativeDasm.NextInstruction());
        }
    }
}
