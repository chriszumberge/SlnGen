using SlnGen.Core.Files;
using SlnGen.Core.Utils;
using System;
using System.Xml.Linq;

namespace SlnGen.Core.Projects
{
    public class NetFrameworkConsoleApplicationCsProj : NetFrameworkProject
    {

        public NetFrameworkConsoleApplicationCsProj(string assemblyName, NetFrameworkPlatform targetFrameworkVersion, string rootNamespace = "") : 
            base(assemblyName, "Exe", targetFrameworkVersion, rootNamespace)
        {
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Debug", "Any CPU"));
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Release", "Any CPU"));

            AddFileToFolder(new AppConfigFile(targetFrameworkVersion));
        }

        protected override XElement[] GetProjectSpecificPropertyNodes(XNamespace xNamespace, Guid solutionGuid)
        {
            return new XElement[] { new XElement(xNamespace + "AutoGenerateBindingRedirects", new XText("true")) };
        }
    }
}
