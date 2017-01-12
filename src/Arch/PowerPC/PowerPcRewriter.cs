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
using Reko.Core.Operators;
using Reko.Core.Machine;
using Reko.Core.Rtl;
using Reko.Core.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Opcode = Gee.External.Capstone.PowerPc.PowerPcInstruction;
using Gee.External.Capstone.PowerPc;

namespace Reko.Arch.PowerPC
{
    public partial class PowerPcRewriter : IEnumerable<RtlInstructionCluster>
    {
        private Frame frame;
        private RtlEmitter emitter;
        private RtlInstructionCluster cluster;
        private PowerPcArchitecture arch;
        private IEnumerator<PowerPcInstruction> dasm;
        private IRewriterHost host;
        private PowerPcInstruction instr;

        public PowerPcRewriter(PowerPcArchitecture arch, IEnumerable<PowerPcInstruction> instrs, Frame frame, IRewriterHost host)
        {
            this.arch = arch;
            this.frame = frame;
            this.host = host;
            this.dasm = instrs.GetEnumerator();
        }

        public PowerPcRewriter(PowerPcArchitecture arch, EndianImageReader rdr, Frame frame, IRewriterHost host)
        {
            this.arch = arch;
            //this.state = ppcState;
            this.frame = frame;
            this.host = host;
            this.dasm = arch.CreateInternalDisassemblerImpl(rdr).GetEnumerator();
        }

        public IEnumerator<RtlInstructionCluster> GetEnumerator()
        {
            while (dasm.MoveNext())
            {
                this.instr = dasm.Current;
                this.cluster = new RtlInstructionCluster(instr.Address, 4);
                this.cluster.Class = RtlClass.Linear;
                this.emitter = new RtlEmitter(cluster.Instructions);
                switch (this.instr.Id)
                {
                default: throw new AddressCorrelatedException(
                    instr.Address,
                    "PowerPC instruction '{0}' ({1}) is not supported yet.",
                    instr,
                    instr.Id);
                case Opcode.ADDI: RewriteAddi(); break;
                case Opcode.ADDC: RewriteAddc(); break;
                case Opcode.ADDIC: RewriteAddic(); break;
                case Opcode.ADDIS: RewriteAddis(); break;
                case Opcode.ADD: RewriteAdd(); break;
                case Opcode.ADDE: RewriteAdde(); break;
                case Opcode.ADDME: RewriteAddme(); break;
                case Opcode.ADDZE: RewriteAddze(); break;
                case Opcode.AND: RewriteAnd(false); break;
                case Opcode.ANDC: RewriteAndc(); break;
                case Opcode.ANDI: RewriteAnd(false); break;
                case Opcode.ANDIS: RewriteAndis(); break;
                case Opcode.B: RewriteBranch(false, false, false, instr.ArchitectureDetail.BranchCode); break;
                case Opcode.BC: RewriteBc(false); break;
                case Opcode.BCTR: RewriteBranch(false, false, true, instr.ArchitectureDetail.BranchCode); break;
                case Opcode.BCCTR: RewriteBcctr(false); break;
                case Opcode.BCTRL: RewriteBranch(true, false, true, instr.ArchitectureDetail.BranchCode); break;
                case Opcode.BDNZ: RewriteCtrBranch(false, false, emitter.Ne, false); break;
                case Opcode.BDNZF: RewriteCtrBranch(false, false, emitter.Ne, false); break;
                case Opcode.BDNZL: RewriteCtrBranch(true, false, emitter.Ne, false); break;
                case Opcode.BDNZT: RewriteCtrBranch(false, false, emitter.Ne, true); break;
                case Opcode.BDZ: RewriteCtrBranch(false, false, emitter.Eq, false); break;
                case Opcode.BDZF: RewriteCtrBranch(false, false, emitter.Eq, false); break;
                case Opcode.BDZL: RewriteCtrBranch(true, false, emitter.Eq, false); break;
                case Opcode.BL: RewriteBranch(true, false, false, instr.ArchitectureDetail.BranchCode); break;
                case Opcode.BLR: RewriteBranch(false, true, false, instr.ArchitectureDetail.BranchCode); break;
                //$REVIEW: Capstone has no constants for these instructions? Wut?
                //case Opcode.BEQ: RewriteBranch(false, false,ConditionCode.EQ); break;
                //case Opcode.BEQL: RewriteBranch(true, false, ConditionCode.EQ); break;
                //case Opcode.BEQLR: RewriteBranch(false, true, ConditionCode.EQ); break;
                //case Opcode.BEQLRl: RewriteBranch(true, true, ConditionCode.EQ); break;
                //case Opcode.BGE: RewriteBranch(false, false,ConditionCode.GE); break;
                //case Opcode.BGEL: RewriteBranch(true, false,ConditionCode.GE); break;
                //case Opcode.BGT: RewriteBranch(false, false,ConditionCode.GT); break;
                //case Opcode.BGTL: RewriteBranch(true, false,ConditionCode.GT); break;
                //case Opcode.BL: RewriteBl(); break;
                //case Opcode.BLR: RewriteBlr(); break;
                //case Opcode.BLE: RewriteBranch(false, false, ConditionCode.LE); break;
                //case Opcode.BLEL: RewriteBranch(true, false, ConditionCode.LE); break;
                //case Opcode.BLELR: RewriteBranch(false, true, ConditionCode.LE); break;
                //case Opcode.BLELRL: RewriteBranch(true, true, ConditionCode.LE); break;
                //case Opcode.BLT: RewriteBranch(false, false, ConditionCode.LT); break;
                //case Opcode.BLTL: RewriteBranch(true, false, ConditionCode.LT); break;
                //case Opcode.BNE: RewriteBranch(false, false, ConditionCode.NE); break;
                //case Opcode.BNEL: RewriteBranch(true, false, ConditionCode.NE); break;
                //case Opcode.BNELR: RewriteBranch(false, true, ConditionCode.NE); break;
                //case Opcode.BNS: RewriteBranch(false, false, ConditionCode.NO); break;
                //case Opcode.BNSL: RewriteBranch(true, false, ConditionCode.NO); break;
                //case Opcode.BSO: RewriteBranch(false, false, ConditionCode.OV); break;
                //case Opcode.BSOL: RewriteBranch(true, false, ConditionCode.OV); break;
                //case Opcode.CMP: RewriteCmp(); break;
                //case Opcode.CMPI: RewriteCmpi(); break;
                //case Opcode.CMPL: RewriteCmpl(); break;
                //case Opcode.CMPLI: RewriteCmpli(); break;
                case Opcode.CLRLDI: RewriteClrldi(); break;
                case Opcode.CMPLW: RewriteCmplw(); break;
                case Opcode.CMPLWI: RewriteCmplwi(); break;
                case Opcode.CMPWI: RewriteCmpwi(); break;
                case Opcode.CNTLZD: RewriteCntlz("__cntlzd", PrimitiveType.Word64); break;
                case Opcode.CNTLZW: RewriteCntlz("__cntlzw", PrimitiveType.Word32); break;
                case Opcode.CRCLR: RewriteCrclr(); break;
                case Opcode.CREQV: RewriteCreqv(); break;
                case Opcode.CROR: RewriteCror(); break;
                case Opcode.CRNOR: RewriteCrnor(); break;
                case Opcode.CRNOT: RewriteCrnot(); break;
                case Opcode.CRSET: RewriteCrset(); break;
                case Opcode.CRXOR: RewriteCrxor(); break;
                case Opcode.DCBT: RewriteDcbt(); break;
                case Opcode.DIVW: RewriteDivw(); break;
                case Opcode.DIVWU: RewriteDivwu(); break;
                case Opcode.EXTSB: RewriteExts(PrimitiveType.SByte); break;
                case Opcode.EXTSH: RewriteExts(PrimitiveType.Int16); break;
                case Opcode.EXTSW: RewriteExts(PrimitiveType.Int32); break;
                case Opcode.FADD: RewriteFadd(); break;
                case Opcode.FADDS: RewriteFadd(); break;
                case Opcode.FCFID: RewriteFcfid(); break;
                case Opcode.FCTIWZ: RewriteFctiwz(); break;
                case Opcode.FCMPU: RewriteFcmpu(); break;
                case Opcode.FDIV: RewriteFdiv(); break;
                case Opcode.FDIVS: RewriteFdiv(); break;
                case Opcode.FMR: RewriteFmr(); break;
                case Opcode.FMADD: RewriteFmadd(); break;
                case Opcode.FMADDS: RewriteFmadd(); break;
                case Opcode.FMSUBS: RewriteFmsub(); break;
                case Opcode.FMUL: RewriteFmul(); break;
                case Opcode.FMULS: RewriteFmul(); break;
                case Opcode.FNEG: RewriteFneg(); break;
                case Opcode.FRSP: RewriteFrsp(); break;
                case Opcode.FSUB: RewriteFsub(); break;
                case Opcode.FSUBS: RewriteFsub(); break;
                case Opcode.LBZ: RewriteLz(PrimitiveType.Byte); break;
                case Opcode.LBZX: RewriteLzx(PrimitiveType.Byte); break;
                case Opcode.LBZU: RewriteLzu(PrimitiveType.Byte); break;
                case Opcode.LBZUX: RewriteLzux(PrimitiveType.Byte); break;
                case Opcode.LD: RewriteLz(PrimitiveType.Word64); break;
                case Opcode.LDU: RewriteLzu(PrimitiveType.Word64); break;
                case Opcode.LFD: RewriteLfd(); break;
                case Opcode.LFS: RewriteLfs(); break;
                case Opcode.LFSX: RewriteLzx(PrimitiveType.Real32); break;
                case Opcode.LHA: RewriteLha(); break;
                case Opcode.LHAU: RewriteLhau(); break;
                case Opcode.LHAUX: RewriteLhaux(); break;
                case Opcode.LHZ: RewriteLz(PrimitiveType.Word16); break;
                case Opcode.LHZU: RewriteLzu(PrimitiveType.Word16); break;
                case Opcode.LHZX: RewriteLzx(PrimitiveType.Word16); break;
                case Opcode.LI: RewriteLi(); break;
                case Opcode.LIS: RewriteLis(); break;
                case Opcode.LVEWX: RewriteLvewx(); break;
                //case Opcode.LVLX: RewriteLvlx(); break;
                case Opcode.LVSL: RewriteLvsl(); break;
                case Opcode.LVX: RewriteLzx(PrimitiveType.Word128); break;
                case Opcode.LWBRX: RewriteLwbrx(); break;
                case Opcode.LWZ: RewriteLz(PrimitiveType.Word32); break;
                case Opcode.LWZU: RewriteLzu(PrimitiveType.Word32); break;
                case Opcode.LWZX: RewriteLzx(PrimitiveType.Word32); break;
                case Opcode.MCRF: RewriteMcrf(); break;
                case Opcode.MFCR: RewriteMfcr(); break;
                case Opcode.MFCTR: RewriteMfctr(); break;
                case Opcode.MFTB: RewriteMftb(); break;
                case Opcode.MFFS: RewriteMffs(); break;
                case Opcode.MFLR: RewriteMflr(); break;
                case Opcode.MR: RewriteMr(); break;
                case Opcode.MTCRF: RewriteMtcrf(); break;
                case Opcode.MTCTR: RewriteMtctr(); break;
                case Opcode.MTFSF: RewriteMtfsf(); break;
                case Opcode.MTLR: RewriteMtlr(); break;
                case Opcode.MULHW: RewriteMulhw(); break;
                case Opcode.MULHWU: RewriteMulhwu(); break;
                case Opcode.MULLI: RewriteMull(); break;
                case Opcode.MULLD: RewriteMull(); break;
                case Opcode.MULLW: RewriteMull(); break;
                case Opcode.NEG: RewriteNeg(); break;
                case Opcode.NAND: RewriteAnd(true); break;
                case Opcode.NOR: RewriteOr(true); break;
                case Opcode.OR: RewriteOr(false); break;
                case Opcode.ORC: RewriteOrc(false); break;
                case Opcode.ORI: RewriteOr(false); break;
                case Opcode.ORIS: RewriteOris(); break;
                case Opcode.ROTLW: RewriteRotlw(); break;
                case Opcode.RLDICL: RewriteRldicl(); break;
                case Opcode.RLWINM: RewriteRlwinm(); break;
                case Opcode.RLWIMI: RewriteRlwimi(); break;
                case Opcode.RLWNM: RewriteRlwnm(); break;
                case Opcode.SC: RewriteSc(); break;
                case Opcode.SLD: RewriteSl(PrimitiveType.Word64); break;
                case Opcode.SLW: RewriteSl(PrimitiveType.Word32); break;
                case Opcode.SLWI: RewriteSl(PrimitiveType.Word32); break;
                case Opcode.SRADI: RewriteSra(); break;
                case Opcode.SRAW: RewriteSra(); break;
                case Opcode.SRAWI: RewriteSra(); break;
                case Opcode.SRW: RewriteSrw(); break;
                case Opcode.SRWI: RewriteSrw(); break;
                case Opcode.STB: RewriteSt(PrimitiveType.Byte); break;
                case Opcode.STBU: RewriteStu(PrimitiveType.Byte); break;
                case Opcode.STBUX: RewriteStux(PrimitiveType.Byte); break;
                case Opcode.STBX: RewriteStx(PrimitiveType.Byte); break;
                case Opcode.STD: RewriteSt(PrimitiveType.Word64); break;
                case Opcode.STDU: RewriteStu(PrimitiveType.Word64); break;
                case Opcode.STDX: RewriteStx(PrimitiveType.Word64); break;
                case Opcode.STFD: RewriteSt(PrimitiveType.Real64); break;
                case Opcode.STFIWX: RewriteStx(PrimitiveType.Int32); break;
                case Opcode.STFS: RewriteSt(PrimitiveType.Real32); break;
                case Opcode.STH: RewriteSt(PrimitiveType.Word16); break;
                case Opcode.STHU: RewriteStu(PrimitiveType.Word16); break;
                case Opcode.STHX: RewriteStx(PrimitiveType.Word16); break;
                case Opcode.STVEWX: RewriteStvewx(); break;
                case Opcode.STVX: RewriteStx(PrimitiveType.Word128); break;
                case Opcode.STW: RewriteSt(PrimitiveType.Word32); break;
                case Opcode.STWBRX: RewriteStwbrx(); break;
                case Opcode.STWU: RewriteStu(PrimitiveType.Word32); break;
                case Opcode.STWUX: RewriteStux(PrimitiveType.Word32); break;
                case Opcode.STWX: RewriteStx(PrimitiveType.Word32); break;
                case Opcode.SUBF: RewriteSubf(); break;
                case Opcode.SUBFC: RewriteSubfc(); break;
                case Opcode.SUBFE: RewriteSubfe(); break;
                case Opcode.SUBFIC: RewriteSubfic(); break;
                case Opcode.SUBFZE: RewriteSubfze(); break;
                case Opcode.SYNC: RewriteSync(); break;
                case Opcode.TW: RewriteTw(null); break;
                case Opcode.TWEQ: RewriteTw(emitter.Eq); break;
                case Opcode.TWLGT: RewriteTw(emitter.Ugt); break;
                case Opcode.TWLLT: RewriteTw(emitter.Ult); break;
                case Opcode.TWLT: RewriteTw(emitter.Lt); break;
                case Opcode.TWGT: RewriteTw(emitter.Gt); break;
                case Opcode.VADDFP: RewriteVaddfp(); break;
                case Opcode.VADDUWM: RewriteVadduwm(); break;
                case Opcode.VAND: RewriteAnd(false); break;
                case Opcode.VANDC: RewriteAndc(); break;
                case Opcode.VCFSX: RewriteVct("__vcfsx", PrimitiveType.Real32); break;
                case Opcode.VCMPGTFP: RewriteVcmpfp("__vcmpgtfp"); break;
                case Opcode.VCMPGTUW: RewriteVcmpuw("__vcmpgtuw"); break;
                case Opcode.VCMPEQFP: RewriteVcmpfp("__vcmpeqfp"); break;
                case Opcode.VCMPEQUW: RewriteVcmpfp("__vcmpequw"); break;
                case Opcode.VCTSXS: RewriteVct("__vctsxs", PrimitiveType.Int32); break;
                case Opcode.VMADDFP: RewriteVmaddfp(); break;
                case Opcode.VMRGHW: RewriteVmrghw(); break;
                case Opcode.VMRGLW: RewriteVmrglw(); break;
                case Opcode.VNMSUBFP: RewriteVnmsubfp(); break;
                case Opcode.VPERM: RewriteVperm(); break;
                case Opcode.VREFP: RewriteVrefp(); break;
                case Opcode.VRSQRTEFP: RewriteVrsqrtefp(); break;
                case Opcode.VSEL: RewriteVsel(); break;
                case Opcode.VSLDOI: RewriteVsldoi(); break;
                case Opcode.VSLW: RewriteVslw(); break;
                case Opcode.VSPLTISW: RewriteVspltisw(); break;
                case Opcode.VSPLTW: RewriteVspltw(); break;
                case Opcode.VSUBFP: RewriteVsubfp(); break;
                case Opcode.VXOR: RewriteXor(); break;
                case Opcode.XOR: RewriteXor(); break;
                case Opcode.XORI: RewriteXor(); break;
                case Opcode.XORIS: RewriteXoris(); break;
                }
                yield return cluster;
            }
        }

        private Expression Shift16(PowerPcInstructionOperand op)
        {
            var imm = op.ImmediateValue.Value;
            return Constant.Word32(imm << 16);
        }

        private Expression RewriteOperand(PowerPcInstructionOperand op, bool maybe0 = false)
        {
            switch (op.Type)
            {
            case PowerPcInstructionOperandType.Register:
                if (maybe0 && op.RegisterValue.Value == PowerPcRegister.R0)
                    return Constant.Zero(arch.WordWidth);
                return EnsureRegister(op.RegisterValue.Value);
            case PowerPcInstructionOperandType.Immediate:
                if (instr.UseAddressImmediate())
                {
                    return arch.MakeAddressFromLinear(op.ImmediateValue.Value);
                }
                else if (instr.UseSignedImmediate())
                {
                    // Sign-extend the bastard.
                    return SignExtend(op.ImmediateValue.Value, Domain.SignedInt);
                }
                else
                {
                    return SignExtend(op.ImmediateValue.Value, Domain.UnsignedInt);
                }
            }
            throw new NotImplementedException(
                string.Format("RewriteOperand:{0} ({1}}}", op, op.GetType()));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private Identifier EnsureRegister(PowerPcRegister ppcReg)
        {
            return frame.EnsureRegister(arch.RegisterByCapstoneID[ppcReg]);
        }

        private Expression EffectiveAddress(PowerPcInstructionOperand mop, RtlEmitter emitter)
        {
            var reg = EnsureRegister(mop.MemoryValue.BaseRegister);
            var offset = mop.MemoryValue.Displacement;
            return emitter.IAdd(
                reg,
                Constant.Create(
                    PrimitiveType.Create(Domain.SignedInt, reg.DataType.Size),
                    offset));
        }

        private Expression EffectiveAddress_r0(PowerPcInstructionOperand mop, RtlEmitter emitter)
        {
            if (mop.MemoryValue.BaseRegister == PowerPcRegister.R0)
            {
                return Constant.Word32((int)mop.MemoryValue.Displacement);
            }
            else
            {
                return EffectiveAddress(mop, emitter);
            }
        }

        private Expression SignExtend(long value, Domain dom)
        {
            if (arch.WordWidth.BitSize == 64)
            {
                return (dom == Domain.SignedInt)
                    ? Constant.Int64(value)
                    : Constant.Word64(value);
            }
            else
            {
                return (dom == Domain.SignedInt)
                    ? Constant.Int32((int)value)
                    : Constant.Word32((int)value);
            }
        }

        private Expression UpdatedRegister(Expression effectiveAddress)
        {
            var bin = (BinaryExpression) effectiveAddress;
            return bin.Left;
        }
    }
}
