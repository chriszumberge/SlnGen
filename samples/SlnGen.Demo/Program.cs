using SlnGen.Core;
using SlnGen.Core.Code;
using SlnGen.Core.Files;
using SlnGen.Core.Projects;
using SlnGen.Core.Utils;
using SlnGen.Core.Wizard;
using System;

namespace SlnGen.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            string slnPath = String.Empty;

            // Test Implementation Support
            Console.WriteLine($"{NetCorePlatform.v1_0} - min -> {NetImplementationSupport.GetMinCompatibleWith(NetCorePlatform.v1_0).NetStandard}");
            Console.WriteLine($"{NetCorePlatform.v1_0} - max -> {NetImplementationSupport.GetMaxCompatibleWith(NetCorePlatform.v1_0).NetStandard}");

            slnPath = TestConsoleAppWithNetFrameworkClassLibrarySolution();
            Console.WriteLine($"Completed at {slnPath}");

            slnPath = TestWizard();
            Console.WriteLine($"Completed at {slnPath}");

            slnPath = TestConsoleAppWithNetCoreClassLibrarySolution();
            Console.WriteLine($"Completed at {slnPath}");

            slnPath = TestConsoleAppWithNetStandardClassLibrarySolution();
            Console.WriteLine($"Completed at {slnPath}");

            Console.ReadLine();
        }

        private static string TestWizard()
        {
            return new SolutionWizard("TestWizard", new RelativePathBuilder().AppendPath(RelativePath.C_Drive).ToString())
                .With_NetFrameworkClassLibrary("TestWizardLib", NetFrameworkPlatform.v4_5_1)
                .With_NetFrameworkConsoleApplication("TestWizardConsole", NetFrameworkPlatform.v4_5_1)
                .Build();
        }

        private static string TestConsoleAppWithNetFrameworkClassLibrarySolution()
        {
            Project classLibProject = new NetFrameworkClassLibraryProject("TestClassLibrary", NetFrameworkPlatform.v4_5_2)
                .WithNugetPackage(Core.References.Nuget.Newtonsoft_Json__10_0_1);

            //classLibProject.AddFileToFolder(new ProjectFile("Class1.cs", true, false, CreateEmptyClassFile("TestClassLibrary", "Class1").ToString()));
            classLibProject.AddFileToFolder(new ProjectFile("HelloWorld.cs", true, false, CreateHelloWorldClassFile(classLibProject.RootNamespace).ToString()));
            classLibProject.AddFileToFolder(new ProjectFile("Test.txt", false, true, "Testing a simple text file creation."));

            Project consoleAppProject = new NetFrameworkConsoleApplicationCsProj("TestConsoleApplication", NetFrameworkPlatform.v4_5_2)
                .WithNugetPackage(Core.References.Nuget.Newtonsoft_Json__10_0_1)
                // Fully qualify
                //.WithProjectReference(new ProjectReference(classLibProject.AssemblyName, $@"..\{classLibProject.AssemblyName}\{classLibProject.AssemblyName}.csproj", classLibProject.AssemblyGuid));
                // or
                .WithProjectReference(new ProjectReference(classLibProject, new RelativePathBuilder().AppendPath(RelativePath.Up_Directory).ToString()));

            //consoleAppProject.AddFileToFolder(new AppConfigFile(consoleAppProject.TargetFrameworkVersion));
            consoleAppProject.AddFileToFolder(new ProjectFile("help.txt", false, true));
            consoleAppProject.AddFileToFolder(new ProjectFile(
                new SGFile("Program.cs")
                {
                    AssemblyReferences =
                    {
                        new SGAssemblyReference("System"),
                        new SGAssemblyReference("System.Collections.Generic"),
                        new SGAssemblyReference("System.Linq"),
                        new SGAssemblyReference("System.Text"),
                        new SGAssemblyReference("System.Threading.Tasks"),
                        new SGAssemblyReference(classLibProject)
                    },
                    Namespaces =
                    {
                        new SGNamespace(consoleAppProject.RootNamespace)
                        {
                            Classes =
                            {
                                new SGClass("Program", SGAccessibilityLevel.None)
                                {
                                    Methods =
                                    {
                                        new SGMethod(new SGMethodSignature(accessibilityLevel: SGAccessibilityLevel.Private, methodName: "Main", returnType: "void", isStatic: true)
                                        {
                                            Arguments =
                                            {
                                                new SGArgument("args", "string[]")
                                            }
                                        })
                                        {
                                            Lines =
                                            {
                                                $"Console.WriteLine(HelloWorld.Greeting);",
                                                "",
                                                "Console.ReadLine();"
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }));

            Solution sln = new Solution("TestNetFrameworkSolution")
                .WithProject(classLibProject)
                .WithProject(consoleAppProject);

            return sln.GenerateSolutionFiles(@"C:\");
        }

        private static string TestConsoleAppWithNetCoreClassLibrarySolution()
        {
            Project classLibProject = new NetCoreClassLibraryProject("NetCoreClassLibrary", NetCorePlatform.v2_0)
                .WithNugetPackage(Core.References.Nuget.Newtonsoft_Json__11_0_2);

            classLibProject.AddFileToFolder(new ProjectFile("HelloWorld.cs", true, false, CreateHelloWorldClassFile(classLibProject.RootNamespace).ToString()));
            classLibProject.AddFileToFolder(new ProjectFile("Test.txt", false, true, "Testing a simple text file creation."));

            Project consoleAppProject = new NetCoreConsoleApplicationProject("NetCoreConsoleApp", NetCorePlatform.v2_0)
                .WithProjectReference(new ProjectReference(classLibProject, new RelativePathBuilder().AppendPath(RelativePath.Up_Directory).ToString()));

            consoleAppProject.AddFileToFolder(new ProjectFile("help.txt", false, true));
            consoleAppProject.AddFileToFolder(new ProjectFile(
                new SGFile("Program.cs")
                {
                    AssemblyReferences =
                    {
                        new SGAssemblyReference("System"),
                        new SGAssemblyReference(classLibProject)
                    },
                    Namespaces =
                    {
                        new SGNamespace(consoleAppProject.RootNamespace)
                        {
                            Classes =
                            {
                                new SGClass("Program", SGAccessibilityLevel.None)
                                {
                                    Methods =
                                    {
                                        new SGMethod(new SGMethodSignature(accessibilityLevel: SGAccessibilityLevel.Private, methodName: "Main", returnType: "void", isStatic: true)
                                        {
                                            Arguments =
                                            {
                                                new SGArgument("args", "string[]")
                                            }
                                        })
                                        {
                                            Lines =
                                            {
                                                $"Console.WriteLine(HelloWorld.Greeting);",
                                                "",
                                                "Console.ReadLine();"
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
            }));

            return new Solution("TestNetCoreSolution")
                .WithProject(classLibProject)
                .WithProject(consoleAppProject)
                .GenerateSolutionFiles(@"C:\");
        }

        private static string TestConsoleAppWithNetStandardClassLibrarySolution()
        {
            NetStandardPlatform netStandardPlatform = NetStandardPlatform.v1_6;

            Project classLibProject = new NetStandardClassLibraryProject("NetStandardClassLibrary", netStandardPlatform)
                .WithNugetPackage(Core.References.Nuget.Newtonsoft_Json__11_0_2);

            classLibProject.AddFileToFolder(new ProjectFile("HelloWorld.cs", true, false, CreateHelloWorldClassFile(classLibProject.RootNamespace).ToString()));
            classLibProject.AddFileToFolder(new ProjectFile("Test.txt", false, true, "Testing a simple text file creation."));

            Project netFrameworkConsoleAppProject = new NetFrameworkConsoleApplicationCsProj("NetFrameworkConsoleApp", NetImplementationSupport.GetMinCompatibleWith(netStandardPlatform).NetFramework)
                .WithProjectReference(new ProjectReference(classLibProject, new RelativePathBuilder().AppendPath(RelativePath.Up_Directory).ToString()))
                .WithNugetPackage(Core.References.Nuget.Newtonsoft_Json__11_0_2);

            //netFrameworkConsoleAppProject.AddFileToFolder(new AppConfigFile(netFrameworkConsoleAppProject.TargetFrameworkVersion));
            netFrameworkConsoleAppProject.AddFileToFolder(new ProjectFile(
                new SGFile("Program.cs")
                {
                    AssemblyReferences =
                    {
                        new SGAssemblyReference("System"),
                        new SGAssemblyReference("System.Collections.Generic"),
                        new SGAssemblyReference("System.Linq"),
                        new SGAssemblyReference("System.Text"),
                        new SGAssemblyReference("System.Threading.Tasks"),
                        new SGAssemblyReference(classLibProject)
                    },
                    Namespaces =
                    {
                        new SGNamespace(netFrameworkConsoleAppProject.RootNamespace)
                        {
                            Classes =
                            {
                                new SGClass("Program", SGAccessibilityLevel.None)
                                {
                                    Methods =
                                    {
                                        new SGMethod(new SGMethodSignature(accessibilityLevel: SGAccessibilityLevel.None, methodName: "Main", returnType: "void", isStatic: true)
                                        {
                                            Arguments =
                                            {
                                                new SGArgument("args", "string[]")
                                            }
                                        })
                                        {
                                            Lines =
                                            {
                                                $"Console.WriteLine(HelloWorld.Greeting);",
                                                "",
                                                "Console.ReadLine();"
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }));

            return new Solution("TestNetStandardSolution")
                .WithProject(classLibProject)
                .WithProject(netFrameworkConsoleAppProject)
                .GenerateSolutionFiles(@"C:\");
        }


        public static SGFile CreateEmptyClassFile(string namespaceName, string className)
        {
            return new SGFile(className, "cs")
            {
                AssemblyReferences =
                {
                    new SGAssemblyReference("System"),
                    new SGAssemblyReference("System.Collections.Generic"),
                    new SGAssemblyReference("System.Linq"),
                    new SGAssemblyReference("System.Text"),
                    new SGAssemblyReference("System.Threading.Tasks")
                },
                Namespaces =
                {
                    new SGNamespace(namespaceName)
                    {
                        Classes =
                        {
                            new SGClass(className, SGAccessibilityLevel.Public)
                        }
                    }
                }
            };
        }

        public static SGFile CreateHelloWorldClassFile(string namespaceName)
        {
            string className = "HelloWorld";

            return new SGFile(className, "cs")
            {
                AssemblyReferences =
                {
                    new SGAssemblyReference("System"),
                    new SGAssemblyReference("System.Collections.Generic"),
                    new SGAssemblyReference("System.Linq"),
                    new SGAssemblyReference("System.Text"),
                    new SGAssemblyReference("System.Threading.Tasks")
                },
                Namespaces =
                {
                    new SGNamespace(namespaceName)
                    {
                        Classes =
                        {
                            new SGClass(className, SGAccessibilityLevel.Public, false, true, false)
                            {
                                Fields =
                                {
                                    new SGClassField("Greeting", "string", SGAccessibilityLevel.Public, false, true).WithInitializationValue("Hello World!")
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
