using Reko.Arch.NativeArchitecture.NativeInterfaces;
using Reko.Core.Machine;
using Reko.Core.NativeInterface;
using System;
using System.Collections.Generic;

namespace Reko.Arch.NativeArchitecture
{
    public class NativeInstruction : MachineInstruction
    {
        private INativeInstruction nativeInstruction;

        public NativeInstruction(INativeInstruction nativeInstruction)
        {
            this.nativeInstruction = nativeInstruction;
        }

        private InstructionClass? _instructionClass = null;
        private bool? _isValid = null;
        private int? _opcodeInteger = null;

        private Dictionary<int, MachineOperand> operands = new Dictionary<int, MachineOperand>();

        public override InstructionClass InstructionClass
        {
            get
            {
                if (!_instructionClass.HasValue)
                    _instructionClass = nativeInstruction.GetInstructionClass();

                return _instructionClass.Value;
            }
        }

        public override bool IsValid
        {
            get
            {
                if (!_isValid.HasValue)
                    _isValid = nativeInstruction.GetIsValid();

                return _isValid.Value;
            }
        }

        public override int OpcodeAsInteger
        {
            get
            {
                if (!_opcodeInteger.HasValue)
                    _opcodeInteger = nativeInstruction.GetOpcodeAsInteger();

                return _opcodeInteger.Value;
            }
        }

        public override MachineOperand GetOperand(int i)
        {
            MachineOperand opnd;
            if (!operands.ContainsKey(i))
            {
                NativeOperandInfo info = nativeInstruction.GetOperand(i, out IntPtr infoData);
                opnd = info.ToMachineOperand(infoData);
                operands[i] = opnd;
            }
            else
            {
                opnd = operands[i];
            }

            return opnd;
        }
    }
}