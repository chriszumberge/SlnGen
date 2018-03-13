using System;
using System.Xml.Linq;

namespace SlnGen.Core.Projects
{
    public class ConsoleApplicationCsProj : Project
    {

        public ConsoleApplicationCsProj(string assemblyName, string targetFrameworkVersion) : base(assemblyName, "Exe", targetFrameworkVersion)
        {
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Debug", "Any CPU"));
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Release", "Any CPU"));
        }

        protected override XElement[] GetProjectSpecificPropertyNodes(XNamespace xNamespace, Guid solutionGuid)
        {
            return new XElement[] { new XElement(xNamespace + "AutoGenerateBindingRedirects", new XText("true")) };
        }
    }
}
