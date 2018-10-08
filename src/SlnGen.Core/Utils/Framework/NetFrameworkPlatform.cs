namespace SlnGen.Core.Utils
{
    public sealed class NetFrameworkPlatform : NetPlatform
    {
        public static readonly NetFrameworkPlatform v2_0 = new NetFrameworkPlatform(".NET Framework 2.0", "v2.0", "v2.0.50727", null);
        public static readonly NetFrameworkPlatform v3_0 = new NetFrameworkPlatform(".NET Framework 3.0", "v3.0", "v2.0.50727", null);
        public static readonly NetFrameworkPlatform v3_5 = new NetFrameworkPlatform(".NET Framework 3.5", "v3.5", "v2.0.50727", null);
        public static readonly NetFrameworkPlatform v3_5_Client = new NetFrameworkPlatform(".NET Framework 3.5 Client Profile", "v3.5", "v2.0.50727", "Client", "Client");
        public static readonly NetFrameworkPlatform v4 = new NetFrameworkPlatform(".NET Framework 4", "v4.0", "v4.0", ".NETFramework,Version=v4.0");
        public static readonly NetFrameworkPlatform v4_Client = new NetFrameworkPlatform(".NET Framework 4 Client Profile", "v4.0", "v4.0", ".NETFramework,Version=v4.0,Profile=Client", "Client");
        public static readonly NetFrameworkPlatform v4_5 = new NetFrameworkPlatform(".NET Framework 4.5", "v4.5", "v4.0", ".NETFramework,Version=v4.5");
        public static readonly NetFrameworkPlatform v4_5_1 = new NetFrameworkPlatform(".NET Framework 4.5.1", "v4.5.1", "v4.0", ".NETFramework,Version=v4.5.1");
        public static readonly NetFrameworkPlatform v4_5_2 = new NetFrameworkPlatform(".NET Framework 4.5.2", "v4.5.2", "v4.0", ".NETFramework,Version=v4.5.2");
        public static readonly NetFrameworkPlatform v4_6 = new NetFrameworkPlatform(".NET Framework 4.6", "4.6", "v4.0", ".NETFramework,Version=v4.6");
        public static readonly NetFrameworkPlatform v4_6_1 = new NetFrameworkPlatform(".NET Framework 4.6.1", "v4.6.1", "v4.0", ".NETFramework,Version=v4.6.1");
        public static readonly NetFrameworkPlatform v4_7 = new NetFrameworkPlatform(".NET Framework 4.7", "v4.7", "v4.0", ".NETFramework,Version=v4.7");
        public static readonly NetFrameworkPlatform v4_7_1 = new NetFrameworkPlatform(".NET Framework 4.7.1", "v4.7.1", "v4.0", ".NETFramework,Version=v4.7.1");

        public string RuntimeVersion { get; }
        public string SKU { get; }
        public string Profile { get; }

        public NetFrameworkPlatform(string name, string version, string runtimeVersion, string sku, string profile = null) : base(name, version)
        {
            RuntimeVersion = runtimeVersion;
            SKU = sku;
            Profile = profile;
        }
    }
}
