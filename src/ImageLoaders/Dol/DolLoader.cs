#region License
/* 
 * Copyright (C) 1999-2017 John Källén.
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2, or (at your option)
 * any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; see the file COPYING.  If not, write to
 * the Free Software Foundation, 675 Mass Ave, Cambridge, MA 02139, USA.
 */
#endregion

using Reko.Core;
using Reko.Core.Configuration;
using Reko.Environments.Wii;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Reko.ImageLoaders.Dol
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	[Endian(Endianness.BigEndian)]
	public struct DolStructure {
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
		public UInt32[] offsetText;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
		public UInt32[] offsetData;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
		public UInt32[] addressText;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
		public UInt32[] addressData;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
		public UInt32[] sizeText;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
		public UInt32[] sizeData;
		public UInt32 addressBSS;
		public UInt32 sizeBSS;
		public UInt32 entrypoint;
	}

	public class DolHeader {
		public UInt32[] offsetText = new UInt32[7];
		public UInt32[] offsetData = new UInt32[11];
		public Address32[] addressText = new Address32[7];
		public Address32[] addressData = new Address32[11];
		public UInt32[] sizeText = new UInt32[7];
		public UInt32[] sizeData = new UInt32[11];
		public Address32 addressBSS;
		public UInt32 sizeBSS;
		public Address32 entrypoint;

		public DolHeader(DolStructure hdr) {
			this.offsetText = hdr.offsetText;
			this.offsetData = hdr.offsetData;
            this.sizeText = hdr.sizeText;
            this.sizeData = hdr.sizeData;
			this.sizeText = hdr.sizeText;
			this.sizeBSS = hdr.sizeBSS;

			for (int i = 0; i < 7; i++) {
				this.addressText[i] = new Address32(hdr.addressText[i]);
			}
			for (int i = 0; i < 11; i++) {
				this.addressData[i] = new Address32(hdr.addressData[i]);
			}
			this.addressBSS = new Address32(hdr.addressBSS);
			this.entrypoint = new Address32(hdr.entrypoint);
		}
	}

	/* Adapted from https://github.com/heinermann/ida-wii-loaders */
	/* Format Reference: http://wiibrew.org/wiki/DOL */
	/// <summary>
	/// Image loader for Nintendo DOL file format.
	/// </summary>
	public class DolLoader : ImageLoader {
		private DolHeader hdr;

		public DolLoader(IServiceProvider services, string filename, byte[] imgRaw) : base(services, filename, imgRaw) {
		}

		public override Address PreferredBaseAddress {
			get {
				return this.hdr.entrypoint;
			}
			set {
				throw new NotImplementedException();
			}
		}

		public override Program Load(Address addrLoad) {
			var cfgSvc = Services.RequireService<IConfigurationService>();
			var arch = cfgSvc.GetArchitecture("ppc");
			var platform = new WiiPlatform(Services, arch);
			return Load(addrLoad, arch, platform);
		}

		public override Program Load(Address addrLoad, IProcessorArchitecture arch, IPlatform platform)
        {
            BeImageReader rdr = new BeImageReader(this.RawImage, 0);
            try
            {
                this.hdr = new DolHeader(new StructureReader<DolStructure>(rdr).Read());
            }
            catch (Exception ex)
            {
                throw new BadImageFormatException("Invalid DOL header.", ex);
            }

            SegmentMap segmentMap = BuildSegmentMap(addrLoad, this.hdr);

            var entryPoint = new ImageSymbol(this.hdr.entrypoint) { Type = SymbolType.Procedure };
            var program = new Program(
                segmentMap,
                arch,
                platform)
            {
                ImageSymbols = { { this.hdr.entrypoint, entryPoint } },
                EntryPoints = { { this.hdr.entrypoint, entryPoint } }
            };
            return program;
        }

        public SegmentMap BuildSegmentMap(Address addrLoad, DolHeader header)
        {
            var segments = new List<ImageSegment>();

            // Create code segments. They are named "Text1", "Text2"... according to
            // http://wiibrew.org/wiki/DOL
            for (uint i = 0; i < 7; i++)
            {
                if (header.addressText[i] == Address32.NULL)
                    continue;
                var seg = LoadSegment(
                    string.Format("Text{0}", i+1),
                    header.offsetText[i],
                    header.sizeText[i],
                    header.addressText[i],
                    AccessMode.ReadExecute);
                segments.Add(seg);
            }

            // Create all data segments, named "Data0", "Data1"...
            for (uint i = 0; i < 11; i++)
            {
                if (header.addressData[i] == Address32.NULL)
                    continue;
                var seg = LoadSegment(
                     string.Format("Data{0}", i),
                     header.offsetData[i],
                     header.sizeData[i],
                     header.addressData[i],
                     AccessMode.ReadWrite);
                segments.Add(seg);
            }

            if (header.addressBSS != Address32.NULL)
            {
                var mem = new MemoryArea(header.addressBSS, new byte[header.sizeBSS]);
                var seg = new ImageSegment(".bss", header.addressBSS, mem, AccessMode.ReadWrite);
                seg.Size = header.sizeBSS;
                segments.Add(seg);
            }
            var segmentMap = new SegmentMap(addrLoad, segments.ToArray());
            return segmentMap;
        }

        /// <summary>
        /// Loads a segment from the image file into a MemoryArea at the correct place in
        /// memory.
        /// </summary>
        private ImageSegment LoadSegment(
            string name, 
            uint offset, 
            uint size,
            Address addr,
            AccessMode access)
        {
            var bytes = new byte[size];
            Array.Copy(RawImage, (int)offset, bytes, 0, size);
            var mem = new MemoryArea(addr, bytes);
            return new ImageSegment(name, addr, mem, access) { Size = size };
        }

        public override RelocationResults Relocate(Program program, Address addrLoad) {
			throw new NotImplementedException();
		}
	}
}
