using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reko.Arch.NativeArchitecture.NativeInterfaces
{
    public enum OperandType : uint
    {
        ImmediateOperand = 0,
        RegisterOperand,
        FpuOperand,
        AddressOperand
    }
}
