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
        StrictEncoding = 1 << 3
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
        public StringScannerFlags Flags { get; private set; }
        public Encoding Encoding { get; private set; }
        public int MinimumLength { get; private set; }
        public int MaximumLength { get; private set; }

        public StringScannerSettings(
            Encoding encoding = null,
            StringScannerFlags flags = StringScannerFlags.IsPrintable,
            int minimumLength = 0,
            int maximumLength = 0
        ){
            this.Encoding = (encoding == null) ? Encoding.Default : encoding;
            this.Flags = flags;
            this.MinimumLength = minimumLength;
            this.MaximumLength = maximumLength;
        }
    }
}