#region License
/* 
 * Copyright (C) 1999-2021 John Källén.
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2, or (at your option)
 * any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; see the file COPYING.  If not, write to
 * the Free Software Foundation, 675 Mass Ave, Cambridge, MA 02139, USA.
 */
#endregion

using Reko.Core;
using Reko.Core.Scripts;
using Reko.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Reko.CmdLineServer
{
    public class CmdLineListener : DecompilerEventListener
    {
        private readonly CmdLineServer srv;

        public CmdLineListener(IServiceProvider services)
        {
            srv = services.RequireService<CmdLineServer>();
        }

        public void Advance(int count)
        {
            //JavaScript.ExecuteScript($"diagnostics.advance({count})");
        }

        public ICodeLocation CreateAddressNavigator(Core.Program program, Address address)
        {
            return new JsLocation(@$"{{""program"":""{program.Name}"",""addr"":""{address}""}}");
        }

        public ICodeLocation CreateBlockNavigator(Core.Program program, Block block)
        {
            return new JsLocation(@$"{{""program"":""{program.Name}"",""blockAddr"":""{block.Address}""}}");
        }

        public ICodeLocation CreateJumpTableNavigator(Core.Program program, IProcessorArchitecture arch, Address addrIndirectJump, Address? addrVector, int stride)
        {
            throw new NotImplementedException();
        }

        public ICodeLocation CreateProcedureNavigator(Core.Program program, Procedure proc)
        {
            return new JsLocation(@$"{{""program"":""{program.Name}"",""procAddr"":""{proc.EntryAddress}""}}");
        }

        public ICodeLocation CreateStatementNavigator(Core.Program program, Statement stm)
        {
            return new JsLocation(@$"{{""program"":""{program.Name}"",""stmLoc"":""{stm.LinearAddress}""}}");
        }

        public void Error(string message)
        {
            Error(new NullCodeLocation(""), message);
        }

        public void Error(string format, params object[] args)
        {
            Error(new NullCodeLocation(""), string.Format(format, args));
        }

        public void Error(Exception ex, string message)
        {
            Error(new NullCodeLocation(""), ex, message);
        }

        public void Error(Exception ex, string format, params object[] args)
        {
            Error(new NullCodeLocation(""), ex, string.Format(format, args));
        }

        public void Error(ICodeLocation location, string message)
        {
            srv.SendEventMessage($"{{ \"type\": \"error\", \"location\": {Quote(location.Text)}, \"message\": {Quote(message)} }}");
        }

        public void Error(ICodeLocation location, string message, params object[] args)
        {
            Error(location, string.Format(message, args));
        }

        public void Error(ICodeLocation location, Exception ex, string message)
        {
            Error(location, string.Format("{0} {1}", ex.Message, message));
        }

        public void Error(ICodeLocation location, Exception ex, string message, params object[] args)
        {
            Error(location, ex, string.Format(message, args));
        }

        public void Info(string message)
        {
            Info(new NullCodeLocation(""), message);
        }

        public void Info(string format, params object[] args)
        {
            Info(new NullCodeLocation(""), string.Format(format, args));
        }

        public void Info(ICodeLocation location, string message)
        {
            srv.SendEventMessage($"{{ \"type\": \"info\", \"location\": {Quote(location.Text)}, \"message\": {Quote(message)} }}");
        }

        public void Info(ICodeLocation location, string message, params object[] args)
        {
            Info(location, string.Format(message, args));
        }

        public void ShowProgress(string caption, int numerator, int denominator)
        {
            //JavaScript.ExecuteScript($"diagnostics.progress({Quote(caption)}, {numerator}, {denominator})");
        }

        public void ShowStatus(string caption)
        {
            //JavaScript.ExecuteScript($"diagnostics.status({Quote(caption)})");
        }


        public void Warn(string message)
        {
            Error(new NullCodeLocation(""), message);
        }

        public void Warn(string format, params object[] args)
        {
            Error(new NullCodeLocation(""), string.Format(format, args));
        }

        public void Error(ScriptError scriptError)
        {
            Error(new NullCodeLocation(scriptError.FileName), $"{scriptError.LineNumber}:{scriptError.Message}");
        }

        public void Warn(ICodeLocation location, string message)
        {
            srv.SendEventMessage($"{{ \"type\": \"warning\", \"location\": {Quote(location.Text)}, \"message\": {Quote(message)} }}");
        }

        public void Warn(ICodeLocation location, string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public class JsLocation : ICodeLocation
        {
            public JsLocation(string jsonText)
            {
                this.Text = jsonText;
            }

            public string Text { get; }

            public void NavigateTo()
            {
                throw new NotSupportedException();
            }
        }

        private string Quote(string s)
        {
            var sb = new StringBuilder();
            sb.Append('"');
            foreach (var ch in s)
            {
                switch (ch)
                {
                case '"': sb.Append(@"\"""); break;
                case '\\': sb.Append(@"\\"); break;
                default: sb.Append(ch); break;
                }
            }
            sb.Append('"');
            return sb.ToString();
        }

        private bool canceled;

        public void Cancel()
        {
            this.canceled = true;
        }

        public void ResetCancel()
        {
            this.canceled = false;
        }

        public bool IsCanceled()
        {
            return canceled;
        }
    }
}
