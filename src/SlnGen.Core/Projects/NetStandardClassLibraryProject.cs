using SlnGen.Core.Utils;

namespace SlnGen.Core.Projects
{
    public class NetStandardClassLibraryProject : NetStandardProject
    {
        public NetStandardClassLibraryProject(string assemblyName, NetStandardPlatform targetFrameworkVersion) : base(assemblyName, "Library", targetFrameworkVersion)
        {
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Debug", "Any CPU"));
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Release", "Any CPU"));
        }
    }
}
