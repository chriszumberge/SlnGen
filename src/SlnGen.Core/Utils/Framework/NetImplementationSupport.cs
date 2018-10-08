using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen.Core.Utils
{
    // https://docs.microsoft.com/en-us/dotnet/standard/net-standard#net-implementation-support
    public static class NetImplementationSupport
    {
        static List<NetImplementation> s_implementations = new List<NetImplementation>
        {
            new NetImplementation{ NetStandard = NetStandardPlatform.v1_0, NetCore = NetCorePlatform.v1_0, NetFramework = NetFrameworkPlatform.v4_5,
                                   XamarinAndroidPlatform = XamarinAndroidPlatform.v7_0},
            new NetImplementation{ NetStandard = NetStandardPlatform.v1_1, NetCore = NetCorePlatform.v1_0, NetFramework = NetFrameworkPlatform.v4_5,
                                   XamarinAndroidPlatform = XamarinAndroidPlatform.v7_0},
            new NetImplementation{ NetStandard = NetStandardPlatform.v1_2, NetCore = NetCorePlatform.v1_0, NetFramework = NetFrameworkPlatform.v4_5_1,
                                   XamarinAndroidPlatform = XamarinAndroidPlatform.v7_0},
            new NetImplementation{ NetStandard = NetStandardPlatform.v1_3, NetCore = NetCorePlatform.v1_0, NetFramework = NetFrameworkPlatform.v4_6,
                                   XamarinAndroidPlatform = XamarinAndroidPlatform.v7_0},
            new NetImplementation{ NetStandard = NetStandardPlatform.v1_4, NetCore = NetCorePlatform.v1_0, NetFramework = NetFrameworkPlatform.v4_6_1,
                                   XamarinAndroidPlatform = XamarinAndroidPlatform.v7_0},
            new NetImplementation{ NetStandard = NetStandardPlatform.v1_5, NetCore = NetCorePlatform.v1_0, NetFramework = NetFrameworkPlatform.v4_6_1,
                                   XamarinAndroidPlatform = XamarinAndroidPlatform.v7_0},
            new NetImplementation{ NetStandard = NetStandardPlatform.v1_6, NetCore = NetCorePlatform.v1_0, NetFramework = NetFrameworkPlatform.v4_6_1,
                                   XamarinAndroidPlatform = XamarinAndroidPlatform.v7_0},
            new NetImplementation{ NetStandard = NetStandardPlatform.v2_0, NetCore = NetCorePlatform.v2_0, NetFramework = NetFrameworkPlatform.v4_6_1,
                                   XamarinAndroidPlatform = XamarinAndroidPlatform.v8_0},
        };

        public static NetImplementation GetMinCompatibleWith<T>(T targetNetPlatform) where T : NetPlatform
        {
            return GetAllCompatibleWith(targetNetPlatform).FirstOrDefault();
        }

        public static NetImplementation GetMaxCompatibleWith<T>(T targetNetPlatform) where T : NetPlatform
        {
            return GetAllCompatibleWith(targetNetPlatform).LastOrDefault();
        }

        public static List<NetImplementation> GetAllCompatibleWith<T>(T targetNetPlatform) where T : NetPlatform
        {
            if (typeof(T) == typeof(NetStandardPlatform))
            {
                return s_implementations.Where(x => x.NetStandard == targetNetPlatform).ToList();
            }
            else if (typeof(T) == typeof(NetCorePlatform))
            {
                return s_implementations.Where(x => x.NetCore == targetNetPlatform).ToList();
            }
            else if (typeof(T) == typeof(NetFrameworkPlatform))
            {
                return s_implementations.Where(x => x.NetFramework == targetNetPlatform).ToList();
            }
            else if (typeof(T) == typeof(XamarinAndroidPlatform))
            {
                return s_implementations.Where(x => x.XamarinAndroidPlatform == targetNetPlatform).ToList();
            }
            else
            {
                return new List<NetImplementation>();
            }
        }
    }

    public class NetImplementation
    {
        public NetStandardPlatform NetStandard { get; set; }
        public NetCorePlatform NetCore { get; set; }
        public NetFrameworkPlatform NetFramework { get; set; }
        public XamarinAndroidPlatform XamarinAndroidPlatform { get; set; }
    }
}
