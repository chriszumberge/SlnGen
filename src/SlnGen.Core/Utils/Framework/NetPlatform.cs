namespace SlnGen.Core.Utils
{
    public abstract class NetPlatform
    {
        string _name;
        protected NetPlatform(string name)
        {
            _name = name;
        }
        public override string ToString()
        {
            return _name;
        }

        //public static implicit operator string(NetPlatform platform) => platform.ToString();
    }
}
