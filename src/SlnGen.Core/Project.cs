using SlnGen.Core.Files;
using SlnGen.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SlnGen.Core
{
    public abstract class Project : IFileContainer
    {
        protected const string DEFAULT_BUILD_CONFIGURATION = "Debug";
        protected const string DEFAULT_BUILD_PLATFORM = "AnyCPU";

        /// <summary>
        /// Gets the name of the assembly.
        /// </summary>
        /// <value>
        /// The name of the assembly.
        /// </value>
        public string AssemblyName { get; }

        /// <summary>
        /// Gets the assembly unique identifier.
        /// </summary>
        /// <value>
        /// The assembly unique identifier.
        /// </value>
        public Guid AssemblyGuid { get; }


        /// <summary>
        /// Gets the assembly references.
        /// </summary>
        /// <value>
        /// The assembly references.
        /// </value>
        public List<AssemblyReference> AssemblyReferences => _assemblyReferences;
        protected List<AssemblyReference> _assemblyReferences = new List<AssemblyReference>();

        /// <summary>
        /// Gets the nuget packages.
        /// </summary>
        /// <value>
        /// The nuget packages.
        /// </value>
        public List<NugetPackage> NugetPackages => _nugetPackages;
        protected List<NugetPackage> _nugetPackages = new List<NugetPackage>();

        /// <summary>
        /// Gets the project references.
        /// </summary>
        /// <value>
        /// The project references.
        /// </value>
        public List<ProjectReference> ProjectReferences => _projectReferences;
        protected List<ProjectReference> _projectReferences = new List<ProjectReference>();

        /// <summary>
        /// Gets the project output type.
        /// </summary>
        /// <value>
        /// The project output type.
        /// </value>
        public string OutputType { get; }

        /// <summary>
        /// Gets the target framework version.
        /// </summary>
        /// <value>
        /// The target framework version.
        /// </value>
        public string TargetFrameworkVersion { get; }

        /// <summary>
        /// Gets the files.
        /// </summary>
        /// <value>
        /// The files.
        /// </value>
        public List<ProjectFile> Files => _files;
        protected List<ProjectFile> _files = new List<ProjectFile>();

        /// <summary>
        /// Gets the folders.
        /// </summary>
        /// <value>
        /// The folders.
        /// </value>
        public List<ProjectFolder> Folders => _folders;
        protected List<ProjectFolder> _folders = new List<ProjectFolder>();

        /// <summary>
        /// Gets the supported build configurations.
        /// </summary>
        /// <value>
        /// The supported build configurations.
        /// </value>
        public List<SupportedBuildConfiguration> SupportedBuildConfigurations => _supportedBuildConfigurations;
        protected List<SupportedBuildConfiguration> _supportedBuildConfigurations = new List<SupportedBuildConfiguration>();

        public Project(string assemblyName, string outputType, string targetFrameworkVersion) :
            this(assemblyName, Guid.NewGuid(), outputType, targetFrameworkVersion) { }

        public Project(string assemblyName, Guid assemblyGuid, string outputType, string targetFrameworkVersion)
        {
            AssemblyGuid = assemblyGuid;
            AssemblyName = assemblyName;
            OutputType = outputType;
            TargetFrameworkVersion = targetFrameworkVersion;

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
            AssemblyReferences.Add(References.Assemblies.SystemCore);
            AssemblyReferences.Add(References.Assemblies.SystemXmlLinq);
            AssemblyReferences.Add(References.Assemblies.MicrosoftCsharp);
            AssemblyReferences.Add(References.Assemblies.SystemData);
            AssemblyReferences.Add(References.Assemblies.SystemNetHttp);
            AssemblyReferences.Add(References.Assemblies.SystemXml);
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

        public Project WithAssemblyReference(AssemblyReference assemblyReference)
        {
            AssemblyReferences.Add(assemblyReference);
            return this;
        }

        public Project WithNugetPackage(NugetPackage nugetPackage)
        {
            NugetPackages.Add(nugetPackage);
            return this;
        }

        public Project WithProjectReference(ProjectReference projectReference)
        {
            ProjectReferences.Add(projectReference);
            return this;
        }

        public void AddFileToFolder(ProjectFile file, params string[] folderNameHierarchy)
        {
            List<string> folderNames = folderNameHierarchy.ToList();
            IFileContainer fileContainer = this;
            foreach (string folderName in folderNames)
            {
                IFileContainer fileContainerFolder = fileContainer.GetFolders().FirstOrDefault(f => f.FolderName.Equals(folderName));
                // if this file container does not have the folder, create it
                if (fileContainerFolder == null)
                {
                    ProjectFolder newFolder = new ProjectFolder(folderName);

                    fileContainer.AddFolder(newFolder);

                    fileContainerFolder = newFolder;
                }

                // Then, enter it whether it existed previously or not
                fileContainer = fileContainerFolder;
            }
            fileContainer.AddFile(file);
        }

        public void AddFileToFolder(XamlProjectFile file, params string[] folderNameHierarchy)
        {
            this.AddFileToFolder(file.XamlCsFile, folderNameHierarchy);
            this.AddFileToFolder(file.XamlFile, folderNameHierarchy);
        }

        List<ProjectFile> IFileContainer.GetFiles() => _files;

        void IFileContainer.AddFile(ProjectFile file) => _files.Add(file);

        List<ProjectFolder> IFileContainer.GetFolders() => _folders;

        void IFileContainer.AddFolder(ProjectFolder folder) => _folders.Add(folder);

        protected XNamespace xNamespace = "http://schemas.microsoft.com/developer/msbuild/2003";
        internal string GenerateProjectFiles(string solutionDirectoryPath, Guid solutionGuid)
        {
            string csprojDirectoryPath = Path.Combine(solutionDirectoryPath, AssemblyName);
            DirectoryInfo csprojDirectory = Directory.CreateDirectory(csprojDirectoryPath);

            tempCsProjDirectoryPath = csprojDirectoryPath;

            // Do packages.config with all the added nuget packages
            var packageRoot = new XElement("packages");
            ProjectFile packagesConfig = new ProjectFile("packages.config", false, false);
            foreach (NugetPackage package in NugetPackages)
            {
                XElement packageElement =
                        new XElement("package",
                            new XAttribute("id", package.Id),
                            new XAttribute("version", package.Version),
                            new XAttribute("targetFramework", package.TargetFramework)
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
                                                new XText(AssemblyName)
                                                ),
                                            new XElement(xNamespace + "AssemblyName",
                                                new XText(AssemblyName)
                                                ),
                                            new XElement(xNamespace + "TargetFrameworkVersion",
                                                new XText(TargetFrameworkVersion)
                                                ),
                                            new XElement(xNamespace + "FileAlignment",
                                                new XText("512")
                                                ),
                                            GetProjectSpecificPropertyNodes(xNamespace, solutionGuid)
                                        ), // END PROPERTY GROUP
                                        GetBuildConfigurationPropertyGroups(xNamespace),
                                        GetAssemblyReferenceItemGroup(),
                                        GetProjectReferenceItemGroup(),
                                        GetCompileFilesItemGroup(),
                                        GetOtherFileItemGroup(),
                                        GetContentFilesItemGroup(),
                                        GetNoneFilesItemGroup(),
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
            foreach (NugetPackage package in NugetPackages)
            {
                foreach (NugetAssembly assembly in package.Assemblies)
                {
                    XElement packageElement =
                        new XElement(xNamespace + "Reference",
                            new XAttribute("Include", assembly.Include),
                            new XElement(xNamespace + "HintPath",
                                new XText(assembly.HintPath)
                            ),
                            new XElement(xNamespace + "Private",
                                new XText(assembly.IsPrivate.ToString())
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
            List<KeyValuePair<ProjectFile, string>> compilableFiles = tempFileRelativePathDictionary.Where(x => x.Key.ShouldCompile).ToList();
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

            List<KeyValuePair<ProjectFile, string>> embeddedFiles = tempFileRelativePathDictionary.Where(x => x.Key is EmbeddedResourceProjectFile).ToList();
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
            List<KeyValuePair<ProjectFile, string>> contentFiles = tempFileRelativePathDictionary.Where(x => !x.Key.ShouldCompile && x.Key.IsContent).ToList();
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
            List<KeyValuePair<ProjectFile, string>> noneTypeFiles = tempFileRelativePathDictionary.Where(x => !x.Key.ShouldCompile && !x.Key.IsContent
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

        Dictionary<ProjectFile, string> tempFileRelativePathDictionary = new Dictionary<ProjectFile, string>();
        string tempCsProjDirectoryPath;
        private void AddProjectFilesAndFolders(IFileContainer container, string currentPath)
        {
            // Create directory if it doesn't exist
            if (!Directory.Exists(currentPath))
            {
                Directory.CreateDirectory(currentPath);
            }
            // Add files to this directory
            foreach (ProjectFile file in container.GetFiles())
            {
                string filePath = Path.Combine(currentPath, file.FileName);
                File.WriteAllText(filePath, file.FileContents);
                tempFileRelativePathDictionary.Add(file, filePath.Replace(String.Concat(tempCsProjDirectoryPath, @"\"), String.Empty));
            }
            // Go into each folder recursively down the chain creating files and folders
            foreach (ProjectFolder folder in container.GetFolders())
            {
                AddProjectFilesAndFolders(folder, Path.Combine(currentPath, folder.FolderName));
            }
        }
    }
}
