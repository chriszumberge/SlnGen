namespace SlnGen.Core.Utils
{
    public sealed class NetCorePlatform : NetPlatform
    {
        //public static readonly NetCoreVersion v1_0 = new NetCoreVersion("v1.0");
        public static readonly NetCorePlatform v2_0 = new NetCorePlatform(".NET Core 2.1", "netcoreapp2.1");

        public string TargetFramework { get; }
        private NetCorePlatform(string name, string targetFramework) : base(name, targetFramework)
        {
            TargetFramework = targetFramework;
        }
    }
}
