﻿#region License
/* 
 * Copyright (C) 1999-2016 John Källén.
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

using NUnit.Framework;
using Reko.Arch.Tlcs;
using Reko.Core;
using Reko.Core.Configuration;
using Reko.Core.Rtl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reko.UnitTests.Arch.Tlcs
{
    [TestFixture]
    public class Tlcs900RewriterTests : RewriterTestBase
    {
        private Tlcs900Architecture arch = new Tlcs900Architecture();
        private Address baseAddr = Address.Ptr32(0x0010000);
        private Tlcs900ProcessorState state;
        private MemoryArea image;

        public override IProcessorArchitecture Architecture
        {
            get { return arch; }
        }

        protected override IEnumerable<RtlInstructionCluster> GetInstructionStream(Frame frame, IRewriterHost host)
        {
            return new Tlcs900Rewriter(arch, new LeImageReader(image, 0), state, new Frame(arch.WordWidth), host);
        }

        public override Address LoadAddress
        {
            get { return baseAddr; }
        }

        protected override MemoryArea RewriteCode(string hexBytes)
        {
            var bytes = OperatingEnvironmentElement.LoadHexBytes(hexBytes)
                .ToArray();
            this.image = new MemoryArea(LoadAddress, bytes);
            return image;
        }


        [SetUp]
        public void Setup()
        {
            state = (Tlcs900ProcessorState)arch.CreateProcessorState();
        }

        [Test]
        public void Tlcs900_rw_ld()
        {
            RewriteCode("9F1621");
            AssertCode(
                "0|L--|00010000(3): 2 instructions",
                "1|L--|v3 = Mem0[xsp + 22:word16]",
                "2|L--|bc = v3");
        }

        [Test]
        public void Tlcs900_rw_add()
        {
            RewriteCode("E9C8FFFFFFFF");
            AssertCode(
                "0|L--|00010000(6): 3 instructions",
                "1|L--|xbc = xbc + 0xFFFFFFFF",
                "2|L--|N = false",
                "3|L--|SZHVC = cond(xbc)");
        }

        [Test]
        public void Tlcs900_rw_inc_predec()
        {
            RewriteCode("E40961"); // inc\t00000001,(-xde)
            AssertCode(
                "0|L--|00010000(3): 5 instructions",
                "1|L--|xde = xde - 0x00000004",
                "2|L--|v3 = Mem0[xde:word32] + 0x00000001",
                "3|L--|Mem0[xde:word32] = v3",
                "4|L--|N = false",
                "5|L--|SZHV = cond(v3)");
        }

        [Test]
        public void Tlcs900_rw_sub_postinc()
        {
            RewriteCode("E509A8"); // sub\t,(xde+),xwa
            AssertCode(
                "0|L--|00010000(3): 5 instructions",
                "1|L--|v4 = Mem0[xde:word32] - xwa",
                "2|L--|Mem0[xde:word32] = v4",
                "3|L--|xde = xde + 0x00000004",
                "4|L--|N = true",
                "5|L--|SZHVC = cond(v4)");
        }

        [Test]
        public void Tlcs900_rw_jp_cc()
        {
            //$REVIEW: not sure if I agree here. Shouldn't this be
            // simply if (Test(GE,SV) goto xwa?
            RewriteCode("B0D9"); // jp\tGE,(xwa)
            AssertCode(
                "0|T--|00010000(2): 2 instructions",
                "1|L--|v4 = Mem0[xwa:word32]",
                "2|T--|if (Test(GE,SV)) goto v4");
        }

        [Test]
        public void Tlcs900_rw_call()
        {
            RewriteCode("1D563412"); // call\t123456
            AssertCode(
                "0|T--|00010000(4): 1 instructions",
                "1|T--|call 00123456 (4)");
        }

        [Test]
        public void Tlcs900_rw_djnz()
        {
            RewriteCode("D91CED");      // djnz\tbc,0000FFF0
            AssertCode(
                "0|T--|00010000(3): 2 instructions",
                "1|L--|bc = bc - 0x0001",
                "2|T--|if (bc != 0x0000) branch 0000FFF0");
        }
    }
}