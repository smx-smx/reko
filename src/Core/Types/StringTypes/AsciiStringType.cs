using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reko.Core.Types.StringTypes
{
    public class AsciiStringType : StringType
    {
        public static new Encoding Encoding {
            get {
                return Encoding.ASCII;
            }
        }

        public AsciiStringType(PrimitiveType lengthPrefixType = null, int prefixOffset = 0) : base(
            PrimitiveType.Char, lengthPrefixType, prefixOffset, AsciiStringType.Encoding
        ){
        }
    }
}
