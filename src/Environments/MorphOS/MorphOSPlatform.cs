#region License
/* 
 * Copyright (C) 1999-2020 John Källén.
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
using Reko.Core.CLanguage;
using System;
using System.Collections.Generic;

namespace Reko.Environments.MorphOS
{
    public class MorphOSPlatform : Platform
    {
        public MorphOSPlatform(IServiceProvider services, IProcessorArchitecture arch, string platformId) 
            : base(services, arch, platformId)
        {
        }

        public override string DefaultCallingConvention => "";

        public override IPlatformEmulator CreateEmulator(SegmentMap segmentMap, Dictionary<Address, ImportReference> importReferences)
        {
            throw new NotImplementedException();
        }

        public override HashSet<RegisterStorage> CreateTrashedRegisters()
        {
            throw new NotImplementedException();
        }

        public override SystemService FindService(int vector, ProcessorState state, SegmentMap segmentMap)
        {
            throw new NotImplementedException();
        }

        public override int GetBitSizeFromCBasicType(CBasicType cb)
        {
            throw new NotImplementedException();
        }

        public override CallingConvention GetCallingConvention(string ccName)
        {
            throw new NotImplementedException();
        }

        public override ExternalProcedure LookupProcedureByName(string moduleName, string procName)
        {
            throw new NotImplementedException();
        }
    }
}
