namespace SlnGen.Core.Projects
{
    public class NetFrameworkClassLibraryProject : Project
    {
        public NetFrameworkClassLibraryProject(string assemblyName, string targetFrameworkVersion) : base(assemblyName, "Library", targetFrameworkVersion)
        {
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Debug", "Any CPU"));
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Release", "Any CPU"));
        }
    }
}
