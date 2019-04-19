using SlnGen.Core.Files;
using SlnGen.Core.Interfaces;
using SlnGen.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace SlnGen.Core.Projects
{
    public abstract class NetFrameworkProject : Project
    {
        public NetFrameworkProject(string assemblyName, string outputType, NetFrameworkPlatform targetFrameworkVersion, string rootNamespace = "") :
            this(assemblyName, Guid.NewGuid(), outputType, targetFrameworkVersion, rootNamespace) { }

        public NetFrameworkProject(string assemblyName, Guid assemblyGuid, string outputType, NetFrameworkPlatform targetFrameworkVersion, string rootNamespace = "") : 
            base(assemblyName, assemblyGuid, outputType, targetFrameworkVersion, rootNamespace)
        {
            AddDefaultAssemblyReferences();
            AddDefaultFoldersAndFiles();
        }

        /// <summary>
        /// Adds the default assembly references.
        /// This method is called in the project constructor. Override to customize.
        /// </summary>
        protected virtual void AddDefaultAssemblyReferences()
        {
            AssemblyReferences.Add(References.Assemblies.System);
            AssemblyReferences.Add(References.Assemblies.System_Core);
            AssemblyReferences.Add(References.Assemblies.System_Xml_Linq);
            AssemblyReferences.Add(References.Assemblies.Microsoft_CSharp);
            AssemblyReferences.Add(References.Assemblies.System_Data);
            AssemblyReferences.Add(References.Assemblies.System_Net_Http);
            AssemblyReferences.Add(References.Assemblies.System_Xml);
        }

        /// <summary>
        /// Adds the default folders and files.
        /// This method is called in the project constructor. Override to customize.
        /// Recommend calling Base on this method as it adds the properties folder and assembly info file.
        /// </summary>
        protected virtual void AddDefaultFoldersAndFiles()
        {
            Folders.Add(new ProjectFolder("Properties")
            {
                Files =
                {
                    new AssemblyInfoFile(AssemblyName, AssemblyGuid, new Version(1, 0, 0, 0), new Version(1, 0, 0, 0))
                }
            });
        }

        protected XNamespace xNamespace = "http://schemas.microsoft.com/developer/msbuild/2003";

        protected override string GenerateProjectFiles(string solutionDirectoryPath, Guid solutionGuid)
        {
            string csprojDirectoryPath = Path.Combine(solutionDirectoryPath, AssemblyName);
            DirectoryInfo csprojDirectory = Directory.CreateDirectory(csprojDirectoryPath);

            tempCsProjDirectoryPath = csprojDirectoryPath;

            CreatePackageConfig();

            AddProjectFilesAndFolders(this, csprojDirectoryPath);

            // create csproj file using xmlwriter
            //using (XmlWriter writer = XmlWriter.Create(String.Concat(AssemblyName, ".csproj")))
            //{
            //    writer.WriteStartDocument();
            //}
            //XmlDocument doc = new XmlDocument();
            //XmlElement root = doc.CreateElement("Project");
            //root.SetAttribute("ToolsVersion", "14.0");
            //root.SetAttribute("DefaultTargets", "Build");
            //root.SetAttribute("xmlns", "http://schemas.microsoft.com/developer/msbuild/2003");
            var xmlNode = new XElement(xNamespace + "Project",
                                        new XAttribute("ToolsVersion", "14.0"),
                                        new XAttribute("DefaultTargets", "Build"),
                                        new XAttribute("xmlns", "http://schemas.microsoft.com/developer/msbuild/2003"),
                                        new XElement(xNamespace + "Import",
                                            new XAttribute("Project", @"$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props"),
                                            new XAttribute("Condition", @"Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')")
                                        ),
                                        new XElement(xNamespace + "PropertyGroup",
                                            new XElement(xNamespace + "Configuration",
                                                new XAttribute("Condition", " '$(Configuration)' == '' "),
                                                new XText(DEFAULT_BUILD_CONFIGURATION)
                                            ),
                                            new XElement(xNamespace + "Platform",
                                                new XAttribute("Condition", " '$(Platform)' == '' "),
                                                new XText(DEFAULT_BUILD_PLATFORM)
                                            ),
                                            new XElement(xNamespace + "ProjectGuid",
                                                new XText(String.Concat("{", AssemblyGuid.ToString(), "}"))
                                            ),
                                            new XElement(xNamespace + "OutputType",
                                                new XText(OutputType)
                                            ),
                                            new XElement(xNamespace + "AppDesignerFolder",
                                                new XText("Properties")
                                            ),
                                            new XElement(xNamespace + "RootNamespace",
                                                new XText(RootNamespace)
                                                ),
                                            new XElement(xNamespace + "AssemblyName",
                                                new XText(AssemblyName)
                                                ),
                                            new XElement(xNamespace + "TargetFrameworkVersion",
                                                new XText(TargetFrameworkVersion.TargetVersion)
                                                ),
                                            new XElement(xNamespace + "FileAlignment",
                                                new XText("512")
                                                ),
                                            GetProjectSpecificPropertyNodes(xNamespace, solutionGuid),
                                            new XElement(xNamespace + "TargetFrameworkProfile",
                                                new XText(((NetFrameworkPlatform)TargetFrameworkVersion).Profile ?? String.Empty)
                                                )
                                        ), // END PROPERTY GROUP
                                        GetBuildConfigurationPropertyGroups(xNamespace),
                                        GetAssemblyReferenceItemGroup(),
                                        GetProjectReferenceItemGroup(),
                                        GetCompileFilesItemGroup(),
                                        GetOtherFileItemGroup(),
                                        GetContentFilesItemGroup(),
                                        GetNoneFilesItemGroup(),
                                        GetProjectFoldersItemGroup(),
                                        GetCustomFilesItemGroups(xNamespace),
                                        GetImportProjectItems(xNamespace),
                                        GetTargetItems(xNamespace),
                                        GetPreBuildEventPropertyGroups(xNamespace),
                                        GetPostBuildEventPropertyGroups(xNamespace)
                                    ); // END PROJECT
            string csprojFilePath = Path.Combine(csprojDirectoryPath, String.Concat(AssemblyName, ".csproj"));
            xmlNode.Save(csprojFilePath);

            return csprojFilePath;
        }

        private void CreatePackageConfig()
        {
            // Do packages.config with all the added nuget packages
            var packageRoot = new XElement("packages");
            ProjectFile packagesConfig = new ProjectFile("packages.config", false, false);
            foreach (NugetPackage package in NugetPackages)
            {
                XElement packageElement =
                        new XElement("package",
                            new XAttribute("id", package.Id),
                            new XAttribute("version", package.Version),
                            new XAttribute("targetFramework", package.TargetFrameworks[TargetFrameworkVersion])
                        );
                packageRoot.Add(packageElement);
            }
            using (var memoryStream = new MemoryStream())
            {
                packageRoot.Save(memoryStream);

                memoryStream.Position = 0;
                using (var streamReader = new StreamReader(memoryStream))
                {
                    string contents = streamReader.ReadToEnd();
                    packagesConfig.FileContents = contents;
                }
            }
            Files.Add(packagesConfig);
        }

        protected XElement[] GetBuildConfigurationPropertyGroups(XNamespace xNamespace)
        {
            List<XElement> nodes = new List<XElement>();
            foreach (SupportedBuildConfiguration config in SupportedBuildConfigurations)
            {
                if (config.Build)
                {
                    XElement buildConfigNode = this.ConstructBuildConfigurationPropertyGroup(xNamespace, config);
                    if (buildConfigNode != null)
                    {
                        nodes.Add(buildConfigNode);
                    }
                }
            }

            return nodes.ToArray();
        }

        protected virtual XElement ConstructBuildConfigurationPropertyGroup(XNamespace xNamespace, SupportedBuildConfiguration config)
        {
            if (config.Platform.Equals("Any CPU") && config.Configuration.Equals("Debug"))
            {
                return GetDebugAnyCPUPropertyGroup();
            }
            else if (config.Platform.Equals("Any CPU") && config.Configuration.Equals("Release"))
            {
                return GetReleaseAnyCPUPropertyGroup();
            }
            else
            {
                return null;
            }
        }

        protected XElement GetAssemblyReferenceItemGroup()
        {
            XElement itemGroup = new XElement(xNamespace + "ItemGroup");
            foreach (NugetPackage nugetPackage in NugetPackages)
            {
                foreach (NugetAssembly nugetAssembly in nugetPackage.Assemblies)
                {
                    XElement packageElement =
                        new XElement(xNamespace + "Reference",
                            new XAttribute("Include", nugetAssembly.Include),
                            new XElement(xNamespace + "HintPath",
                                new XText(nugetAssembly.HintPath)
                            ),
                            new XElement(xNamespace + "Private",
                                new XText(nugetAssembly.IsPrivate.ToString())
                            )
                        );
                    itemGroup.Add(packageElement);
                }
            }
            foreach (AssemblyReference assembly in AssemblyReferences)
            {
                XElement assemblyElement =
                    new XElement(xNamespace + "Reference",
                        new XAttribute("Include", assembly.Name)
                    );
                itemGroup.Add(assemblyElement);
            }
            return itemGroup;
        }

        protected XElement GetProjectReferenceItemGroup()
        {
            XElement itemGroup = new XElement(xNamespace + "ItemGroup");
            foreach (ProjectReference project in ProjectReferences)
            {
                XElement projectElement =
                    new XElement(xNamespace + "ProjectReference",
                        new XAttribute("Include", project.Include),
                        new XElement(xNamespace + "Project",
                            new XText(String.Concat("{", project.ProjectGuid.ToString(), "}"))
                        ),
                        new XElement(xNamespace + "Name",
                            new XText(project.Name)
                        )
                    );
                itemGroup.Add(projectElement);
            }
            return itemGroup;
        }

        protected XElement GetCompileFilesItemGroup()
        {
            List<KeyValuePair<ProjectFile, string>> compilableFiles = _tempFileRelativePathDictionary.Where(x => x.Key.ShouldCompile).ToList();
            XElement itemGroup = new XElement(xNamespace + "ItemGroup");
            foreach (KeyValuePair<ProjectFile, string> compilableFile in compilableFiles)
            {
                XElement compilableElement =
                    new XElement(xNamespace + "Compile",
                        new XAttribute("Include", compilableFile.Value)
                    );

                foreach (string dependent in compilableFile.Key.DependentUpon)
                {
                    compilableElement.Add(
                        new XElement(xNamespace + "DependentUpon",
                            new XText(dependent)
                        )
                    );
                }

                itemGroup.Add(compilableElement);
            }
            return itemGroup;
        }

        protected XElement GetOtherFileItemGroup()
        {
            XElement itemGroup = new XElement(xNamespace + "ItemGroup");

            List<KeyValuePair<ProjectFile, string>> embeddedFiles = _tempFileRelativePathDictionary.Where(x => x.Key is EmbeddedResourceProjectFile).ToList();
            foreach (KeyValuePair<ProjectFile, string> embeddedFile in embeddedFiles)
            {
                EmbeddedResourceProjectFile typeCastedFile = embeddedFile.Key as EmbeddedResourceProjectFile;

                XElement embeddedElement =
                    new XElement(xNamespace + "EmbeddedResource",
                        new XAttribute("Include", embeddedFile.Value),
                        new XElement(xNamespace + "SubType",
                            new XText(typeCastedFile.SubType)
                        ),
                        new XElement(xNamespace + "Generator",
                            new XText(typeCastedFile.Generator)
                        )
                    );
                itemGroup.Add(embeddedElement);
            }

            // TODO WHEN MONO ANDROID PROJECT?
            //List<KeyValuePair<ProjectFile, string>> androidResourceFiles = tempFileRelativePathDictionary.Where(x => x.Key is AndroidResourceProjectFile).ToList();
            //foreach (KeyValuePair<ProjectFile, string> androidResourceFile in androidResourceFiles)
            //{
            //    AndroidResourceProjectFile typeCastedFile = androidResourceFile.Key as AndroidResourceProjectFile;

            //    XElement resourceElement =
            //        new XElement(xNamespace + "AndroidResource",
            //            new XAttribute("Include", androidResourceFile.Value)
            //        );
            //    itemGroup.Add(resourceElement);
            //}

            return itemGroup;
        }

        protected XElement GetContentFilesItemGroup()
        {
            List<KeyValuePair<ProjectFile, string>> contentFiles = _tempFileRelativePathDictionary.Where(x => !x.Key.ShouldCompile && x.Key.IsContent).ToList();
            XElement itemGroup = new XElement(xNamespace + "ItemGroup");
            foreach (KeyValuePair<ProjectFile, string> contentFile in contentFiles)
            {
                XElement contentElement =
                    new XElement(xNamespace + "Content",
                        new XAttribute("Include", contentFile.Value)
                    );
                itemGroup.Add(contentElement);
            }
            return itemGroup;
        }

        protected XElement GetNoneFilesItemGroup()
        {
            // TODO WHEN MOONO ANDROID PROJECT?
            //List<KeyValuePair<ProjectFile, string>> noneTypeFiles = tempFileRelativePathDictionary.Where(x => !x.Key.ShouldCompile && !x.Key.IsContent
            //    && !(x.Key is EmbeddedResourceProjectFile || x.Key is AndroidResourceProjectFile)).ToList();
            List<KeyValuePair<ProjectFile, string>> noneTypeFiles = _tempFileRelativePathDictionary.Where(x => !x.Key.ShouldCompile && !x.Key.IsContent
                && !(x.Key is EmbeddedResourceProjectFile)).ToList();
            XElement itemGroup = new XElement(xNamespace + "ItemGroup");
            foreach (KeyValuePair<ProjectFile, string> noneTypeFile in noneTypeFiles)
            {
                XElement noneTypeElement =
                    new XElement(xNamespace + "None",
                        new XAttribute("Include", noneTypeFile.Value)
                    );
                itemGroup.Add(noneTypeElement);
            }
            return itemGroup;
        }

        protected XElement GetProjectFoldersItemGroup()
        {
            XElement itemGroup = new XElement(xNamespace + "ItemGroup");

            foreach (var emptyFolderPath in _emptyFolderRelativePathList)
            {
                XElement folderElement =
                    new XElement(xNamespace + "Folder",
                        new XAttribute("Include", emptyFolderPath)
                    );
                itemGroup.Add(folderElement);
            }

            return itemGroup;
        }

        protected XElement GetDebugAnyCPUPropertyGroup()
        {
            return
            new XElement(xNamespace + "PropertyGroup",
                new XAttribute("Condition", " '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' "),
                new XElement(xNamespace + "DebugSymbols",
                    new XText("true")
                    ),
                new XElement(xNamespace + "DebugType",
                    new XText("full")
                    ),
                new XElement(xNamespace + "Optimize",
                    new XText("false")
                    ),
                new XElement(xNamespace + "OutputPath",
                    new XText(@"bin\Debug\")
                    ),
                new XElement(xNamespace + "DefineConstants",
                    new XText("DEBUG;TRACE")
                    ),
                new XElement(xNamespace + "ErrorReport",
                    new XText("prompt")
                    ),
                new XElement(xNamespace + "WarningLevel",
                    new XText("4")
                    )
                );

        }

        protected XElement GetReleaseAnyCPUPropertyGroup()
        {
            return
            new XElement(xNamespace + "PropertyGroup",
                new XAttribute("Condition", " '$(Configuration)|$(Platform)' == 'Release|AnyCPU' "),
                new XElement(xNamespace + "DebugType",
                    new XText("pdbonly")
                    ),
                new XElement(xNamespace + "Optimize",
                    new XText("true")
                    ),
                new XElement(xNamespace + "OutputPath",
                    new XText(@"bin\Release\")
                    ),
                new XElement(xNamespace + "DefineConstants",
                    new XText("TRACE")
                    ),
                new XElement(xNamespace + "ErrorReport",
                    new XText("prompt")
                    ),
                new XElement(xNamespace + "WarningLevel",
                    new XText("4")
                    )
                );
        }

        protected virtual XElement[] GetPreBuildEventPropertyGroups(XNamespace xNamespace)
        {
            //<PropertyGroup>
            //    <PreBuildEvent>$(SolutionDir)\BuildUtilities\IncrementBuildiOS.exe "$(ProjectDir)\Info.plist"</PreBuildEvent>
            //</PropertyGroup>
            return new XElement[] { };
        }

        protected virtual XElement[] GetPostBuildEventPropertyGroups(XNamespace xNamespace)
        {
            //<PropertyGroup>
            //    <PostBuildEvent>$(SolutionDir)\BuildUtilities\IncrementBuildiOS.exe "$(ProjectDir)\Info.plist"</PostBuildEvent>
            //</PropertyGroup>
            return new XElement[] { };
        }

        protected virtual XElement[] GetCustomFilesItemGroups(XNamespace xNamespace)
        {
            return new XElement[] { };
        }

        /// <remarks>
        /// Probably should not call base when overriding this method. If you're overriding it there's a chance you do not want the default features.
        /// </remarks>
        protected virtual XElement[] GetImportProjectItems(XNamespace xNamespace)
        {
            return new XElement[] {
                new XElement(xNamespace + "Import",
                    new XAttribute("Project", @"$(MSBuildToolsPath)\Microsoft.CSharp.targets")
                )
            };
        }

        protected virtual XElement[] GetTargetItems(XNamespace xNamespace)
        {
            return new XElement[] { };
        }

        protected virtual XElement[] GetProjectSpecificPropertyNodes(XNamespace xNamespace, Guid solutionGuid)
        {
            return new XElement[] { };
        }
    }
}
