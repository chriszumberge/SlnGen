using SlnGen.Core;
using SlnGen.Core.Utils;
using System;
using ZESoft.SlnGen.Web.Projects;

namespace SlnGen.Web.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            string slnPath = String.Empty;

            slnPath = TestNetCoreAPI();
            Console.WriteLine($"Completed at {slnPath}");

            Console.ReadLine();
        }

        private static string TestNetCoreAPI()
        {
            // Project config
            string appName = "TestNetCoreAPI";

            // Project Settings

            // Assembling of Projects
            Project apiProj = new NetCoreAPIProject(appName, NetCorePlatform.v2_0);

            return new Solution("TestNetCoreWebApplication")
                .WithProject(apiProj)
                .GenerateSolutionFiles(@"C:\");
        }
    }
}
