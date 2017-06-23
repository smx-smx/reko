using System;
using System.Text;

namespace Reko.Scanning.StringFormats
{
    [Flags]
    public enum StringScannerFlags
    {
        IsPrintable = 1 << 0,
        MinimumLength = 1 << 1,
        MaximumLength = 1 << 2,
        StrictEncoding = 1 << 3,
        MatchEncoding = 1 << 4
    }

    public enum StringEncoding
    {
        ASCII = 0,
        UTF8,
        Unicode
    }

    public enum StringFormat
    {
        CStyle = 0,
        BSTR = 1
    }

    public class StringScannerSettings
    {
        public StringScannerFlags Flags;
        public StringEncoding Encoding;
        public int MinimumLength;
        public int MaximumLength;

        public Encoding GetEncoding()
        {
            string encodingName = Enum.GetName(this.Encoding.GetType(), this.Encoding);
            switch (encodingName) {
                case "ASCII":
                    return System.Text.Encoding.ASCII;
                case "UTF8":
                    return System.Text.Encoding.UTF8;
                case "Unicode":
                    return System.Text.Encoding.Unicode;
                default:
                    throw new NotSupportedException(string.Format("Encoding {0} not supported yet", encodingName));
            }
        }
    }
}