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

using Reko.Core;
using Reko.Core.Expressions;
using Reko.Core.Lib;
using Reko.Core.Machine;
using Reko.Core.Rtl;
using Reko.Core.Serialization;
using Reko.Core.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Opcode = Gee.External.Capstone.PowerPc.PowerPcInstruction;
using DisassembleMode = Gee.External.Capstone.DisassembleMode;
using Gee.External.Capstone.PowerPc;

namespace Reko.Arch.PowerPC
{
    public abstract class PowerPcArchitecture : ProcessorArchitecture
    {
        private ReadOnlyCollection<RegisterStorage> regs;
        private ReadOnlyCollection<RegisterStorage> fpregs;
        private ReadOnlyCollection<RegisterStorage> vregs;
        private ReadOnlyCollection<RegisterStorage> cregs;

        public RegisterStorage lr { get; private set; }
        public RegisterStorage ctr { get; private set; }
        public RegisterStorage xer { get; private set; }
        public RegisterStorage fpscr { get; private set; }

        public FlagRegister cr { get; private set; }

        /// <summary>
        /// Creates an instance of PowerPcArchitecture.
        /// </summary>
        /// <param name="wordWidth">Supplies the word width of the PowerPC architecture.</param>
        public PowerPcArchitecture(PrimitiveType wordWidth)
        {
            WordWidth = wordWidth;
            PointerType = PrimitiveType.Create(Domain.Pointer, wordWidth.Size);
            FramePointerType = PointerType;
            InstructionBitSize = 32;

            this.lr = new RegisterStorage("lr", 0x68, 0, wordWidth);
            this.ctr = new RegisterStorage("ctr", 0x6A, 0, wordWidth);
            this.xer = new RegisterStorage("xer", 0x6B, 0, wordWidth);
            this.fpscr = new RegisterStorage("fpscr", 0x6C, 0, wordWidth);

            this.cr = new FlagRegister("cr", 0x80, wordWidth);

            const int CrStart = 0x60;
            regs = new ReadOnlyCollection<RegisterStorage>(
                Enumerable.Range(0, 0x20)
                    .Select(n => new RegisterStorage("r" + n, n, 0, wordWidth))
                .Concat(Enumerable.Range(0, 0x20)
                    .Select(n => new RegisterStorage("f" + n, n + 0x20, 0, PrimitiveType.Word64)))
                .Concat(Enumerable.Range(0, 0x20)
                    .Select(n => new RegisterStorage("v" + n, n + 0x40, 0, PrimitiveType.Word128)))
                .Concat(Enumerable.Range(0, 8)
                    .Select(n => new RegisterStorage("cr" + n, n + CrStart, 0, PrimitiveType.Byte)))
                .Concat(new[] { lr, ctr, xer })
                .ToList());

            fpregs = new ReadOnlyCollection<RegisterStorage>(
                regs.Skip(0x20).Take(0x20).ToList());

            vregs = new ReadOnlyCollection<RegisterStorage>(
                regs.Skip(0x40).Take(0x20).ToList());

            cregs = new ReadOnlyCollection<RegisterStorage>(
                regs.Skip(0x60).Take(0x8).ToList());

            //$REVIEW: using R1 as the stack register is a _convention_. It 
            // should be platform-specific at the very least.
            StackRegister = regs[1];

            RegisterByCapstoneID = new Dictionary<PowerPcRegister, RegisterStorage>
            {
                { PowerPcRegister.R0, regs[0] },
                { PowerPcRegister.R1, regs[1] },
                { PowerPcRegister.R2, regs[2] },
                { PowerPcRegister.R3, regs[3] },
                { PowerPcRegister.R4, regs[4] },
                { PowerPcRegister.R5, regs[5] },
                { PowerPcRegister.R6, regs[6] },
                { PowerPcRegister.R7, regs[7] },
                { PowerPcRegister.R8, regs[8] },
                { PowerPcRegister.R9, regs[9] },
                { PowerPcRegister.R10, regs[10] },
                { PowerPcRegister.R11, regs[11] },
                { PowerPcRegister.R12, regs[12] },
                { PowerPcRegister.R13, regs[13] },
                { PowerPcRegister.R14, regs[14] },
                { PowerPcRegister.R15, regs[15] },
                { PowerPcRegister.R16, regs[16] },
                { PowerPcRegister.R17, regs[17] },
                { PowerPcRegister.R18, regs[18] },
                { PowerPcRegister.R19, regs[19] },
                { PowerPcRegister.R20, regs[20] },
                { PowerPcRegister.R21, regs[21] },
                { PowerPcRegister.R22, regs[22] },
                { PowerPcRegister.R23, regs[23] },
                { PowerPcRegister.R24, regs[24] },
                { PowerPcRegister.R25, regs[25] },
                { PowerPcRegister.R26, regs[26] },
                { PowerPcRegister.R27, regs[27] },
                { PowerPcRegister.R28, regs[28] },
                { PowerPcRegister.R29, regs[29] },
                { PowerPcRegister.R30, regs[30] },
                { PowerPcRegister.R31, regs[31] },

                { PowerPcRegister.F0, fpregs[0] },
                { PowerPcRegister.F1, fpregs[1] },
                { PowerPcRegister.F2, fpregs[2] },
                { PowerPcRegister.F3, fpregs[3] },
                { PowerPcRegister.F4, fpregs[4] },
                { PowerPcRegister.F5, fpregs[5] },
                { PowerPcRegister.F6, fpregs[6] },
                { PowerPcRegister.F7, fpregs[7] },
                { PowerPcRegister.F8, fpregs[8] },
                { PowerPcRegister.F9, fpregs[9] },
                { PowerPcRegister.F10, fpregs[10] },
                { PowerPcRegister.F11, fpregs[11] },
                { PowerPcRegister.F12, fpregs[12] },
                { PowerPcRegister.F13, fpregs[13] },
                { PowerPcRegister.F14, fpregs[14] },
                { PowerPcRegister.F15, fpregs[15] },
                { PowerPcRegister.F16, fpregs[16] },
                { PowerPcRegister.F17, fpregs[17] },
                { PowerPcRegister.F18, fpregs[18] },
                { PowerPcRegister.F19, fpregs[19] },
                { PowerPcRegister.F20, fpregs[20] },
                { PowerPcRegister.F21, fpregs[21] },
                { PowerPcRegister.F22, fpregs[22] },
                { PowerPcRegister.F23, fpregs[23] },
                { PowerPcRegister.F24, fpregs[24] },
                { PowerPcRegister.F25, fpregs[25] },
                { PowerPcRegister.F26, fpregs[26] },
                { PowerPcRegister.F27, fpregs[27] },
                { PowerPcRegister.F28, fpregs[28] },
                { PowerPcRegister.F29, fpregs[29] },
                { PowerPcRegister.F30, fpregs[30] },
                { PowerPcRegister.F31, fpregs[31] },

                { PowerPcRegister.V0, vregs[0] },
                { PowerPcRegister.V1, vregs[1] },
                { PowerPcRegister.V2, vregs[2] },
                { PowerPcRegister.V3, vregs[3] },
                { PowerPcRegister.V4, vregs[4] },
                { PowerPcRegister.V5, vregs[5] },
                { PowerPcRegister.V6, vregs[6] },
                { PowerPcRegister.V7, vregs[7] },
                { PowerPcRegister.V8, vregs[8] },
                { PowerPcRegister.V9, vregs[9] },
                { PowerPcRegister.V10, vregs[10] },
                { PowerPcRegister.V11, vregs[11] },
                { PowerPcRegister.V12, vregs[12] },
                { PowerPcRegister.V13, vregs[13] },
                { PowerPcRegister.V14, vregs[14] },
                { PowerPcRegister.V15, vregs[15] },
                { PowerPcRegister.V16, vregs[16] },
                { PowerPcRegister.V17, vregs[17] },
                { PowerPcRegister.V18, vregs[18] },
                { PowerPcRegister.V19, vregs[19] },
                { PowerPcRegister.V20, vregs[20] },
                { PowerPcRegister.V21, vregs[21] },
                { PowerPcRegister.V22, vregs[22] },
                { PowerPcRegister.V23, vregs[23] },
                { PowerPcRegister.V24, vregs[24] },
                { PowerPcRegister.V25, vregs[25] },
                { PowerPcRegister.V26, vregs[26] },
                { PowerPcRegister.V27, vregs[27] },
                { PowerPcRegister.V28, vregs[28] },
                { PowerPcRegister.V29, vregs[29] },
                { PowerPcRegister.V30, vregs[30] },
                { PowerPcRegister.V31, vregs[31] },

                { PowerPcRegister.CR0, cregs[0] },
                { PowerPcRegister.CR1, cregs[1] },
                { PowerPcRegister.CR2, cregs[2] },
                { PowerPcRegister.CR3, cregs[3] },
                { PowerPcRegister.CR4, cregs[4] },
                { PowerPcRegister.CR5, cregs[5] },
                { PowerPcRegister.CR6, cregs[6] },
                { PowerPcRegister.CR7, cregs[7] },
            };
        }

        public ReadOnlyCollection<RegisterStorage> Registers
        {
            get { return regs; }
        }

        public ReadOnlyCollection<RegisterStorage> FpRegisters
        {
            get { return fpregs; }
        }

        public ReadOnlyCollection<RegisterStorage> VecRegisters
        {
            get { return vregs; }
        }
        public ReadOnlyCollection<RegisterStorage> CrRegisters
        {
            get { return cregs; }
        }

        public readonly Dictionary<PowerPcRegister, RegisterStorage> RegisterByCapstoneID;


        #region IProcessorArchitecture Members

        public PowerPcDisassembler CreateInternalDisassemblerImpl(EndianImageReader rdr)
        {
            var mode = WordWidth.Size == 64
                ? DisassembleMode.BigEndian | DisassembleMode.Bit64
                : DisassembleMode.BigEndian | DisassembleMode.Bit32;
            return new PowerPcDisassembler(mode, rdr);
        }

        public IEnumerable<MachineInstruction> CreateInternalDisassembler(EndianImageReader rdr)
        {
            return CreateInternalDisassemblerImpl(rdr);
        }

        public override EndianImageReader CreateImageReader(MemoryArea image, Address addr)
        {
            //$TODO: PowerPC is bi-endian.
            return new BeImageReader(image, addr);
        }

        public override EndianImageReader CreateImageReader(MemoryArea image, Address addrBegin, Address addrEnd)
        {
            //$TODO: PowerPC is bi-endian.
            return new BeImageReader(image, addrBegin, addrEnd);
        }

        public override EndianImageReader CreateImageReader(MemoryArea image, ulong offset)
        {
            //$TODO: PowerPC is bi-endian.
            return new BeImageReader(image, offset);
        }

        public override ImageWriter CreateImageWriter()
        {
            //$TODO: PowerPC is bi-endian.
            return new BeImageWriter();
        }

        public override ImageWriter CreateImageWriter(MemoryArea mem, Address addr)
        {
            //$TODO: PowerPC is bi-endian.
            return new BeImageWriter(mem, addr);
        }

        public override IEqualityComparer<MachineInstruction> CreateInstructionComparer(Normalize norm)
        {
            throw new NotImplementedException();
        }

        public override abstract IEnumerable<Address> CreatePointerScanner(
            SegmentMap map,
            EndianImageReader rdr,
            IEnumerable<Address> addrs,
            PointerScannerFlags flags);

        //public override ProcedureBase GetTrampolineDestination(EndianImageReader rdr, IRewriterHost host)
        //{
        //    var dasm = new PowerPcDisassembler(this, rdr, WordWidth);
        //    return GetTrampolineDestination(dasm, host);
        //}

        /// <summary>
        /// Detects the presence of a PowerPC trampoline and returns the imported function 
        /// that is actually being requested.
        /// </summary>
        /// <remarks>
        /// A PowerPC trampoline looks like this:
        ///     addis  rX,r0,XXXX (or oris rx,r0,XXXX)
        ///     lwz    rY,YYYY(rX)
        ///     mtctr  rY
        ///     bctr   rY
        /// When loading the ELF binary, we discovered the memory locations
        /// that will contain pointers to imported functions. If the address
        /// XXXXYYYY matches one of those memory locations, we have found a
        /// trampoline.
        /// </remarks>
        /// <param name="rdr"></param>
        /// <param name="host"></param>
        /// <returns></returns>
        public ProcedureBase GetTrampolineDestination(IEnumerable<PowerPcInstruction> rdr, IRewriterHost host)
        {
            var e = rdr.GetEnumerator();

            //$TODO: verify that both ORIS and ADDIS map to LIS
            if (!e.MoveNext() || (e.Current.Opcode != Opcode.LIS))
                return null;
            var addrInstr = e.Current.Address;
            var reg = e.Current.op1.RegisterValue.Value;
            var uAddr = (uint)e.Current.op3.ImmediateValue.Value << 16;

            if (!e.MoveNext() || e.Current.Opcode != Opcode.LWZ)
                return null;
            if (e.Current.op2.Type != PowerPcInstructionOperandType.Memory)
                return null;
            if (e.Current.op2.MemoryValue.BaseRegister != reg)
                return null;
            uAddr = (uint)((int)uAddr + e.Current.op2.MemoryValue.Displacement);
            reg = e.Current.op1.RegisterValue.Value;

            if (!e.MoveNext() || e.Current.Opcode != Opcode.MTCTR)
                return null;
            if (e.Current.op1.RegisterValue!= reg)
                return null;

            if (!e.MoveNext() || e.Current.Opcode != Opcode.BCCTR)
                return null;

            // We saw a thunk! now try to resolve it.

            var addr = Address.Ptr32(uAddr);
            var ep = host.GetImportedProcedure(addr, addrInstr);
            if (ep != null)
                return ep;
            return host.GetInterceptedCall(addr);
        }

        public override ProcessorState CreateProcessorState()
        {
            return new PowerPcState(this);
        }

        public override SortedList<string, int> GetOpcodeNames()
        {
            return Enum.GetValues(typeof(Opcode))
                .Cast<Opcode>()
                .ToSortedList(v => Enum.GetName(typeof(Opcode), v), v => (int)v);
        }

        public override int? GetOpcodeNumber(string name)
        {
            Opcode result;
            if (!Enum.TryParse(name, true, out result))
                return null;
            return (int)result;
        }

        public override RegisterStorage GetRegister(int i)
        {
            if (0 <= i && i < regs.Count)
                return regs[i];
            else
                return null;
        }

        public override RegisterStorage GetRegister(string name)
        {
            return this.regs.Where(r => r.Name == name).SingleOrDefault();
        }

        public override RegisterStorage[] GetRegisters()
        {
            return regs.ToArray();
        }

        public override bool TryGetRegister(string name, out RegisterStorage reg)
        {
            throw new NotImplementedException();
        }

        public override FlagGroupStorage GetFlagGroup(uint grf)
        {
            throw new NotImplementedException();
        }

        public override FlagGroupStorage GetFlagGroup(string name)
        {
            throw new NotImplementedException();
        }

        public override RegisterStorage GetSubregister(RegisterStorage reg, int offset, int width)
        {
            if (offset == 0)
                return reg;
            else
                return null;
        }

        public override string GrfToString(uint grf)
        {
            //$BUG: this needs to be better conceved. There are 
            // 32 (!) condition codes in the PowerPC architecture
            return "crX";
        }

        public override Expression CreateStackAccess(Frame frame, int cbOffset, DataType dataType)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<RtlInstructionCluster> CreateRewriter(EndianImageReader rdr, ProcessorState state, Frame frame, IRewriterHost host)
        {
            return new PowerPcRewriter(this, rdr, frame, host);
        }

        public override abstract Address MakeAddressFromConstant(Constant c);

        public abstract Address MakeAddressFromLinear(long value);

        public override Address ReadCodeAddress(int size, EndianImageReader rdr, ProcessorState state)
        {
            throw new NotImplementedException();
        }

        public override bool TryParseAddress(string txtAddress, out Address addr)
        {
            return Address.TryParse32(txtAddress, out addr);
        }

        #endregion
    }

    public class PowerPcArchitecture32 : PowerPcArchitecture
    {
        public PowerPcArchitecture32()
            : base(PrimitiveType.Word32)
        { }

        public override IEnumerable<MachineInstruction> CreateDisassembler(EndianImageReader imageReader)
        {
            return new PowerPcDisassembler(this, imageReader);
        }

        public override IEnumerable<Address> CreatePointerScanner(
            SegmentMap map,
            EndianImageReader rdr,
            IEnumerable<Address> knownAddresses,
            PointerScannerFlags flags)
        {
            var knownLinAddresses = knownAddresses
                .Select(a => a.ToUInt32())
                .ToHashSet();
            return new PowerPcPointerScanner32(rdr, knownLinAddresses, flags)
                .Select(u => Address.Ptr32(u));
        }

        public override Address MakeAddressFromConstant(Constant c)
        {
            return Address.Ptr32(c.ToUInt32());
        }

        public override Address MakeAddressFromLinear(long value)
        {
            return Address.Ptr32((uint)value);
        }
    }

    public class PowerPcArchitecture64 : PowerPcArchitecture
    {
        public PowerPcArchitecture64()
            : base(PrimitiveType.Word64)
        { }

        public override IEnumerable<MachineInstruction> CreateDisassembler(EndianImageReader imageReader)
        {
            return new PowerPcDisassembler(this, imageReader);
        }

        public override IEnumerable<Address> CreatePointerScanner(
            SegmentMap map,
            EndianImageReader rdr,
            IEnumerable<Address> knownAddresses,
            PointerScannerFlags flags)
        {
            var knownLinAddresses = knownAddresses
                .Select(a => a.ToLinear())
                .ToHashSet();
            return new PowerPcPointerScanner64(rdr, knownLinAddresses, flags)
                .Select(u => Address.Ptr64(u));
        }

        public override Address MakeAddressFromConstant(Constant c)
        {
            return Address.Ptr64(c.ToUInt64());
        }

        public override Address MakeAddressFromLinear(long value)
        {
            return Address.Ptr64((ulong)value);
        }
    }
}
