meta:
  id: ecoff
  file-extension: ecoff
  endian: be

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
    instances:
      data:
        pos: s_scnptr
        size: s_size
seq:
  - id: filehdr
    type: filehdr
  - id: aouthdr
    type: aouthdr
    size: filehdr.f_opthdr
  - id: sections
    type: scnhdr
    repeat: expr
    repeat-expr: filehdr.f_nscns