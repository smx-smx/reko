;;; Segment .interp (000100D4)
000100D4             2F 75 73 72 2F 6C 69 62 2F 6C 64 2E     /usr/lib/ld.
000100E0 73 6F 2E 31 00                                  so.1.          
;;; Segment .hash (000100E8)
000100E8                         00 00 00 35 00 00 00 3B         ...5...;
000100F0 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 33 ...............3
00010100 00 00 00 00 00 00 00 20 00 00 00 21 00 00 00 24 ....... ...!...$
00010110 00 00 00 38 00 00 00 27 00 00 00 28 00 00 00 29 ...8...'...(...)
00010120 00 00 00 2A 00 00 00 2B 00 00 00 2C 00 00 00 30 ...*...+...,...0
00010130 00 00 00 37 00 00 00 00 00 00 00 00 00 00 00 39 ...7...........9
00010140 00 00 00 00 00 00 00 23 00 00 00 3A 00 00 00 00 .......#...:....
00010150 00 00 00 00 00 00 00 36 00 00 00 00 00 00 00 2E .......6........
00010160 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ................
00010170 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 35 ...............5
00010180 00 00 00 00 00 00 00 00 00 00 00 2F 00 00 00 1A .........../....
00010190 00 00 00 1E 00 00 00 00 00 00 00 1B 00 00 00 00 ................
000101A0 00 00 00 1C 00 00 00 00 00 00 00 00 00 00 00 00 ................
000101B0 00 00 00 00 00 00 00 26 00 00 00 32 00 00 00 00 .......&...2....
000101C0 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ................
; ...
00010270 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 22 ..............."
00010280 00 00 00 00 00 00 00 2D 00 00 00 00 00 00 00 1D .......-........
00010290 00 00 00 00 00 00 00 00 00 00 00 34 00 00 00 00 ...........4....
000102A0 00 00 00 1F 00 00 00 25 00 00 00 31 00 00 00 00 .......%...1....
;;; Segment .dynsym (000102B0)
; 0000                                          00000000 00000000 00 
; 0001                                          000100D4 00000000 03 .interp
; 0002                                          000100E8 00000000 03 .hash
; 0003                                          000102B0 00000000 03 .dynsym
; 0004                                          00010660 00000000 03 .dynstr
; 0005                                          000108F0 00000000 03 .SUNW_version
; 0006                                          00010910 00000000 03 .rela.ex_shared
; 0007                                          00010928 00000000 03 .rela.plt
; 0008                                          00010958 00000000 03 .text
; 0009                                          00010C80 00000000 03 .init
; 000A                                          00010CB8 00000000 03 .fini
; 000B                                          00010CF0 00000000 03 .exception_ranges
; 000C                                          00010CF4 00000000 03 .rodata
; 000D                                          00010CF8 00000000 03 .rodata1
; 000E                                          00020D0C 00000000 03 .got
; 000F                                          00020D10 00000000 03 .plt
; 0010                                          00020D74 00000000 03 .dynamic
; 0011                                          00020E1C 00000000 03 .ex_shared
; 0012                                          00020E3C 00000000 03 .data
; 0013                                          00020E64 00000000 03 .bss
; 0014                                          00000000 00000000 03 .symtab
; 0015                                          00000000 00000000 03 .strtab
; 0016                                          00000000 00000000 03 .stab.index
; 0017                                          00000000 00000000 03 .comment
; 0018                                          00000000 00000000 03 .shstrtab
; 0019                                          00000000 00000000 03 .stab.indexstr
; 001A _start                                   00010958 000000F4 12 .text
; 001B _environ                                 00020E3C 00000004 11 .data
; 001C _end                                     00020E64 00000000 11 .bss
; 001D _ex_register                             00000000 00000000 20 
; 001E _GLOBAL_OFFSET_TABLE_                    00020D0C 00000000 11 .got
; 001F atexit                                   00020D40 00000000 12 
; 0020 exit                                     00020D4C 00000000 12 
; 0021 _init                                    00010C80 00000038 12 .init
; 0022 ___Argv                                  00020E60 00000004 11 .data
; 0023 _DYNAMIC                                 00020D74 00000000 11 .dynamic
; 0024 func1                                    00010A5C 00000008 12 .text
; 0025 func2                                    00010A74 00000008 12 .text
; 0026 printf                                   00020D64 00000000 12 
; 0027 func3                                    00010A8C 00000008 12 .text
; 0028 func4                                    00010AA4 00000008 12 .text
; 0029 func5                                    00010ABC 00000008 12 .text
; 002A func6                                    00010AD4 00000008 12 .text
; 002B func7                                    00010AEC 00000008 12 .text
; 002C func8                                    00010B04 00000008 12 .text
; 002D _exit                                    00020D58 00000000 12 
; 002E _ex_deregister                           00000000 00000000 20 
; 002F environ                                  00020E3C 00000004 21 .data
; 0030 __cg89_used                              00020E5C 00000004 11 .data
; 0031 __cg92_used                              00020E5C 00000004 11 .data
; 0032 __fnonstd_used                           00020E5C 00000004 11 .data
; 0033 _edata                                   00020E64 00000000 11 .data
; 0034 _PROCEDURE_LINKAGE_TABLE_                00020D10 00000000 11 .plt
; 0035 __fsr_init_value                         00000000 00000000 10 SHN_ABS
; 0036 _etext                                   00010D09 00000000 11 .rodata1
; 0037 _lib_version                             00010CF4 00000004 11 .rodata
; 0038 main                                     00010B0C 00000174 12 .text
; 0039 __environ_lock                           00020E40 00000018 11 .data
; 003A _fini                                    00010CB8 00000038 12 .fini
;;; Segment .dynstr (00010660)
00010660 00 5F 73 74 61 72 74 00 5F 65 6E 76 69 72 6F 6E ._start._environ
00010670 00 5F 65 6E 64 00 5F 65 78 5F 72 65 67 69 73 74 ._end._ex_regist
00010680 65 72 00 5F 47 4C 4F 42 41 4C 5F 4F 46 46 53 45 er._GLOBAL_OFFSE
00010690 54 5F 54 41 42 4C 45 5F 00 61 74 65 78 69 74 00 T_TABLE_.atexit.
000106A0 65 78 69 74 00 5F 69 6E 69 74 00 5F 5F 5F 41 72 exit._init.___Ar
000106B0 67 76 00 5F 44 59 4E 41 4D 49 43 00 66 75 6E 63 gv._DYNAMIC.func
000106C0 31 00 66 75 6E 63 32 00 70 72 69 6E 74 66 00 66 1.func2.printf.f
000106D0 75 6E 63 33 00 66 75 6E 63 34 00 66 75 6E 63 35 unc3.func4.func5
000106E0 00 66 75 6E 63 36 00 66 75 6E 63 37 00 66 75 6E .func6.func7.fun
000106F0 63 38 00 5F 65 78 69 74 00 5F 65 78 5F 64 65 72 c8._exit._ex_der
00010700 65 67 69 73 74 65 72 00 65 6E 76 69 72 6F 6E 00 egister.environ.
00010710 5F 5F 63 67 38 39 5F 75 73 65 64 00 5F 5F 63 67 __cg89_used.__cg
00010720 39 32 5F 75 73 65 64 00 5F 5F 66 6E 6F 6E 73 74 92_used.__fnonst
00010730 64 5F 75 73 65 64 00 5F 65 64 61 74 61 00 5F 50 d_used._edata._P
00010740 52 4F 43 45 44 55 52 45 5F 4C 49 4E 4B 41 47 45 ROCEDURE_LINKAGE
00010750 5F 54 41 42 4C 45 5F 00 5F 5F 66 73 72 5F 69 6E _TABLE_.__fsr_in
00010760 69 74 5F 76 61 6C 75 65 00 5F 65 74 65 78 74 00 it_value._etext.
00010770 5F 6C 69 62 5F 76 65 72 73 69 6F 6E 00 6D 61 69 _lib_version.mai
00010780 6E 00 5F 5F 65 6E 76 69 72 6F 6E 5F 6C 6F 63 6B n.__environ_lock
00010790 00 5F 66 69 6E 69 00 6C 69 62 63 2E 73 6F 2E 31 ._fini.libc.so.1
000107A0 00 53 59 53 56 41 42 49 5F 31 2E 33 00 6C 69 62 .SYSVABI_1.3.lib
000107B0 63 2E 73 6F 2E 31 00 00 00 00 00 00 00 00 00 00 c.so.1..........
000107C0 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ................
; ...
000108E0 00 00 00 00 00 00 00 00 00 00 00 00 00          .............  
;;; Segment .SUNW_version (000108F0)
000108F0 00 01 00 01 00 00 01 37 00 00 00 10 00 00 00 00 .......7........
00010900 05 37 CC B3 00 00 00 00 00 00 01 41 00 00 00 00 .7.........A....
;;; Segment .rela.ex_shared (00010910)
; 00020E20   3 0000002E 00000000 _ex_deregister (46)
; 00020E1C   3 0000001D 00000000 _ex_register (29)
;;; Segment .rela.plt (00010928)
; 00020D40  21 0000001F 00000000 atexit (31)
; 00020D4C  21 00000020 00000000 exit (32)
; 00020D58  21 0000002D 00000000 _exit (45)
; 00020D64  21 00000026 00000000 printf (38)
;;; Segment .text (00010958)

;; _start: 00010958
_start proc
	or	%g0,0x00000000,%i6
	ld	[%sp+64],%l0
	add	%sp,0x00000044,%l1
	sethi	0x00000083,%o1
	st	%l1,[%o1+608]
	sll	%l0,0x00000002,%l2
	add	%l2,0x00000004,%l2
	add	%l1,%l2,%l2
	sethi	0x00000083,%l3
	st	%l2,[%l3+572]
	sethi	0x00000000,%l5
	or	%l5,0x00000000,%l5
	orcc	%g0,%l5,%g0
	be	000109F8
	sethi	0x00000000,%g0

l00010994:
	sll	%l5,0x00000002,%l6
	and	%l6,0x00000300,%l7
	and	%l5,0x0000003F,%l6
	or	%l6,%l7,%l7
	sll	%l7,0x00000016,%l5
	sethi	0x00000083,%l4
	or	%l4,0x00000258,%l4
	stfsr	%fsr,[%l4+%g0]
	ld	[%l4+%g0],%l6
	sethi	0x000C0FFF,%l7
	or	%l7,0x000003FF,%l7
	and	%l6,%l7,%l6
	or	%l6,%l5,%l6
	st	%l6,[%l4+%g0]
	ldfsr	[%l4+%g0],%fsr
	sethi	0x00000000,%l5
	or	%l5,0x00000000,%l5
	and	%l5,0x0000003F,%l5
	subcc	%l5,0x00000035,%g0
	bne	000109F8
	sethi	0x00000000,%g0

l000109E8:
	sethi	0x00000083,%l5
	or	%l5,0x0000025C,%l5
	or	%g0,0x00000001,%l4
	st	%l4,[%l5+%g0]

l000109F8:
	sub	%sp,0x00000020,%sp
	orcc	%g0,%g1,%g0
	be	00010A10
	or	%g0,%g1,%o0

l00010A08:
	call	00020D40
	sethi	0x00000000,%g0

l00010A10:
	sethi	0x00000043,%o0
	call	00020D40
	or	%o0,0x000000B8,%o0
	call	00010C80
	sethi	0x00000000,%g0
	or	%g0,%l0,%o0
	or	%g0,%l1,%o1
	or	%g0,%l2,%o2
	or	%g0,%l3,%o3
	call	00010B0C
	sethi	0x00000000,%g0
	call	00020D4C
00010A40 01 00 00 00 40 00 40 C5 01 00 00 00 00 00 00 00 ....@.@.........
00010A50 00 00 00 00 00 00 00 00 00 00 00 00             ............   

;; func1: 00010A5C
func1 proc
	jmpl	%o7,8,%g0
	sethi	0x00000000,%g0
00010A64             00 00 00 00 00 00 00 00 00 00 00 00     ............
00010A70 00 00 00 00                                     ....           

;; func2: 00010A74
func2 proc
	jmpl	%o7,8,%g0
	sethi	0x00000000,%g0
00010A7C                                     00 00 00 00             ....
00010A80 00 00 00 00 00 00 00 00 00 00 00 00             ............   

;; func3: 00010A8C
func3 proc
	jmpl	%o7,8,%g0
	sethi	0x00000000,%g0
00010A94             00 00 00 00 00 00 00 00 00 00 00 00     ............
00010AA0 00 00 00 00                                     ....           

;; func4: 00010AA4
func4 proc
	jmpl	%o7,8,%g0
	sethi	0x00000000,%g0
00010AAC                                     00 00 00 00             ....
00010AB0 00 00 00 00 00 00 00 00 00 00 00 00             ............   

;; func5: 00010ABC
func5 proc
	jmpl	%o7,8,%g0
	sethi	0x00000000,%g0
00010AC4             00 00 00 00 00 00 00 00 00 00 00 00     ............
00010AD0 00 00 00 00                                     ....           

;; func6: 00010AD4
func6 proc
	jmpl	%o7,8,%g0
	sethi	0x00000000,%g0
00010ADC                                     00 00 00 00             ....
00010AE0 00 00 00 00 00 00 00 00 00 00 00 00             ............   

;; func7: 00010AEC
func7 proc
	jmpl	%o7,8,%g0
	sethi	0x00000000,%g0
00010AF4             00 00 00 00 00 00 00 00 00 00 00 00     ............
00010B00 00 00 00 00                                     ....           

;; func8: 00010B04
func8 proc
	jmpl	%o7,8,%g0
	sethi	0x00000000,%g0

;; main: 00010B0C
;;   Called from:
;;     00010A34 (in _start)
main proc
	save	%sp,0xFFFFFFA0,%sp
	or	%g0,0x00000001,%o0
	subcc	%i0,0x00000001,%g0
	ble,a	00010B20
	or	%g0,0x00000000,%o0

l00010B20:
	orcc	%g0,%o0,%g0
	be	00010B34
	sethi	0x00000042,%o0

l00010B2C:
	ba	00010B3C
	add	%o0,0x0000025C,%o0

l00010B34:
	sethi	0x00000042,%o0
	add	%o0,0x00000274,%o0

l00010B3C:
	be	00010B4C
	sethi	0x00000042,%o1

l00010B44:
	ba	00010B54
	add	%o1,0x0000028C,%o3

l00010B4C:
	sethi	0x00000042,%o1
	add	%o1,0x000002A4,%o3

l00010B54:
	be	00010B64
	sethi	0x00000042,%o1

l00010B5C:
	ba	00010B6C
	add	%o1,0x000002BC,%o2

l00010B64:
	sethi	0x00000042,%o1
	add	%o1,0x000002D4,%o2

l00010B6C:
	be	00010B7C
	sethi	0x00000042,%o1

l00010B74:
	ba	00010B84
	add	%o1,0x000002EC,%o1

l00010B7C:
	sethi	0x00000042,%o1
	add	%o1,0x00000304,%o1

l00010B84:
	be	00010BE8
	sethi	0x00000042,%o4

l00010B8C:
	add	%o4,0x0000025C,%o4
	subcc	%o0,%o4,%g0
	bne	00010BDC
	sethi	0x00000042,%o0

l00010B9C:
	add	%o0,0x0000028C,%o0
	subcc	%o3,%o0,%g0
	bne	00010BE0
	or	%g0,0x00000000,%i1

l00010BAC:
	sethi	0x00000042,%o0
	add	%o0,0x000002BC,%o0
	subcc	%o2,%o0,%g0
	bne	00010BE0
	or	%g0,0x00000000,%i1

l00010BC0:
	sethi	0x00000042,%o0
	add	%o0,0x000002EC,%o0
	subcc	%o1,%o0,%g0
	bne	00010BE0
	or	%g0,0x00000000,%i1

l00010BD4:
	ba	00010C40
	or	%g0,0x00000001,%i1

l00010BDC:
	or	%g0,0x00000000,%i1

l00010BE0:
	ba	00010C44
	subcc	%i1,0x00000000,%g0

l00010BE8:
	sethi	0x00000042,%o4
	add	%o4,0x00000274,%o4
	subcc	%o0,%o4,%g0
	bne	00010C3C
	sethi	0x00000042,%o0

l00010BFC:
	add	%o0,0x000002A4,%o0
	subcc	%o3,%o0,%g0
	bne	00010C3C
	sethi	0x00000000,%g0

l00010C0C:
	sethi	0x00000042,%o0
	add	%o0,0x000002D4,%o0
	subcc	%o2,%o0,%g0
	bne	00010C3C
	sethi	0x00000000,%g0

l00010C20:
	sethi	0x00000042,%o0
	add	%o0,0x00000304,%o0
	subcc	%o1,%o0,%g0
	bne	00010C3C
	sethi	0x00000000,%g0

l00010C34:
	ba	00010C40
	or	%g0,0x00000001,%i1

l00010C3C:
	or	%g0,0x00000000,%i1

l00010C40:
	subcc	%i1,0x00000000,%g0

l00010C44:
	be	00010C5C
	sethi	0x00000043,%g1

l00010C4C:
	call	00020D64
	add	%g1,0x000000F8,%o0
	ba	00010C6C
	or	%g0,0x00000000,%i0

l00010C5C:
	sethi	0x00000043,%g1
	call	00020D64
	add	%g1,0x00000100,%o0
	or	%g0,0x00000000,%i0

l00010C6C:
	subcc	%i1,0x00000000,%g0
	be,a	00010C78
	or	%g0,0x00000001,%i0

l00010C78:
	jmpl	%i7,8,%g0
	restore	%g0,%g0,%g0
;;; Segment .init (00010C80)

;; _init: 00010C80
;;   Called from:
;;     00010A1C (in _start)
_init proc
	save	%sp,0xFFFFFFA0,%sp
	call	00010C90
	sethi	0x00000000,%g0
	unimp

;; fn00010C90: 00010C90
;;   Called from:
;;     00010C84 (in _init)
fn00010C90 proc
	ld	[%o7+8],%o0
	add	%o7,%o0,%o0
	ld	[%o0-8],%l0
	subcc	%l0,%g0,%g0
	be	00010CB0
	sethi	0x00000000,%g0

l00010CA8:
	jmpl	%l0,%g0,%o7
	sethi	0x00000000,%g0

l00010CB0:
	jmpl	%i7,8,%g0
	restore	%g0,%g0,%g0
;;; Segment .fini (00010CB8)

;; _fini: 00010CB8
_fini proc
	save	%sp,0xFFFFFFA0,%sp
	call	00010CC8
	sethi	0x00000000,%g0
	unimp

;; fn00010CC8: 00010CC8
;;   Called from:
;;     00010CBC (in _fini)
fn00010CC8 proc
	ld	[%o7+8],%o0
	add	%o7,%o0,%o0
	ld	[%o0-4],%l0
	subcc	%l0,%g0,%g0
	be	00010CE8
	sethi	0x00000000,%g0

l00010CE0:
	jmpl	%l0,%g0,%o7
	sethi	0x00000000,%g0

l00010CE8:
	jmpl	%i7,8,%g0
	restore	%g0,%g0,%g0
;;; Segment .exception_ranges (00010CF0)
00010CF0 00 00 00 00                                     ....           
;;; Segment .rodata (00010CF4)
_lib_version		; 00010CF4
	dd	0x00000001
;;; Segment .rodata1 (00010CF8)
00010CF8                         50 61 73 73 0A 00 00 00         Pass....
00010D00 46 61 69 6C 65 64 21 0A 00                      Failed!..      
;;; Segment .got (00020D0C)
00020D0C                                     00 02 0D 74             ...t
;;; Segment .plt (00020D10)
00020D10 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ................
; ...
00020D40 03 00 00 30 30 BF FF F3 01 00 00 00 03 00 00 3C ...00..........<
00020D50 30 BF FF F0 01 00 00 00 03 00 00 48 30 BF FF ED 0..........H0...
00020D60 01 00 00 00 03 00 00 54 30 BF FF EA 01 00 00 00 .......T0.......
00020D70 01 00 00 00                                     ....           
;;; Segment .dynamic (00020D74)
; DT_NEEDED            libc.so.1
; DT_INIT              00010C80
; DT_DEBUG             00010CB8
; DT_VERNEED           000108F0
; DT_VERNEEDNUM               1
; DT_HASH              000100E8
; DT_STRTAB            00010660
; DT_STRSZ             0000028D
; DT_SYMTAB            000102B0
; DT_SYMENT                  16
; DT_DEBUG             00000000
; 6FFFFDFC             00000001
; DT_PLTGOT            00020D10
; DT_PLTRELSZ                48
; DT_PLTREL            00000007
; DT_JMPREL            00010928
; DT_RELA              00010910
; DT_RELASZ                  72
; DT_RELAENT                 12
;;; Segment .ex_shared (00020E1C)
00020E1C                                     00 00 00 00             ....
00020E20 00 00 00 00 00 00 00 00 00 00 00 00 FF FE FB 34 ...............4
00020E30 FF FE FE CC FF FE FE 5C FF FE FE CC             .......\....   
;;; Segment .data (00020E3C)
environ		; 00020E3C
	dd	0x00000000
__environ_lock		; 00020E40
	db	0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
	db	0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
00020E58                         00 00 00 00                     ....   
__fnonstd_used		; 00020E5C
	dd	0x00000000
___Argv		; 00020E60
	dd	0x00000000
