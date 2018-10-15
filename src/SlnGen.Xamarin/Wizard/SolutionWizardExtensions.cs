using SlnGen.Core;
using SlnGen.Core.Utils;
using SlnGen.Core.Wizard;
using SlnGen.Xamarin.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen.Xamarin.Wizard
{
    public static class SolutionWizardExtensions
    {
        public static SolutionWizard With_NetStandardXamarinClassLibrary(this SolutionWizard wizard, string assemblyName, NugetPackage xamarinFormsPkg, NetStandardPlatform targetFrameworkVersion)
        {
            Project newProj = new NetStandardXamarinClassLibraryProject(assemblyName, targetFrameworkVersion, xamarinFormsPkg);

            wizard.WithProject(newProj);
            return wizard;
        }

        //public static SolutionWizard With_XamarinAndroidApp(this SolutionWizard wizard, string assemblyName, NugetPackage xamarinFormsPkg, XamarinAndroidPlatform targetFrameworkVersion)
        //{
        //    Project newAndroidApp = new XamarinAndroidAppProject(assemblyName, assemblyName, )
        //}
    }
}
