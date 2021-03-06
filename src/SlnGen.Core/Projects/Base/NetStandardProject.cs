﻿using SlnGen.Core.Utils;
using System;
using System.IO;
using System.Xml.Linq;

namespace SlnGen.Core.Projects
{
    public abstract class NetStandardProject : Project
    {
        public NetStandardProject(string assemblyName, string outputType, NetStandardPlatform targetFrameworkVersion, string rootNamespace = "") :
            this(assemblyName, Guid.NewGuid(), outputType, targetFrameworkVersion, rootNamespace)
        { }

        public NetStandardProject(string assemblyName, Guid assemblyGuid, string outputType, NetStandardPlatform targetFrameworkVersion, string rootNamespace = "") :
            base(assemblyName, assemblyGuid, outputType, targetFrameworkVersion, rootNamespace)
        { }

        protected override string GenerateProjectFiles(string solutionDirectoryPath, Guid solutionGuid)
        {
            string csprojDirectoryPath = Path.Combine(solutionDirectoryPath, AssemblyName);
            DirectoryInfo csprojDirectory = Directory.CreateDirectory(csprojDirectoryPath);

            tempCsProjDirectoryPath = csprojDirectoryPath;

            AddProjectFilesAndFolders(this, csprojDirectoryPath);

            var xmlNode = new XElement("Project",
                                            new XAttribute("Sdk", "Microsoft.NET.Sdk"),
                                            new XElement("PropertyGroup",
                                                new XElement("OutputType",
                                                    new XText(OutputType)
                                                ),
                                                new XElement("TargetFramework",
                                                    new XText(TargetFrameworkVersion.TargetVersion)
                                                )
                                            ), // END PROPERTY GROUP
                                            GetAssemblyReferenceItemGroup(),
                                            GetProjectReferenceItemGroup()
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
    }
}
