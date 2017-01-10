using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gee.External.Capstone;
using Gee.External.Capstone.PowerPc;
using Reko.Core;

namespace Reko.Arch.PowerPC {
    public class PowerPcDisassembler : DisassemblerBase<PowerPcInstruction>
    {
        private IEnumerator<Instruction<Gee.External.Capstone.PowerPc.PowerPcInstruction, PowerPcRegister, PowerPcInstructionGroup, PowerPcInstructionDetail>> stream;

        public static PowerPcDisassembler Create32(ImageReader rdr)
        {
            //$sxm: todo!
            throw new NotImplementedException();
        }

        public static PowerPcDisassembler Create64(ImageReader rdr)
        {
            //$sxm: todo!
            throw new NotImplementedException();
        }

        public PowerPcDisassembler(DisassembleMode mode, ImageReader rdr) {
			var dasm = new InternalDisassembler(mode);
			dasm.EnableDetails = true;
			this.stream = dasm.DisassembleStream(
				rdr.Bytes,
				(int)rdr.Offset,
				(long)rdr.Address.ToLinear() - rdr.Offset)
				.GetEnumerator();
		}

		public PowerPcDisassembler(PowerPcArchitecture32 arch, ImageReader rdr)
			: this(DisassembleMode.Bit32 | DisassembleMode.BigEndian, rdr) { }

		public PowerPcDisassembler(PowerPcArchitecture64 arch, ImageReader rdr)
			: this(DisassembleMode.Bit64 | DisassembleMode.BigEndian, rdr) { }

		protected override void Dispose(bool disposing) {
			if (disposing) {
				stream.Dispose();
			}
			base.Dispose(disposing);
		}

		public override PowerPcInstruction DisassembleInstruction() {
			if (stream.MoveNext()) {
				return (PowerPcInstruction) stream.Current;
			} else
				return null;
		}

        private class InternalDisassembler : CapstonePowerPcDisassembler
        {
            public InternalDisassembler(DisassembleMode mode) : base(mode)
            {
            }

            public override Instruction<Gee.External.Capstone.PowerPc.PowerPcInstruction, PowerPcRegister, PowerPcInstructionGroup, PowerPcInstructionDetail> CreateManagedInstruction()
            {
                return new PowerPcInstruction();
            }
        }
	}
}