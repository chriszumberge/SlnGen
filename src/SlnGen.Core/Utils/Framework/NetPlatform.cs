namespace SlnGen.Core.Utils
{
    public abstract class NetPlatform
    {
        public string Name { get; }
        public string TargetVersion { get; }

        protected NetPlatform(string name, string targetVersion)
        {
            Name = name;
            TargetVersion = targetVersion;
        }
        public override string ToString()
        {
            return Name;
        }

        //public static implicit operator string(NetPlatform platform) => platform.ToString();
    }
}
