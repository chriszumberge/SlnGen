using SlnGen.Core.Utils;

namespace SlnGen.Core.Projects
{
    public class NetStandardClassLibraryProject : NetStandardProject
    {
        public NetStandardClassLibraryProject(string assemblyName, NetStandardPlatform targetFrameworkVersion, string rootNamespace = "") : 
            base(assemblyName, "Library", targetFrameworkVersion, rootNamespace)
        {
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Debug", "Any CPU"));
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Release", "Any CPU"));
        }
    }
}
