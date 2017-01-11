using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gee.External.Capstone;
using Gee.External.Capstone.PowerPc;
using Reko.Core;

namespace Reko.Arch.PowerPC
{
    public class PowerPcDisassembler : DisassemblerBase<PowerPcInstruction>
    {
        private ImageReader rdr;
        private IEnumerator<Instruction<Gee.External.Capstone.PowerPc.PowerPcInstruction, PowerPcRegister, PowerPcInstructionGroup, PowerPcInstructionDetail>> stream;

        public static PowerPcDisassembler Create32(ImageReader rdr)
        {
            return new PowerPcDisassembler(DisassembleMode.BigEndian | DisassembleMode.Bit32, rdr);
        }

        public static PowerPcDisassembler Create64(ImageReader rdr)
        {
            return new PowerPcDisassembler(DisassembleMode.BigEndian | DisassembleMode.Bit64, rdr);
        }

        public PowerPcDisassembler(DisassembleMode mode, ImageReader rdr)
        {
            this.rdr = rdr;
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                stream.Dispose();
            }
            base.Dispose(disposing);
        }

        public override PowerPcInstruction DisassembleInstruction()
        {
            if (!rdr.IsValid)
            {
                // Signal end of stream
                return null;
            }
            if (stream.MoveNext())
            {
                // Capstone doesn't actually use the imageReader, but apparently
                // Reko components peek at the reader, so we have to simulate motion.
                rdr.Offset += stream.Current.Bytes.Length;
                return (PowerPcInstruction)stream.Current;
            }
            else
            {
                // Create an empty shell instruction. Since its Id
                // property is blank and its Operands will be null,
                // it should be treated as invalid.
                return new PowerPcInstruction();
            }
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