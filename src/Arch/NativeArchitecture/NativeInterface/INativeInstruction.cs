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
using Reko.Core.Machine;
using Reko.Core.NativeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Reko.Arch.NativeArchitecture.NativeInterfaces
{
    [ComVisible(true)]
    [Guid("2CAF9227-76D6-4DED-BC74-B95801E1524E")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface INativeInstruction
    {
        [PreserveSig] void GetInfo(out NativeInstructionInfo info);
        [PreserveSig] InstructionClass GetInstructionClass();
        [PreserveSig] bool GetIsValid();
        [PreserveSig] void Render(INativeInstructionWriter writer, MachineInstructionWriterOptions options);
        [PreserveSig] int GetOpcodeAsInteger();
        [PreserveSig] NativeOperandInfo GetOperand(int i, out IntPtr info);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NativeInstructionInfo
    {
        public ulong LinearAddress;
        public uint Length;
        public InstructionClass InstructionClass;
        public int Opcode;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NativeRegisterOperandInfo
    {
        public RegisterOperand ToRegisterOperand()
        {
            return new RegisterOperand(null);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NativeImmadiateOperandInfo
    {
        public ImmediateOperand ToImmediateOperand()
        {
            return new ImmediateOperand(null);
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct NativeAddressOperandInfo
    {
        [FieldOffset(0)]
        public uint AddressWidth;

        [FieldOffset(1)]
        public ushort ShortAddressValue;
        [FieldOffset(1)]
        public uint IntAddressValue;
        [FieldOffset(1)]
        public ulong LongAddressValue;

        public AddressOperand ToAddressOperand()
        {
            switch (AddressWidth)
            {
                case 2:
                    return AddressOperand.Ptr16(ShortAddressValue);
                case 4:
                    return AddressOperand.Ptr32(IntAddressValue);
                case 8:
                    return AddressOperand.Ptr64(LongAddressValue);
                default:
                    throw new NotSupportedException();
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NativeOperandInfo
    {
        public OperandType OperandType;
        public uint Witdh;

        public MachineOperand ToMachineOperand(IntPtr info)
        {
            switch (OperandType)
            {
                case OperandType.AddressOperand:
                    return Marshal.PtrToStructure<NativeAddressOperandInfo>(info).ToAddressOperand();
                case OperandType.RegisterOperand:
                    return Marshal.PtrToStructure<NativeRegisterOperandInfo>(info).ToRegisterOperand();
                case OperandType.ImmediateOperand:
                    return Marshal.PtrToStructure<NativeImmadiateOperandInfo>(info).ToImmediateOperand();
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
