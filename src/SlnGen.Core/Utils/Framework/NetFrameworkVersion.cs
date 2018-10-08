namespace SlnGen.Core.Utils
{
    public sealed class NetFrameworkVersion : NetPlatform
    {
        public static readonly NetFrameworkVersion v2_0 = new NetFrameworkVersion(".NET Framework 2.0", "v2.0", null);
        public static readonly NetFrameworkVersion v3_0 = new NetFrameworkVersion(".NET Framework 3.0", "v3.0", null);
        public static readonly NetFrameworkVersion v3_5 = new NetFrameworkVersion(".NET Framework 3.5", "v3.5", null);
        public static readonly NetFrameworkVersion v3_5_Client = new NetFrameworkVersion(".NET Framework 3.5 Client Profile", "v3.5", "Client");
        public static readonly NetFrameworkVersion v4 = new NetFrameworkVersion(".NET Framework 4", "v4.0", ".NETFramework,Version=v4.0");
        public static readonly NetFrameworkVersion v4_Client = new NetFrameworkVersion(".NET Framework 4 Client Profile", "v4.0", ".NETFramework,Version=v4.0,Profile=Client");
        public static readonly NetFrameworkVersion v4_5 = new NetFrameworkVersion(".NET Framework 4.5", "v4.0", ".NETFramework,Version=v4.5");
        public static readonly NetFrameworkVersion v4_5_1 = new NetFrameworkVersion(".NET Framework 4.5.1", "v4.0", ".NETFramework,Version=v4.5.1");
        public static readonly NetFrameworkVersion v4_5_2 = new NetFrameworkVersion(".NET Framework 4.5.2", "v4.0", ".NETFramework,Version=v4.5.2");
        public static readonly NetFrameworkVersion v4_6 = new NetFrameworkVersion(".NET Framework 4.6", "v4.0", ".NETFramework,Version=v4.6");
        public static readonly NetFrameworkVersion v4_6_1 = new NetFrameworkVersion(".NET Framework 4.6.1", "v4.0", ".NETFramework,Version=v4.6.1");
        public static readonly NetFrameworkVersion v4_7 = new NetFrameworkVersion(".NET Framework 4.7", "v4.0", ".NETFramework,Version=v4.7");
        public static readonly NetFrameworkVersion v4_7_1 = new NetFrameworkVersion(".NET Framework 4.7.1", "v4.0", ".NETFramework,Version=v4.7.1");

        public string TargetVersion { get; }
        public string SKU { get; }

        public NetFrameworkVersion(string name, string version, string sku) : base(name)
        {
            TargetVersion = version;
            SKU = sku;
        }
    }
}
