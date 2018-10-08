using SlnGen.Core;
using SlnGen.Core.Projects;
using SlnGen.Core.Utils;
using SlnGen.Xamarin.Files;

namespace SlnGen.Xamarin.Projects
{
    public class NetStandardXamarinClassLibraryProject : NetStandardClassLibraryProject
    {
        public NetStandardXamarinClassLibraryProject(string assemblyName, NetStandardPlatform targetFrameworkVersion, NugetPackage xamarinPackage) : base(assemblyName, targetFrameworkVersion)
        {
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Ad-Hoc", "Any CPU"));
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Ad-Hoc", "iPhone"));
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Ad-Hoc", "iPhoneSimulator"));
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("AppStore", "Any CPU"));
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("AppStore", "iPhone"));
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("AppStore", "iPhoneSimulator"));
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Debug", "iPhone"));
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Debug", "iPhoneSimulator"));
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Release", "iPhone"));
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Release", "iPhoneSimulator"));

            WithNugetPackage(xamarinPackage);

            AddFileToFolder(new DefaultAppXamlFile(DefaultNamespace));
            AddFileToFolder(new DefaultMainPageXamlFile(DefaultNamespace));
        }
    }
}
