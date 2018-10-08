namespace SlnGen.Core.Utils
{
    public sealed class NetCorePlatform : NetPlatform
    {
        public static readonly NetCorePlatform v1_0 = new NetCorePlatform(".NET Core 1.0", "netcoreapp1.0");
        public static readonly NetCorePlatform v2_0 = new NetCorePlatform(".NET Core 2.0", "netcoreapp2.1");

        private NetCorePlatform(string name, string targetFramework) : base(name, targetFramework){ }
    }
}
