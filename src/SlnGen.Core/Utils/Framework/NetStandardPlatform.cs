namespace SlnGen.Core.Utils
{
    public sealed class NetStandardPlatform : NetPlatform
    {
        public static readonly NetStandardPlatform v1_0 = new NetStandardPlatform(".NET Standard 1.0", "netstandard1.0");
        public static readonly NetStandardPlatform v1_1 = new NetStandardPlatform(".NET Standard 1.1", "netstandard1.1");
        public static readonly NetStandardPlatform v1_2 = new NetStandardPlatform(".NET Standard 1.2", "netstandard1.2");
        public static readonly NetStandardPlatform v1_3 = new NetStandardPlatform(".NET Standard 1.3", "netstandard1.3");
        public static readonly NetStandardPlatform v1_4 = new NetStandardPlatform(".NET Standard 1.4", "netstandard1.4");
        public static readonly NetStandardPlatform v1_5 = new NetStandardPlatform(".NET Standard 1.5", "netstandard1.5");
        public static readonly NetStandardPlatform v1_6 = new NetStandardPlatform(".NET Standard 1.6", "netstandard1.6");
        public static readonly NetStandardPlatform v2_0 = new NetStandardPlatform(".NET Standard 2.0", "netstandard2.0");

        private NetStandardPlatform(string name, string targetFramework) : base(name, targetFramework) { }
    }
}
