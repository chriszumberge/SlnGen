using SlnGen.Core.Files;
using SlnGen.Core.Projects;
using SlnGen.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen.Core.Wizard
{
    public static class SolutionWizardExtensions
    {
        public static SolutionWizard With_NetFrameworkClassLibrary(this SolutionWizard wizard, string assemblyName, NetFrameworkPlatform targetFrameworkVersion)
        {
            Project newProj = new NetFrameworkClassLibraryProject(assemblyName, targetFrameworkVersion);

            newProj.AddFileToFolder(new ProjectFile("Class1.cs", true, false, SolutionWizard.CreateEmptyClassFile(assemblyName).ToString()));

            wizard.WithProject(newProj);

            return wizard;
        }

        public static SolutionWizard With_NetFrameworkConsoleApplication(this SolutionWizard wizard, string assemblyName, NetFrameworkPlatform targetFrameworkVersion)
        {
            Project newConsoleApp = new NetFrameworkConsoleApplicationCsProj(assemblyName, targetFrameworkVersion);

            newConsoleApp.AddFileToFolder(new AppConfigFile(targetFrameworkVersion));
            newConsoleApp.AddFileToFolder(new ProjectFile(SolutionWizard.CreateDefaultConsoleProgram(assemblyName)));

            wizard.WithProject(newConsoleApp);

            return wizard;
        }
    }
}
