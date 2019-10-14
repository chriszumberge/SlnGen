using SlnGen.Core.Files;
using SlnGen.Core.Projects;
using SlnGen.Core.Utils;

namespace SlnGen.Core.Wizard
{
    public static class SolutionWizardExtensions
    {
        public static SolutionWizard With_NetFrameworkClassLibrary(this SolutionWizard wizard, string assemblyName, NetFrameworkPlatform targetFrameworkVersion)
        {
            Project newProj = new NetFrameworkClassLibraryProject(assemblyName, targetFrameworkVersion);

            newProj.AddFileToFolder(new ProjectFile(SolutionWizard.CreateEmpty_NetFramework_ClassFile(assemblyName)));

            wizard.WithProject(newProj);

            return wizard;
        }

        public static SolutionWizard With_NetFrameworkConsoleApplication(this SolutionWizard wizard, string assemblyName, NetFrameworkPlatform targetFrameworkVersion)
        {
            Project newConsoleApp = new NetFrameworkConsoleApplicationCsProj(assemblyName, targetFrameworkVersion);

            newConsoleApp.AddFileToFolder(new ProjectFile(SolutionWizard.CreateDefault_NetFramework_ConsoleProgram(assemblyName)));

            wizard.WithProject(newConsoleApp);

            return wizard;
        }

        public static SolutionWizard With_NetCoreClassLibrary(this SolutionWizard wizard, string assemblyName, NetCorePlatform targetFrameworkVersion)
        {
            Project newProj = new NetCoreClassLibraryProject(assemblyName, targetFrameworkVersion);

            newProj.AddFileToFolder(new ProjectFile(SolutionWizard.CreateEmpty_NetFramework_ClassFile(assemblyName)));

            wizard.WithProject(newProj);

            return wizard;
        }

        public static SolutionWizard With_NetCoreConsoleApplication(this SolutionWizard wizard, string assemblyName, NetCorePlatform targetFrameworkVersion)
        {
            Project newConsoleApp = new NetCoreConsoleApplicationProject(assemblyName, targetFrameworkVersion);

            newConsoleApp.AddFileToFolder(new ProjectFile(SolutionWizard.CreateDefault_NetFramework_ConsoleProgram(assemblyName)));

            wizard.WithProject(newConsoleApp);

            return wizard;
        }

        public static SolutionWizard With_NetStandardClassLibrary(this SolutionWizard wizard, string assemblyName, NetStandardPlatform targetFrameworkVersion)
        {
            Project newProj = new NetStandardClassLibraryProject(assemblyName, targetFrameworkVersion);

            newProj.AddFileToFolder(new ProjectFile(SolutionWizard.CreateEmpty_NetFramework_ClassFile(assemblyName)));

            wizard.WithProject(newProj);

            return wizard;
        }
    }
}
