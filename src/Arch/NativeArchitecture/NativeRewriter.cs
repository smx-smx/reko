using Reko.Core;
using Reko.Core.Rtl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reko.Arch.NativeArchitecture
{
    public class NativeRewriter : IEnumerable<RtlInstructionCluster>
    {
        private NativeProcessorArchitecture nativeArchitecture;
        private EndianImageReader rdr;
        private NativeProcessorState state;
        private IStorageBinder frame;
        private IRewriterHost host;

        public NativeRewriter(NativeProcessorArchitecture nativeArchitecture, EndianImageReader rdr, NativeProcessorState state, IStorageBinder frame, IRewriterHost host)
        {
            this.nativeArchitecture = nativeArchitecture;
            this.rdr = rdr;
            this.state = state;
            this.frame = frame;
            this.host = host;
        }

        public IEnumerator<RtlInstructionCluster> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
