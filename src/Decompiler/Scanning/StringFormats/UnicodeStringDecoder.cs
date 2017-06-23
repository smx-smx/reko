using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reko.Core;

namespace Reko.Scanning.StringFormats
{
    class UnicodeStringDecoder : StringDecoder
    {
        public UnicodeStringDecoder(byte[] data, StringScannerSettings settings = null) : base(data, settings) { }
        public UnicodeStringDecoder(ImageReader rdr, StringScannerSettings settings = null) : base(rdr, settings) { }

        public override string DecodeString()
        {
            List<byte> bytes = new List<byte>();
            byte ch;

            // Number of consecutive zeros
            int zeroCount = 0;
            // Total number of characters read
            int chCount = 0;

            while (true) {
                ch = Reader.ReadByte();
                chCount++;
                if (ch == 0x00) {
                    // Abort if we have an initial zero
                    // Or if we have an odd number of characters (c.c., so if we see a zero, we must have read a pair)
                    // Of if we have >= 2 zeros (null terminator is doubled in Unicode)
                    if (bytes.Count == 0)
                        return null;
                    if (chCount % 2 != 0 || ++zeroCount >= 2)
                        break;
                    // Skip zero characters
                    continue;
                // It's not a zero, but was it preceded by one?
                } else if(chCount > 1 && zeroCount < 1){
                    return null;
                } else {
                    zeroCount = 0;
                }

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
