#region License
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

using NUnit.Framework;
using Reko.Core;
using Reko.ImageLoaders.Dol;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reko.UnitTests.ImageLoaders.Dol
{
    [TestFixture]
    public class DolLoaderTests
    {
        [Test]
        public void Doll_LoadBinary()
        {
            var sc = new ServiceContainer();
            var image = new byte[]
            {
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC, 0xDE, 0xF0,
                0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF, 0x00, 0x11,
                0xFF, 0xFE, 0xFD, 0xFC, 0xFB, 0xFA, 0xF9, 0xF8,
                0x01, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00
            };
            var dol = new DolLoader(sc, "foo.dol", image);
            var hdr = new DolHeader(new DolStructure
            {
                offsetText = new uint[] { 0x0008, 0x0010, 0, 0, 0, 0, 0 },
                offsetData = new uint[] { 0x0018, 0x0020, 0, 0, 0, 0, 0, 0, 0, 0, 0 },

                addressText = new uint[] { 0x90000000, 0x91000000, 0, 0, 0, 0, 0 },
                addressData = new uint[] { 0xA0000000, 0xA8000000, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                addressBSS = 0x70000000,

                sizeText = new uint[] { 8, 8, 0, 0, 0, 0, 0 },
                sizeData = new uint[] { 8, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                sizeBSS = 0x100
            });
            var segmap = dol.BuildSegmentMap(Address.Ptr32(0x8003F00), hdr);

            Assert.AreEqual(5, segmap.Segments.Count);
            Assert.AreEqual("Segment .bss at 70000000, 100 / 100 bytes", segmap.Segments.Values[0].ToString());
            Assert.AreEqual("Segment Text1 at 90000000, 8 / 8 bytes", segmap.Segments.Values[1].ToString());
            Assert.AreEqual("Segment Text2 at 91000000, 8 / 8 bytes", segmap.Segments.Values[2].ToString());
            Assert.AreEqual("Segment Data0 at A0000000, 8 / 8 bytes", segmap.Segments.Values[3].ToString());
            Assert.AreEqual("Segment Data1 at A8000000, 8 / 8 bytes", segmap.Segments.Values[4].ToString());

            Assert.AreEqual(0x12345678, segmap.Segments.Values[1].MemoryArea.ReadBeInt32(0));
        }
    }
}
