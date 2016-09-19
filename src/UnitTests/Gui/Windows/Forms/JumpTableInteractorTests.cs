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
using Reko.Core;
using Reko.Gui.Windows.Forms;
using Reko.Scanning;
using Reko.UnitTests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reko.UnitTests.Gui.Windows.Forms
{
    [TestFixture]
    [Category(Categories.UserInterface)]
    public class JumpTableInteractorTests
    {
        private JumpTableDialog dlg;
        private Program program;

        [SetUp]
        public void Setup()
        {
            dlg = null;
        }

        [TearDown]
        public void TearDown()
        {
            if (dlg != null) dlg.Dispose();
        }

        private void Given_Dialog_32Bit()
        {
            this.dlg = new JumpTableDialog()
            {
                Program = program,
                Instruction = new FakeInstruction(Operation.Jump) { Address = Address.Ptr32(0x1000) }
            };
        }

        private void Given_Program()
        {
            var arch = new FakeArchitecture();
            var platform = new FakePlatform(null, arch);

            this.program = new Program
            {
                SegmentMap = new SegmentMap(
                        Address.Ptr32(0x1000),
                        new ImageSegment(
                            ".text",
                            new MemoryArea(Address.Ptr32(0x1000), new byte[1000]),
                            AccessMode.ReadExecute)),
                Platform = platform,
                Architecture = arch,
            };
        }

        private void Given_Table_UInt32(Address address, params uint[] entries)
        {
            ImageSegment seg;
            program.SegmentMap.Segments.TryGetValue(address, out seg);
            var writer = new LeImageWriter(seg.MemoryArea, address);
            foreach (uint entry in entries)
            {
                writer.WriteLeUInt32(entry);
            }
        }

        [Test]
        public void Jti_GetAddresses_Linear()
        {
            Given_Program();
            Given_Dialog_32Bit();
            Given_Table_UInt32(Address.Ptr32(0x1000), 0x1010, 0x01023, 0x01018);

            dlg.Show();
            dlg.JumpTableStartAddress.Text = "001000";
            dlg.IndexRegister.SelectedIndex = 0;
            dlg.EntryCount.Value = 3;
            dlg.Refresh();
            dlg.AcceptButton.PerformClick();
            dlg.Close();

            var table = dlg.GetResults();
            Assert.AreEqual(Address.Ptr32(0x1000), table.Address);
            Assert.AreEqual(3, table.Table.Addresses.Count);
            Assert.AreEqual(Address.Ptr32(0x01018), table.Table.Addresses[2]);
        }

        [Test]
        public void Jti_Load_Segments()
        {
            Given_Program();
            Given_Dialog_32Bit();

            dlg.Show();
            Assert.AreEqual(1, dlg.SegmentList.Items.Count);
            Assert.AreEqual(".text", dlg.SegmentList.Items[0]);
        }

        [Test]
        public void Jti_Load_Registers()
        {
            Given_Program();
            Given_Dialog_32Bit();

            dlg.Show();
            Assert.AreEqual(64, dlg.IndexRegister.Items.Count);
            Assert.AreEqual("r0", dlg.IndexRegister.Items[0].ToString());
        }

        [Test]
        public void Jti_Item_Count_Changed()
        {
            Given_Program();
            Given_Dialog_32Bit();
            Given_Table_UInt32(Address.Ptr32(0x1000), 0x1010, 0x01023, 0x01018);

            dlg.Show();
            dlg.JumpTableStartAddress.Text = "001000";
            dlg.EntryCount.Value = 2;

            Assert.AreEqual(2, dlg.Entries.Items.Count);
        }
    }
}
