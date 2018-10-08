using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen.Core.Utils
{
    public sealed class NetCoreVersion : NetPlatform
    {
        public static readonly NetCoreVersion v1_0 = new NetCoreVersion("v1.0");
        public static readonly NetCoreVersion v2_0 = new NetCoreVersion("v2.0");


        private NetCoreVersion(string name) : base(name) { }
    }
}
