using SlnGen.Core;
using SlnGen.Core.Code;
using SlnGen.Core.Projects;
using SlnGen.Core.Utils;
using ZESoft.SlnGen.Web.Files;
using ZESoft.SlnGen.Web.References;

namespace ZESoft.SlnGen.Web.Projects
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
            WithFile(new NetCoreAPIAppSettingsJsonFile());
            WithFile(new NetCoreAPIAppSettingsDevelopmentJsonFile());

            // Configurable
            WithFile(new NetCoreAPIStartupFile(RootNamespace).Build());

            WithFile(new ProjectFile(new SGFile("Program.cs")
            {
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

    }
}
