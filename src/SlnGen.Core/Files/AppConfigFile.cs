using SlnGen.Core.Utils;
using System;
using System.IO;
using System.Xml.Linq;

namespace SlnGen.Core.Files
{
    public class AppConfigFile : ConfigFile
    {
        //public AppConfigFile(NetFrameworkVersion netFrameworkVersion) : this(netFrameworkVersion.ToString()) { }

        public AppConfigFile(NetPlatform netPlatformVersion) : base("App")
        {
            var supportedRuntimeNode = new XElement("supportedRuntime");

            if (netPlatformVersion is NetFrameworkPlatform)
            {
                NetFrameworkPlatform frameworkVersion = netPlatformVersion as NetFrameworkPlatform;

                supportedRuntimeNode.SetAttributeValue("version", frameworkVersion.TargetFrameworkVersion);

                if (!String.IsNullOrEmpty(frameworkVersion.SKU))
                {
                    supportedRuntimeNode.SetAttributeValue("sku", frameworkVersion.SKU);
                }
            }

            var configNode = new XElement("configuration",
                                    new XElement("startup",
                                        supportedRuntimeNode
                                        //new XElement("supportedRuntime",
                                        //    new XAttribute("version", "v4.0"),
                                        //    new XAttribute("sku", ".NETFramework,Version=v4.5.2")
                                        //)
                                    )
                                );

            using (var memoryStream = new MemoryStream())
            {
                configNode.Save(memoryStream);

                memoryStream.Position = 0;
                using (var streamReader = new StreamReader(memoryStream))
                {
                    FileContents = streamReader.ReadToEnd();
                }
            }
        }
    }
}
