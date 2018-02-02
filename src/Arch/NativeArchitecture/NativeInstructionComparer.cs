using Reko.Core;
using Reko.Core.Machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reko.Arch.NativeArchitecture
{
    public class NativeInstructionComparer : InstructionComparer
    {
        public NativeInstructionComparer(Normalize norm) : base(norm)
        {
        }

        public override bool CompareOperands(MachineInstruction x, MachineInstruction y)
        {
            throw new NotImplementedException();
        }

        public override int GetOperandsHash(MachineInstruction instr)
        {
            throw new NotImplementedException();
        }
    }
}
