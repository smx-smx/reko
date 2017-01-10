namespace Reko.Core.Machine
{
    public interface MachineInstruction
    {
        /// <summary>
        /// The address at which the instruction begins.
        /// </summary>
        Address Address { get; }

        /// <summary>
        /// The length of the entire instruction. Some architectures, e.g. M68k, x86, and most
        /// 8-bit microprocessors, have variable length instructions.
        /// </summary>
        int Length { get; }

        InstructionClass InstructionClass { get; }

        /// <summary>
        /// Returns true if the instruction is valid.
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Some analyses benefit from having access to opcodes.
        /// </summary>
        int OpcodeAsInteger { get; }

        /// <summary>
        /// Returns true if the Address is contained inside this instruction.
        /// </summary>
        /// <param name="addr"></param>
        bool Contains(Address addr);

        /// <summary>
        /// Returns the 'i'th instruction operand.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        MachineOperand GetOperand(int i);

        /// <summary>
        /// Renders the instruction to a Reko machine instruction writer.
        /// </summary>
        /// <param name="writer"></param>
        void Render(MachineInstructionWriter writer);

        /// <summary>
        /// Renders the instruction in a platform-specific way.
        /// </summary>
        /// <param name="platform"></param>
        /// <returns></returns>
        string ToString(IPlatform platform);
    }
}