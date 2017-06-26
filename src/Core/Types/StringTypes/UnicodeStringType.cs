using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reko.Core.Types.StringTypes
{
    public class UnicodeStringType : StringType
    {
        public static new Encoding Encoding {
            get {
                return Encoding.Unicode;
            }
        }

        public UnicodeStringType(PrimitiveType lengthPrefixType = null, int prefixOffset = 0) : base(
            PrimitiveType.WChar, lengthPrefixType, prefixOffset, UnicodeStringType.Encoding
        ){
        }
    }
}
