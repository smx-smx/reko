﻿#region License
/* 
 * Copyright (C) 1999-2017 John Källén.
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
using System.Linq;
using System.Text;
using Reko.Core;
using Reko.Core.Types;
using Reko.Scanning.StringFormats;
using Reko.Core.Types.StringTypes;

namespace Reko.Scanning
{
    /// <summary>
    /// Given a program, returns all addresses within the program that
    /// _appear_ to be strings.
    /// </summary>
    public class StringFinder
    {
        private Program program;

        public StringFinder(Program program)
        {
            this.program = program;
        }

        public IEnumerable<ProgramAddress> FindStrings(StringType stringType, int minLength)
        {
            foreach (var segment in program.SegmentMap.Segments.Values)
            {
                Address segEnd = Address.Min(
                    segment.Address + segment.Size,
                    segment.MemoryArea.BaseAddress + segment.MemoryArea.Bytes.Length);
                var rdr = program.Architecture.CreateImageReader(segment.MemoryArea, segment.Address);
                Address addrStartRun = null;

                //$TODO: CreateDecoder() in stringType?
                Type decoderType;
                if(stringType is UnicodeStringType) {
                    decoderType = typeof(UnicodeStringDecoder);
                } else {
                    decoderType = typeof(CStringDecoder);
                }

                StringDecoder decoder = Activator.CreateInstance(decoderType, new object[]{rdr, new StringScannerSettings(
                    stringType.Encoding,
                    StringScannerFlags.IsPrintable | StringScannerFlags.StrictEncoding | StringScannerFlags.MinimumLength,
                    minLength
                )}) as StringDecoder;

                while (rdr.Address < segEnd)
                {
                    string str;
                    addrStartRun = rdr.Address;
                    if (decoder.TryDecodeString(out str) && str.Length > minLength) {
                        yield return new ProgramStringAddress(program, addrStartRun, str);
                    }
                }
            }
        }

        public bool IsValid(char ch)
        {
            return (' ' <= ch && ch < 0x7F);
        }
    }
}
