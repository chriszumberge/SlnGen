using SlnGen.Core.Utils;
using System;
using System.Xml.Linq;

namespace SlnGen.Core.Projects
{
    public class NetFrameworkConsoleApplicationCsProj : NetFrameworkProject
    {

        public NetFrameworkConsoleApplicationCsProj(string assemblyName, NetFrameworkPlatform targetFrameworkVersion) : base(assemblyName, "Exe", targetFrameworkVersion)
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
