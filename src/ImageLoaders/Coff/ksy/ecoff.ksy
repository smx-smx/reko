meta:
  id: ecoff
  file-extension: ecoff
  endian: be

enums:
  section_flags:
    0x0: styp_reg
    0x20: styp_text
    0x40: styp_data
    0x80: styp_bss
    0x100: styp_rdata
    0x200: styp_sdata
    0x400: styp_sbss
    0x800: styp_ucode
    0x1000: styp_got
    0x2000: styp_dynamic
    0x4000: styp_dynsym
    0x8000: styp_rel_dyn
    0x10000: styp_dynstr
    0x20000: styp_hash
    0x40000: styp_dsolist
    0x80000: styp_msym
    0x100000: styp_conflict
    0x01000000: styp_fini
    0x02000000: styp_comment
    0x02200000: styp_rconst
    0x02400000: styp_xdata
    0x02500000: styp_tlsdata
    0x02600000: styp_tlsbss
    0x02700000: styp_tlsinit
    0x02800000: styp_pdata
    0x04000000: styp_lita
    0x08000000: styp_lit8
    0x0ff00000: styp_extmask
    0x10000000: styp_lit4
    0x20000000: s_nreloc_ovfl
    0x40000000: styp_ecoff_lib
    0x80000000: styp_init
types:
  filehdr:
    seq:
    - id: f_magic
      type: u2
    - id: f_nscns
      type: u2
    - id: f_timdat
      type: u4
    - id: f_symptr
      type: u4
    - id: f_nsyms
      type: u4
    - id: f_opthdr
      type: u2
    - id: f_flags
      type: u2
  aouthdr:
    seq:
      - id: magic
        type: u2
      - id: vstamp
        type: u2
      - id: tsize
        type: u4
      - id: dsize
        type: u4
      - id: bsize
        type: u4
      - id: entry
        type: u4
      - id: text_start
        type: u4
      - id: data_start
        type: u4
      - id: bss_start
        type: u4
      - id: gprmask
        type: u4
      - id: cprmask
        type: u4
        repeat: expr
        repeat-expr: 4
      - id: gp_value
        type: u4
  scnhdr:
    seq:
      - id: s_name
        type: str
        encoding: ASCII
        size: 8
      - id: s_paddr
        type: u4
      - id: s_vaddr
        type: u4
      - id: s_size
        type: u4
      - id: s_scnptr
        type: u4
      - id: s_relptr
        type: u4
      - id: s_lnnoptr
        type: u4
      - id: s_nreloc
        type: u2
      - id: s_nlnno
        type: u2
      - id: s_flags
        type: u4
        enum: section_flags
    instances:
      data:
        pos: s_scnptr
        size: s_size

seq:
  - id: file_header
    type: filehdr
  - id: aout_header
    type: aouthdr
    size: file_header.f_opthdr
  - id: sections
    type: scnhdr
    repeat: expr
    repeat-expr: file_header.f_nscns