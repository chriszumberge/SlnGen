using System;
using SlnGen.Core;
using SlnGen.Core.Code;
using SlnGen.Core.Projects;
using SlnGen.Core.Utils;
using SlnGen.Web.Files;
using SlnGen.Web.References;

namespace SlnGen.Web.Projects
{
    public class NetCoreAPIProject : NetCoreProject
    {
        public NetCoreAPIProject(string assemblyName, NetCorePlatform targetFrameworkVersion, string rootNamespace = "") :
            base(assemblyName, "Exe", targetFrameworkVersion, rootNamespace, "Microsoft.NET.Sdk.Web")
        {
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Debug", "Any CPU"));
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Release", "Any CPU"));

            WithNugetPackage(Nuget.Microsoft_AspNetCore_App__2_1_1);
            WithNugetPackage(Nuget.Microsoft_AspNetCore_Razor_Design__2_1_2);

            WithFolder(new ProjectFolder("wwwroot"));
            WithFolder(new ProjectFolder("Controllers"));
            
            WithFile(new NetCoreAPILaunchSettingsJsonFile(assemblyName), "Properties");
            
            AppSettings = new NetCoreAPIAppSettingsJsonFile();
            DevelopmentAppSettings = new NetCoreAPIAppSettingsDevelopmentJsonFile();

            // Configurable
            StartupFile = new NetCoreAPIStartupFile(RootNamespace);

            WithFile(new ProjectFile(new SGFile("Program.cs")
            {
                AssemblyReferences =
                {
                    new SGAssemblyReference("Microsoft.AspNetCore"),
                    new SGAssemblyReference("Microsoft.AspNetCore.Hosting")
                },
                Namespaces =
                {
                    new SGNamespace(RootNamespace)
                    {
                        Classes =
                        {
                            new SGClass("Program", SGAccessibilityLevel.Public)
                            {
                                Methods =
                                {
                                    new SGMethod(new SGMethodSignature("Main", SGAccessibilityLevel.Public, isStatic: true)
                                    {
                                        Arguments =
                                        {
                                            new SGArgument("string[]", "args")
                                        }
                                    })
                                    {
                                        Lines = { "CreateWebHostBuilder(args).Build().Run();" }
                                    },
                                    new SGMethod(new SGMethodSignature("CreateWebHostBuilder", SGAccessibilityLevel.Public, isStatic: true, returnType: "IWebHostBuilder")
                                    {
                                        Arguments =
                                        {
                                            new SGArgument("string[]", "args")
                                        }
                                    })
                                    {
                                        Lines = { "return WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();"}
                                    }
                                }
                            }
                        }
                    }
                }
            }));
            
        }

        public NetCoreAPIAppSettingsJsonFile AppSettings { get; private set; }
        public NetCoreAPIAppSettingsDevelopmentJsonFile DevelopmentAppSettings { get; private set; }
        public NetCoreAPIStartupFile StartupFile { get; private set; }

        protected override string GenerateProjectFiles(string solutionDirectoryPath, Guid solutionGuid)
        {
            WithFile(DevelopmentAppSettings);
            WithFile(AppSettings);
            WithFile(StartupFile.Build());

            return base.GenerateProjectFiles(solutionDirectoryPath, solutionGuid);
        }

    }
}
