using Reko.Core;
using Reko.Core.Code;
using Reko.Core.Expressions;
using Reko.Core.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reko.Arch.NativeArchitecture
{
    public class NativeProcessorState : ProcessorState
    {
        public NativeProcessorState()
        {
        }

        public NativeProcessorState(ProcessorState orig) : base(orig)
        {
        }

        public override IProcessorArchitecture Architecture
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override ProcessorState Clone()
        {
            throw new NotImplementedException();
        }

        public override Constant GetRegister(RegisterStorage r)
        {
            throw new NotImplementedException();
        }

        public override void OnAfterCall(FunctionType sigCallee)
        {
            throw new NotImplementedException();
        }

        public override CallSite OnBeforeCall(Identifier stackReg, int returnAddressSize)
        {
            throw new NotImplementedException();
        }

        public override void OnProcedureEntered()
        {
            throw new NotImplementedException();
        }

        public override void OnProcedureLeft(FunctionType procedureSignature)
        {
            throw new NotImplementedException();
        }

        public override void SetInstructionPointer(Address addr)
        {
            throw new NotImplementedException();
        }

        public override void SetRegister(RegisterStorage r, Constant v)
        {
            throw new NotImplementedException();
        }
    }
}
