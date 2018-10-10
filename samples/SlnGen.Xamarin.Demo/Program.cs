using SlnGen.Core;
using SlnGen.Core.Projects;
using SlnGen.Core.Utils;
using SlnGen.Xamarin.Files;
using SlnGen.Xamarin.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen.Xamarin.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            string slnPath = String.Empty;

            slnPath = TestXamarinCrossPlatformApp();
            Console.WriteLine($"Completed at {slnPath}");

            Console.ReadLine();
        }

        private static string TestXamarinCrossPlatformApp()
        {
            // Project Config
            string appName = "TestCrossPlatformApp";

            // Project Settings
            NugetPackage xamarinFormsPkg = new NugetPackage("Xamarin.Forms", "3.1.0.697729");

            // Assembling of Projects
            //Project codeProj = new NetStandardClassLibraryProject(appName, NetStandardPlatform.v2_0)
            //    .WithNugetPackage(xamarinFormsPkg);
            Project codeProj = new NetStandardXamarinClassLibraryProject(appName, NetStandardPlatform.v2_0, xamarinFormsPkg);
            Project androidProj = new XamarinAndroidAppProject(appName + ".Droid", appName, $"com.slngen.{appName}", XamarinAndroidPlatform.v8_1, 21, 27, xamarinFormsPkg)
                .WithProjectReference(new ProjectReference(codeProj, new RelativePathBuilder().AppendPath(RelativePath.Up_Directory).ToPath()));
            Project iOsProj = new XamariniOSAppProject(appName + ".iOS", appName, $"com.slngen.{appName}", XamariniOSPlatform.v8_0, xamarinFormsPkg)
                .WithProjectReference(new ProjectReference(codeProj, new RelativePathBuilder().AppendPath(RelativePath.Up_Directory).ToPath()));

            return new Solution("TestXamarinCrossPlatformApp")
                .WithProject(codeProj)
                .WithProject(androidProj)
                .WithProject(iOsProj)
                .GenerateSolutionFiles(@"C:\");
        }
    }
}
