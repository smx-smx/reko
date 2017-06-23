using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reko.Core;

namespace Reko.Scanning.StringFormats
{
    class CStringDecoder : StringDecoder
    {
        public CStringDecoder(byte[] data, StringScannerSettings settings = null) : base(data, settings) { }
        public CStringDecoder(ImageReader rdr, StringScannerSettings settings = null) : base(rdr, settings) { }

        public override string DecodeString()
        {
            List<byte> bytes = new List<byte>();
            byte ch;
            while((ch = Reader.ReadByte()) != 0x00) {
                if (Settings.Flags.HasFlag(StringScannerFlags.IsPrintable) && !IsPrintableCharacter((char)ch))
                    return null;

                bytes.Add(ch);
            }

            byte[] data = bytes.ToArray();
            string result;

            if (Settings.Flags.HasFlag(StringScannerFlags.StrictEncoding)) {
                Encoding e = Settings.GetEncoding();
                result = e.GetString(data);
                if (Settings.Flags.HasFlag(StringScannerFlags.MatchEncoding) && !BytesMatchEncoding(data, e))
                    return null;
            } else {
                result = Encoding.Default.GetString(data);
            }

            if (Settings.Flags.HasFlag(StringScannerFlags.MinimumLength) && result.Length < Settings.MinimumLength)
                return null;

            if (Settings.Flags.HasFlag(StringScannerFlags.MaximumLength) && result.Length > Settings.MaximumLength)
                return null;

            return result;
        }
    }
}
