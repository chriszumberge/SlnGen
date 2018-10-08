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



            return new Solution("TestXamarinCrossPlatformApp")
                .WithProject(codeProj)
                .GenerateSolutionFiles(@"C:\");
        }
    }
}
