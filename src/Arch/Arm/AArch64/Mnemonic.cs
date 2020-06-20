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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reko.Arch.Arm.AArch64
{
    public enum Mnemonic
    {
        Invalid,

        add,
        adds,
        addv,
        adr,
        adrp,
        and,
        ands,
        asr,
        asrv,
        b,
        bfm,
        bic,
        bics,
        bif,
        bit,
        bl,
        blr,
        br,
        cbnz,
        cbz,
        ccmn,
        ccmp,
        clz,
        cmp,
        csel,
        csinc,
        csinv,
        csneg,
        drps,
        dup,
        eon,
        eor,
        eret,
        fabs,
        fadd,
        fcmp,
        fcmpe,
        fcsel,
        fcvt,
        fcvtms,
        fcvtps,
        fcvtzs,
        fcvtzu,
        fdiv,
        fmax,
        fmaxnm,
        fmin,
        fmov,
        fmul,
        fneg,
        fnmul,
        fsqrt,
        fsub,
        ins,
        ld3,
        ldp,
        ldpsw,
        ldr,
        ldrb,
        ldrh,
        ldrsb,
        ldrsh,
        ldrsw,
        ldur,
        ldurb,
        ldurh,
        ldursb,
        ldursh,
        ldursw,
        lsl,
        lslv,
        lsr,
        lsrv,
        madd,
        mneg,
        mov,
        movi,
        movk,
        movn,
        movz,
        msub,
        mul,
        mvn,
        nop,
        orn,
        orr,
        prfm,
        ret,
        rev16,
        ror,
        rorv,
        sbfiz,
        sbfm,
        scvtf,
        sdiv,
        sev,
        sevl,
        smaddl,
        smnegll,
        smsubl,
        smulh,
        smull,
        stp,
        str,
        strb,
        strh,
        stur,
        sturb,
        sturh,
        sub,
        subs,
        sxtb,
        sxth,
        sxtl,
        sxtw,
        sxtx,
        tbnz,
        tbz,
        test,
        ubfm,
        ucvtf,
        udiv,
        umaddl,
        umsubl,
        umulh,
        umull,
        ushll,
        ushll2,
        uxtb,
        uxth,
        uxtl,
        uxtl2,
        uxtw,
        uxtx,
        wfe,
        wfi,
        xtn,
        xtn2,
        yield,
        umlal,
        smax,
        smaxv,
        uaddw,

        cmeq,
        tbl,
        tbx,
        st1,
        addhn,
        ld4,
        ld2,
        addhn2,
        st2,
        st3,
        st4,
        not,
        fmadd,
        fmsub,
        fnmadd,
        fnmsub,
        sqdmulh,
        shrn,
        ld1r,
        sshll,
        brk,
        svc,
        hvc,
        dcps2,
        hlt,
        dcps1,
        dcps3,
        smc,
        dmb,
        hint,
        isb,
        dsb,
        mrs,
        msr,
        adc,
        adcs,
        sbc,
        sbcs,
        rbit,
        rev,
        cls,
        extr,
        ldxr,
        stxr,
        stlxr,
        stlxp,
        stxp,
        ldaxr,
        ldxp,
        ldaxp,
        stxrb,
        ext,
        aesd,
        aese,
        aesmc,
        aesimc,
        crc32b,
        crc32h,
        crc32w,
        rev32,
        crc32x,
        sha1h,
        sha1su1,
        sha256su0,
        sha1c,
        sha1p,
        sha1m,
        sha1su0,
        sha256h,
        sha256h2,
        sha256su1,
        stlr,
        stllr,
        stnp,
        ldnp,
        crc32cb,
        crc32ch,
        crc32cw,
        sshr,
        ldarh,
        ldarb,
        stlxrb,
        casp,
        caspl,
        ldaxrb,
        ldxrb,
        caspa,
        caspal,
        stllrb,
        stlrb,
        caspb,
        caspbl,
        ldlarb,
        casab,
        casalb,
        ldaxrh,
        stlrh,
        stllrh,
        uabd
    }
}
