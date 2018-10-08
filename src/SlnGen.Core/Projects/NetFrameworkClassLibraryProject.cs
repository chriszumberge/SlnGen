using SlnGen.Core.Utils;

namespace SlnGen.Core.Projects
{
    public class NetFrameworkClassLibraryProject : NetFrameworkProject
    {
        public NetFrameworkClassLibraryProject(string assemblyName, NetFrameworkPlatform targetFrameworkVersion) : base(assemblyName, "Library", targetFrameworkVersion)
        {
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Debug", "Any CPU"));
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Release", "Any CPU"));
        }
    }
}
