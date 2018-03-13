namespace SlnGen.Core
{
    public class NugetAssembly : AssemblyReference
    {
        public string Include { get; }

        public NugetAssembly(string name, string include, string hintPath, bool isPrivate) : base(name, hintPath, isPrivate)
        {
            Include = include;
        }
    }
}
