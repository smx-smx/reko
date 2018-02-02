using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Reko.Arch.NativeArchitecture.NativeInterface;
using Reko.Arch.NativeArchitecture.NativeInterfaces;
using Reko.Core;
using Reko.Core.Configuration;
using Reko.Core.Expressions;
using Reko.Core.Machine;
using Reko.Core.NativeInterface;
using Reko.Core.Rtl;
using Reko.Core.Types;

namespace Reko.Arch.NativeArchitecture
{
    public class NativeProcessorArchitecture : ProcessorArchitecture
    {
        private Endianness endianess;
        private INativeArchitecture native;
        private INativeDisassembler nativeDasm;

        [DllImport("NativeProxy", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern IntPtr CreateNativeArchitecture(NativeArchitectureType archType);

        private NativeArchitectureType arch;
        public NativeProcessorArchitecture(Architecture arch)
        {
            switch (arch.Name)
            {
                case "arm":
                    this.arch = NativeArchitectureType.Arm32;
                    break;
                default:
                    throw new NotSupportedException();
            }

            this.CreateNativeArchitecture();
        }

        private void CreateNativeArchitecture()
        {
            IntPtr unk = CreateNativeArchitecture(arch);
            this.native = (INativeArchitecture)Marshal.GetObjectForIUnknown(unk);
            Marshal.Release(unk);

            InstructionBitSize = native.GetInstructionBitSize();

            int pointerSize = native.GetPointerBitSize();
            int wordSize = native.GetWordBitSize();

            PointerType = PrimitiveType.Create(Domain.Pointer, pointerSize / 8);
            WordWidth = PrimitiveType.CreateWord(wordSize / 8);
        }

        // Not called
        public NativeProcessorArchitecture()
        {
            /*InstructionBitSize = 32;
            FramePointerType = PrimitiveType.Ptr32;
            PointerType = PrimitiveType.Ptr32;
            WordWidth = PrimitiveType.Word32;
            StackRegister = A32Registers.sp;
            this.flagGroups = new Dictionary<uint, FlagGroupStorage>();*/
        }

        public NativeProcessorArchitecture(Endianness endianess)
        {
            this.endianess = endianess;
        }

        public override IEnumerable<MachineInstruction> CreateDisassembler(EndianImageReader rdr)
        {
            var bytes = rdr.Bytes;
            ulong uAddr = rdr.Address.ToLinear();
            var hBytes = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            INativeDisassembler ndasm = null;

            try
            {
                ndasm = native.CreateDisassembler(hBytes.AddrOfPinnedObject(), bytes.Length, (int)rdr.Offset, uAddr);
                for (; ; )
                {
                    INativeInstruction nInstr = ndasm.NextInstruction();
                    if (nInstr == null)
                        yield break;
                    else
                        yield return new NativeInstruction(nInstr);
                }
            }
            finally
            {
                if (ndasm != null)
                {
                    ndasm = null;
                }
                if (hBytes != null && hBytes.IsAllocated)
                {
                    hBytes.Free();
                }
            }
        }

        public override EndianImageReader CreateImageReader(MemoryArea img, Address addr)
        {
            switch (endianess)
            {
                case Endianness.BigEndian:
                    return new BeImageReader(img, addr);
                case Endianness.LittleEndian:
                    return new LeImageReader(img, addr);
                default:
                    throw new NotSupportedException();
            }
        }

        public override EndianImageReader CreateImageReader(MemoryArea img, Address addrBegin, Address addrEnd)
        {
            switch (endianess)
            {
                case Endianness.BigEndian:
                    return new BeImageReader(img, addrBegin, addrEnd);
                case Endianness.LittleEndian:
                    return new LeImageReader(img, addrBegin, addrEnd);
                default:
                    throw new NotSupportedException();
            }
        }

        public override EndianImageReader CreateImageReader(MemoryArea img, ulong off)
        {
            switch (endianess)
            {
                case Endianness.BigEndian:
                    return new BeImageReader(img, off);
                case Endianness.LittleEndian:
                    return new LeImageReader(img, off);
                default:
                    throw new NotSupportedException();
            }
        }

        public override ImageWriter CreateImageWriter()
        {
            switch (endianess)
            {
                case Endianness.BigEndian:
                    return new BeImageWriter();
                case Endianness.LittleEndian:
                    return new LeImageWriter();
                default:
                    throw new NotSupportedException();
            }
        }

        public override ImageWriter CreateImageWriter(MemoryArea img, Address addr)
        {
            switch (endianess)
            {
                case Endianness.BigEndian:
                    return new BeImageWriter(img, addr);
                case Endianness.LittleEndian:
                    return new LeImageWriter(img, addr);
                default:
                    throw new NotSupportedException();
            }
        }

        public override IEqualityComparer<MachineInstruction> CreateInstructionComparer(Normalize norm)
        {
            return new NativeInstructionComparer(norm);
        }

        public override IEnumerable<Address> CreatePointerScanner(SegmentMap map, EndianImageReader rdr, IEnumerable<Address> knownAddresses, PointerScannerFlags flags)
        {
            throw new NotImplementedException();
        }

        public override ProcessorState CreateProcessorState()
        {
            return new NativeProcessorState();
        }

        public override IEnumerable<RtlInstructionCluster> CreateRewriter(EndianImageReader rdr, ProcessorState state, IStorageBinder frame, IRewriterHost host)
        {
            return new NativeRewriter(this, rdr, (NativeProcessorState)state, frame, host);
        }

        public override Expression CreateStackAccess(IStorageBinder binder, int cbOffset, DataType dataType)
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

        public override SortedList<string, int> GetOpcodeNames()
        {
            throw new NotImplementedException();
        }

        public override int? GetOpcodeNumber(string name)
        {
            throw new NotImplementedException();
        }

        public override RegisterStorage GetRegister(int i)
        {
            throw new NotImplementedException();
        }

        public override RegisterStorage GetRegister(string name)
        {
            throw new NotImplementedException();
        }

        public override RegisterStorage[] GetRegisters()
        {
            throw new NotImplementedException();
        }

        public override string GrfToString(uint grf)
        {
            throw new NotImplementedException();
        }

        public override Address MakeAddressFromConstant(Constant c)
        {
            throw new NotImplementedException();
        }

        public override Address ReadCodeAddress(int size, EndianImageReader rdr, ProcessorState state)
        {
            throw new NotImplementedException();
        }

        public override bool TryGetRegister(string name, out RegisterStorage reg)
        {
            throw new NotImplementedException();
        }

        public override bool TryParseAddress(string txtAddr, out Address addr)
        {
            throw new NotImplementedException();
        }
    }
}
