using SlnGen.Core.Utils;

namespace SlnGen.Core.Projects
{
    public class NetCoreConsoleApplicationProject : NetCoreProject
    {
        public NetCoreConsoleApplicationProject(string assemblyName, NetCorePlatform targetFrameworkVersion, string rootNamespace = "") : 
            base(assemblyName, "Exe", targetFrameworkVersion, rootNamespace)
        {
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Debug", "Any CPU"));
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Release", "Any CPU"));
        }
    }
}
