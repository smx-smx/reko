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

namespace Reko.Arch.PowerPC
{
    public enum Opcode : ushort
    {
        illegal,

        add,
        addc,
        adde,
        addco,
        addi,
        addic,
        addis,
        addme,
        addze,
        and,
        andc,
        andi,
        andis,
        b,
        bc,
        bcdadd,
        bcl,
        bclr,
        bclrl,
        bcctr,
        bctrl,
        bdnz,
        bdnzf,
        bdnzfl,
        bdnzl,
        bdnzt,
        bdnztl,
        bdz,
        bdzf,
        bdzfl,
        bdzl,
        bdzt,
        bdztl,
        beq,
        beql,
        beqlr,
        beqlrl,
        bge,
        bgel,
        bgelr,
        bgelrl,
        bgt,
        bgtl,
        bgtlr,
        bgtlrl,
        bl,
        ble,
        blel,
        blelr,
        blelrl,
        blr,
        blrl,
        blt,
        bltl,
        bltlr,
        bltlrl,
        bne,
        bnel,
        bnelr,
        bnelrl,
        bns,
        bnsl,
        bnslr,
        bnslrl,
        bso,
        bsol,
        bsolr,
        bsolrl,
        cmp,
        cmpi,
        cmpl,
        cmpli,
        cmplw,
        cmplwi,
        cmpwi,
        cntlzw,
        cntlzd,
        creqv,
        crnor,
        cror,
        crxor,
        dcbf,
        dcbi,
        dcbst,
        dcbt,
        dcbtst,
        dcbz,
        divd,
        divdu,
        divw,
        divwu,
        eieio,
        eqv,
        evmhessfaaw,
        evmhesmfaaw,
        extsb,
        extsh,
        extsw,
        fabs,
        fadd,
        fadds,
        fcmpo,
        fcmpu,
        fcfid,
        fctid,
        fctidz,
        fctiw,
        fctiwz,
        fdiv,
        fdivs,
        fmadd,
        fmadds,
        fmr,
        fmsub,
        fmsubs,
        fmul,
        fmuls,
        fnabs,
        fnmadd,
        fnmadds,
        fnmsub,
        fnmsubs,
        fneg,
        fres,
        frsp,
        frsqrte,
        fsel,
        fsub,
        fsubs,
        fsqrt,
        fsqrts,
        icbi,
        isync,
        lbz,
        lbzu,
        lbzux,
        lbzx,
        ld,
        ldarx,
        ldu,
        ldux,
        ldx,
        lfd,
        lfdp,
        lfdu,
        lfdux,
        lfdx,
        lfs,
        lfsu,
        lfsux,
        lfsx,
        lha,
        lhau,
        lhaux,
        lhax,
        lhbrx,
        lhz,
        lhzu,
        lhzx,
        lmw,
        lswi,
        lq,
        lvewx,
        lvlx,
        lvsl,
        lvx,
        lwarx,
        lwax,
        lwbrx,
        lwzu,
        lwz,
        lwzux,
        lwzx,
        mcrf,
        mfcr,
        mfctr,
        mftb,
        mffs,
        mflr,
        mfmsr,
        mfspr,
        mtcrf,
        mtctr,
        mtfsf,
        mtlr,
        mtmsr,
        mtmsrd,
        mtspr,
        mulld,
        mulli,
        mullw,
        mulhhwu,
        mulhw,
        mulhwu,
        nand,
        neg,
        nor,
        or,
        orc,
        ori,
        oris,
        psq_st,
        rfi,
        rldcl,
        rldcr,
        rldic,
        rldicl,
        rldicr,
        rldimi,
        rlwimi,
        rlwinm,
        rlwnm,
        sc,
        sld,
        slw,
        srad,
        sradi,
        sraw,
        srawi,
        srd,
        srw,
        stb,
        stbu,
        stbux,
        stbx,
        std,
        stdcx,
        stdu,
        stdx,
        stfd,
        stfdu,
        stfdux,
        stfdp,
        stfdx,
        stfiwx,
        stfs,
        stfsu,
        stfsux,
        stfsx,
        sth,
        sthu,
        sthx,
        stmw,
        stswi,
        stvewx,
        stvx,
        stw,
        stwbrx,
        stwcx,
        stwu,
        stwux,
        stwx,
        subf,
        subfc,
        subfe,
        subfic,
        subfze,
        sync,
        td,
        tdi,
        tw,
        twi,
        vaddubm,
        vadduwm,
        vaddfp,
        vaddubs,
        vadduqm,
        vand,
        vandc,
        vcfsx,
        vcmpbfp,
        vcmpeqfp,
        vcmpequb,
        vcmpequd,
        vcmpequw,
        vcmpgtfp,
        vcmpgtuw,
        vctsxs,
        vmaddfp,
        vmaxub,
        vmaxuh,
        vmladduhm,
        vmhaddshs,
        vmrghw,
        vmrglw,
        vnmsubfp,
        vor,
        vperm,
        vrefp,
        vrsqrtefp,
        vsel,
        vsldoi,
        vslw,
        vspltisw,
        vspltw,
        vsubfp,
        vxor,
        xor,
        xori,
        xoris,
        xsaddsp,
        xsmaddasp,
        xsmaddmsp,
        xxsldwi,

        // XBox360 extensions
        lvlx128,
        lvrx128,
        lvx128,
        stvx128,
        stvlx128,
        stvrx128,
        vaddfp128,
        vand128,
        vcfpsxws128,
        vcmpbfp128,
        vcmpeqfp128,
        vcmpequw128,
        vcmpgefp128,
        vcmpgtfp128,
        vcsxwfp128,
        vexptefp128,
        vlogefp128,
        vmaddcfp128,
        vmaxfp128,
        vminfp128,
        vmrghw128,
        vmrglw128,
        vmsub3fp128,
        vmsub4fp128,
        vmulfp128,
        vor128,
        vperm128,
        vpkd3d128,
        vrefp128,
        vrfin128,
        vrfiz128,
        vrlimi128,
        vrsqrtefp128,
        vsldoi128,
        vslw128,
        vspltisw128,
        vspltw128,
        vsrw128,
        vsubfp128,
        vupkd3d128,
        vxor128,
        vmsumshm,
        vmsumshs,
        vpermxor,
        ps_neg,
        ps_sub,
        ps_mul,
        ps_muls0,
        ps_muls1,
        ps_madds0,
        ps_madds1,
        ps_sum0,
        ps_sum1,
        ps_div,
        ps_add,
        ps_madd,
        psq_lx,
        psq_lux,
        psq_stx,
        psq_stux,
        ps_rsqrte,
        ps_mr,
        ps_nabs,
        ps_abs,
        ps_res,
        ps_merge00,
        ps_merge01,
        ps_merge10,
        ps_merge11,
        ps_msub,
        ps_nmsub,
        ps_nmadd,
        ps_sel,
        ps_cmpo0,
        psq_l,
        psq_lu,
        psq_stu
    }
}
