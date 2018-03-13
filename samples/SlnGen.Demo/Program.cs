using SlnGen.Core;
using SlnGen.Core.Code;
using SlnGen.Core.Files;
using SlnGen.Core.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            string slnPath = String.Empty;

            slnPath = TestConsoleAppWithNetFrameworkClassLibrarySolution();

            Console.WriteLine($"Completed at {slnPath}");
            Console.ReadLine();
        }

        private static string TestConsoleAppWithNetFrameworkClassLibrarySolution()
        {
            Project classLibProject = new NetFrameworkClassLibraryProject("TestClassLibrary", "v4.5.2")
                .WithNugetPackage(Core.References.Nuget.NewtonsoftJson_net452);

            classLibProject.AddFileToFolder(new ProjectFile("Class1.cs", true, false, CreateEmptyClassFile("TestClassLibrary", "Class1").ToString()));
            classLibProject.AddFileToFolder(new ProjectFile("Test.txt", false, true, String.Empty));

            Project consoleAppProject = new ConsoleApplicationCsProj("TestConsoleApplication", "v4.5.2")
                .WithNugetPackage(Core.References.Nuget.NewtonsoftJson_net452)
                // TODO something to make the reference relative path simpler?
                .WithProjectReference(new ProjectReference(classLibProject.AssemblyName, $@"..\{classLibProject.AssemblyName}\{classLibProject.AssemblyName}.csproj", classLibProject.AssemblyGuid));

            consoleAppProject.AddFileToFolder(new AppConfigFile());
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
                        new SGAssemblyReference("System.Threading.Tasks")
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
    }
}
