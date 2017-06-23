using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reko.Core
{
    public class ProgramStringAddress : ProgramAddress
    {
        public readonly string StringData;
        public ProgramStringAddress(Program program, Address addr, string stringData) : base(program, addr, AddressType.String)
        {
            StringData = stringData;
        }
    }
}
