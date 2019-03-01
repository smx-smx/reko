#region License
/* 
 * Copyright (C) 1999-2019 John K�ll�n.
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

using System;
using System.Collections.Generic;
using System.Text;

namespace Reko.UnitTests.Mocks
{
    public class BigLoopHeadFragment : ProcedureBuilder
    {
        protected override void BuildBody()
        {
            Label("loopHeader");
            SideEffect(Fn("DoSomething"));
            SideEffect(Fn("DoSomethingelse"));
            BranchIf(Fn("IsDone"), "done");
            Label("loopBody");
                SideEffect(Fn("LoopWork"));
                Goto("loopHeader");
            Label("done");
            Return();
        }
    }
}
