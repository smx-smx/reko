using Reko.Core;
using Reko.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reko.ApiServer
{
    class ApiDecompilerDiagnostics : IDiagnosticsService
    {
        public void ClearDiagnostics()
        {
            throw new NotImplementedException();
        }

        public void Error(string message)
        {
            throw new NotImplementedException();
        }

        public void Error(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Error(Exception ex, string message)
        {
            throw new NotImplementedException();
        }

        public void Error(ICodeLocation location, string message)
        {
            throw new NotImplementedException();
        }

        public void Error(ICodeLocation location, string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Error(ICodeLocation location, Exception ex, string message)
        {
            throw new NotImplementedException();
        }

        public void Error(ICodeLocation location, Exception ex, string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Inform(string message)
        {
            throw new NotImplementedException();
        }

        public void Inform(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Inform(ICodeLocation location, string message)
        {
            throw new NotImplementedException();
        }

        public void Inform(ICodeLocation location, string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Warn(string message)
        {
            throw new NotImplementedException();
        }

        public void Warn(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Warn(ICodeLocation location, string message)
        {
            throw new NotImplementedException();
        }

        public void Warn(ICodeLocation location, string message, params object[] args)
        {
            throw new NotImplementedException();
        }
    }
}
