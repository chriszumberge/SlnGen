using SlnGen.Core.Utils;
using System;
using System.IO;
using System.Xml.Linq;

namespace SlnGen.Core.Projects
{
    public abstract class NetCoreProject : Project
    {
        public NetCoreProject(string assemblyName, string outputType, NetCorePlatform targetFrameworkVersion, string rootNamespace = "", string sdk = "") :
            this(assemblyName, Guid.NewGuid(), outputType, targetFrameworkVersion, rootNamespace)
        { }

        public NetCoreProject(string assemblyName, Guid assemblyGuid, string outputType, NetCorePlatform targetFrameworkVersion, string rootNamespace = "", string sdk = "") :
            base(assemblyName, assemblyGuid, outputType, targetFrameworkVersion, rootNamespace)
        {
            if (!String.IsNullOrEmpty(sdk))
            {
                SDK = sdk;
            }
        }

        protected string SDK = "Microsoft.NET.Sdk";

        protected override string GenerateProjectFiles(string solutionDirectoryPath, Guid solutionGuid)
        {
            string csprojDirectoryPath = Path.Combine(solutionDirectoryPath, AssemblyName);
            DirectoryInfo csprojDirectory = Directory.CreateDirectory(csprojDirectoryPath);

            tempCsProjDirectoryPath = csprojDirectoryPath;

            AddProjectFilesAndFolders(this, csprojDirectoryPath);

            var xmlNode = new XElement("Project",
                                            new XAttribute("Sdk", SDK),
                                            new XElement("PropertyGroup",
                                                new XElement("OutputType",
                                                    new XText(OutputType)
                                                ),
                                                new XElement("TargetFramework",
                                                    new XText(TargetFrameworkVersion.TargetVersion)
                                                )
                                            ), // END PROPERTY GROUP
                                            GetAssemblyReferenceItemGroup(),
                                            GetProjectReferenceItemGroup(),
                                            GetProjectFoldersItemGroup()
                                        ); // END PROJECT
            string csprojFilePath = Path.Combine(csprojDirectoryPath, String.Concat(AssemblyName, ".csproj"));
            xmlNode.Save(csprojFilePath);

            return csprojFilePath;
        }

        protected XElement GetAssemblyReferenceItemGroup()
        {
            XElement itemGroup = new XElement("ItemGroup");
            foreach (NugetPackage nugetPackage in NugetPackages)
            {
                XElement packageElement =
                    new XElement("PackageReference",
                        new XAttribute("Include", nugetPackage.Id),
                        new XAttribute("Version", nugetPackage.Version)
                    );

                itemGroup.Add(packageElement);
            }
            return itemGroup;
        }

        protected XElement GetProjectReferenceItemGroup()
        {
            XElement itemGroup = new XElement("ItemGroup");
            foreach (ProjectReference project in ProjectReferences)
            {
                XElement projectElement =
                    new XElement("ProjectReference",
                        new XAttribute("Include", project.Include)
                    );
                itemGroup.Add(projectElement);
            }
            return itemGroup;
        }

        protected XElement GetProjectFoldersItemGroup()
        {
            XElement itemGroup = new XElement("ItemGroup");

            foreach (var emptyFolderPath in _emptyFolderRelativePathList)
            {
                XElement folderElement =
                    new XElement("Folder",
                        new XAttribute("Include", emptyFolderPath)
                    );
                itemGroup.Add(folderElement);
            }

            return itemGroup;
        }
    }
}
