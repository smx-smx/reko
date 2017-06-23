using Reko.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reko.Scanning.StringFormats
{
    public abstract class StringDecoder
    {
        public ImageReader Reader { get; private set; }
        public StringScannerSettings Settings { get; private set; }

        public static bool BytesMatchEncoding(byte[] data, Encoding encoding)
        {
            //decode and reencode string
            byte[] encodedBytes = encoding.GetBytes(encoding.GetString(data));
            return data.SequenceEqual(encodedBytes);
        }

        public bool IsPrintableCharacter(char ch)
        {
            return (' ' <= ch && ch < 0x7F);
        }

        private void DefaultStringScannerSettings()
        {
            if(Settings == null) {
                Settings = new StringScannerSettings {
                    Encoding = StringEncoding.ASCII,
                    Flags = StringScannerFlags.IsPrintable | StringScannerFlags.StrictEncoding | StringScannerFlags.MatchEncoding
                };
            }
        }

        public StringDecoder(ImageReader rdr, StringScannerSettings settings = null)
        {
            Reader = rdr;
            if (settings == null)
                DefaultStringScannerSettings();
        }

        public StringDecoder(byte[] data, StringScannerSettings settings = null) : this(new ImageReader(data), settings)
        {
        }

        public abstract string DecodeString();

        public bool TryDecodeString(out string decoded)
        {
            try {
                decoded = DecodeString();
                return decoded != null;
            } catch (Exception) {
                decoded = null;
                return false;
            }
        }
    }
}
