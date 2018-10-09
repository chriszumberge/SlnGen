using SlnGen.Core.Utils;

namespace SlnGen.Core.Projects
{
    public class NetFrameworkClassLibraryProject : NetFrameworkProject
    {
        public NetFrameworkClassLibraryProject(string assemblyName, NetFrameworkPlatform targetFrameworkVersion, string rootNamespace = "") : 
            base(assemblyName, "Library", targetFrameworkVersion, rootNamespace)
        {
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Debug", "Any CPU"));
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Release", "Any CPU"));
        }
    }
}
