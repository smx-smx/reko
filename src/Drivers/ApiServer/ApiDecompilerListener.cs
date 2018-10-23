using Reko.Core;
using Reko.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reko.ApiServer
{
    class ApiDecompilerListener : DecompilerEventListener
    {
        public ApiDecompilerListener()
        {
        }

        public ICodeLocation CreateAddressNavigator(Program program, Address address)
        {
            return new NullCodeLocation("");
        }

        public ICodeLocation CreateBlockNavigator(Program program, Block block)
        {
            return new NullCodeLocation("");
        }

        public ICodeLocation CreateJumpTableNavigator(Program program, Address addrIndirectJump, Address addrVector, int stride)
        {
            return new NullCodeLocation("");
        }

        public ICodeLocation CreateProcedureNavigator(Program program, Procedure proc)
        {
            return new NullCodeLocation("");
        }

        public ICodeLocation CreateStatementNavigator(Program program, Statement stm)
        {
            return new NullCodeLocation("");
        }

        public void Error(ICodeLocation location, string message)
        {
            
        }

        public void Error(ICodeLocation location, string message, params object[] args)
        {
            
        }

        public void Error(ICodeLocation location, Exception ex, string message)
        {
            
        }

        public void Error(ICodeLocation location, Exception ex, string message, params object[] args)
        {
            
        }

        public void Info(ICodeLocation location, string message)
        {
            
        }

        public void Info(ICodeLocation location, string message, params object[] args)
        {
            
        }

        public bool IsCanceled()
        {
            return false;
        }

        public void ShowProgress(string caption, int numerator, int denominator)
        {
            
        }

        public void ShowStatus(string caption)
        {
            
        }

        public void Warn(ICodeLocation location, string message)
        {
            
        }

        public void Warn(ICodeLocation location, string message, params object[] args)
        {
            
        }
    }
}
