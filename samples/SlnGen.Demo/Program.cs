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

            slnPath = TestConsoleAppWithNetFrameworkClassLibrarySolution();
            Console.WriteLine($"Completed at {slnPath}");

            slnPath = TestWizard();
            Console.WriteLine($"Completed at {slnPath}");

            Console.ReadLine();
        }

        private static string TestWizard()
        {
            return new SolutionWizard("TestWizard", new RelativePathBuilder().AppendPath(RelativePath.C_Drive).ToString())
                .With_NetFrameworkClassLibrary("TestWizardLib", NetFrameworkVersion.v4_5_1)
                .With_NetFrameworkConsoleApplication("TestWizardConsole", NetFrameworkVersion.v4_5_1)
                .Build();
        }

        private static string TestConsoleAppWithNetFrameworkClassLibrarySolution()
        {
            Project classLibProject = new NetFrameworkClassLibraryProject("TestClassLibrary", NetFrameworkVersion.v4_5_2.TargetVersion)
                .WithNugetPackage(Core.References.Nuget.NewtonsoftJson_net452);

            //classLibProject.AddFileToFolder(new ProjectFile("Class1.cs", true, false, CreateEmptyClassFile("TestClassLibrary", "Class1").ToString()));
            classLibProject.AddFileToFolder(new ProjectFile("HelloWorld.cs", true, false, CreateHelloWorldClassFile(classLibProject.AssemblyName).ToString()));
            classLibProject.AddFileToFolder(new ProjectFile("Test.txt", false, true, "Testing a simple text file creation."));

            Project consoleAppProject = new NetFrameworkConsoleApplicationCsProj("TestConsoleApplication", NetFrameworkVersion.v4_5_2.TargetVersion)
                .WithNugetPackage(Core.References.Nuget.NewtonsoftJson_net452)
                // Fully qualify
                //.WithProjectReference(new ProjectReference(classLibProject.AssemblyName, $@"..\{classLibProject.AssemblyName}\{classLibProject.AssemblyName}.csproj", classLibProject.AssemblyGuid));
                // or
                .WithProjectReference(new ProjectReference(classLibProject, new RelativePathBuilder().AppendPath(RelativePath.Up_Directory).ToString()));

            consoleAppProject.AddFileToFolder(new AppConfigFile(NetFrameworkVersion.v4_5_2));
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
                        new SGNamespace("TestConsoleApplication")
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

            Solution sln = new Solution("TestClassLibraries")
                .WithProject(classLibProject)
                .WithProject(consoleAppProject);

            return sln.GenerateSolutionFiles(@"C:\");
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
