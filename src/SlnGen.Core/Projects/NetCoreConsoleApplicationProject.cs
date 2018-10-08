using SlnGen.Core.Utils;

namespace SlnGen.Core.Projects
{
    public class NetCoreConsoleApplicationProject : NetCoreProject
    {
        public NetCoreConsoleApplicationProject(string assemblyName, NetCorePlatform targetFrameworkVersion) : base(assemblyName, "Exe", targetFrameworkVersion)
        {
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Debug", "Any CPU"));
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Release", "Any CPU"));
        }
    }
}
