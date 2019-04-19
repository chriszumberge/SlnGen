using SlnGen.Core;
using SlnGen.Core.Files;
using SlnGen.Core.Utils;
using SlnGen.Xamarin.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace SlnGen.Xamarin.Projects
{
    public class XamariniOSAppProject : Project
    {
        public XamariniOSAppProject(string assemblyName, string appName, string bundleIdentifier, XamariniOSPlatform targetFrameworkVersion, NugetPackage xamarinPackage, string rootNamespace = "") :
            this(assemblyName, appName, bundleIdentifier, new Guid(), targetFrameworkVersion, xamarinPackage, rootNamespace)
        { }

        public XamariniOSAppProject(string assemblyName, string appName, string bundleIdentifier, Guid assemblyGuid, XamariniOSPlatform targetFrameworkVersion, NugetPackage xamarinPackage, string rootNamespace = "") :
            base(assemblyName, assemblyGuid, "Exe", targetFrameworkVersion, rootNamespace)
        {
            // project specific fields
            _appName = appName;
            _bundleIdentifier = bundleIdentifier;

            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Ad-Hoc", "Any CPU"));
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Ad-Hoc", "iPhone"));
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Ad-Hoc", "iPhoneSimulator"));
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("AppStore", "Any CPU"));
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("AppStore", "iPhone"));
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("AppStore", "iPhoneSimulator"));
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Debug", "Any CPU"));
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Debug", "iPhone"));
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Debug", "iPhoneSimulator"));
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Release", "Any CPU"));
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Release", "iPhone"));
            SupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Release", "iPhoneSimulator"));

            WithNugetPackage(xamarinPackage);

            AddDefaultAssemblyReferences();
            AddDefaultFoldersAndFiles();
        }

        protected string _appName;
        protected string _bundleIdentifier;

        private void AddDefaultAssemblyReferences()
        {
            AssemblyReferences.Add(Core.References.Assemblies.System);
            AssemblyReferences.Add(Core.References.Assemblies.System_Xml);
            AssemblyReferences.Add(Core.References.Assemblies.System_Core);
            AssemblyReferences.Add(Xamarin.References.Assemblies.Xamarin_iOS);
        }

        private void AddDefaultFoldersAndFiles()
        {
            Folders.Add(new ProjectFolder("Assets.xcassets")
            {
                Folders =
                {
                    new ProjectFolder("AppIcon.appiconset")
                    {
                        Files = { }
                    }
                }
            });
            Folders.Add(new ProjectFolder("Properties")
            {
                Files =
                {
                    new AssemblyInfoFile(AssemblyName, AssemblyGuid, new Version(1, 0, 0, 0), new Version(1, 0, 0, 0))
                }
            });
            Folders.Add(new ProjectFolder("Resources")
            {
                Files =
                {

                }
            });
            Files.Add(new AppDelegateFile(RootNamespace));
            Files.Add(new EntitlementsPListFile());
            Files.Add(new InfoPListFile(_appName, _bundleIdentifier));
            Files.Add(new iOSMainFile(RootNamespace));
        }

        protected XNamespace xNamespace = "http://schemas.microsoft.com/developer/msbuild/2003";

        protected override string GenerateProjectFiles(string solutionDirectoryPath, Guid solutionGuid)
        {
            string csprojDirectoryPath = Path.Combine(solutionDirectoryPath, AssemblyName);
            DirectoryInfo csprojDirectory = Directory.CreateDirectory(csprojDirectoryPath);

            tempCsProjDirectoryPath = csprojDirectoryPath;

            AddProjectFilesAndFolders(this, csprojDirectoryPath);

            var xmlNode = new XElement(xNamespace + "Project",
                                        new XAttribute("ToolsVersion", "4.0"),
                                        new XAttribute("DefaultTargets", "Build"),
                                        new XAttribute("xmlns", "http://schemas.microsoft.com/developer/msbuild/2003"),
                                        new XElement(xNamespace + "PropertyGroup",
                                            new XElement(xNamespace + "Configuration",
                                                new XAttribute("Condition", " '$(Configuration)' == '' "),
                                                new XText(DEFAULT_BUILD_CONFIGURATION)
                                            ),
                                            new XElement(xNamespace + "Platform",
                                                new XAttribute("Condition", " '$(Platform)' == '' "),
                                                new XText("iPhoneSimulator")
                                            ),
                                            new XElement(xNamespace + "ProductVersion",
                                                new XText(TargetFrameworkVersion.TargetVersion)
                                            ),
                                            new XElement(xNamespace + "SchemaVersion",
                                                new XText(((XamariniOSPlatform)TargetFrameworkVersion).SchemaVersion)
                                            ),
                                            new XElement(xNamespace + "ProjectGuid",
                                                new XText(String.Concat("{", AssemblyGuid.ToString(), "}"))
                                            ),
                                            new XElement(xNamespace + "ProjectTypeGuids",
                                                new XText($"{{{ProjectTypeGuids.Xamarin_iOS}}};{{{ProjectTypeGuids.Cs}}}")
                                            ),
                                            //new XElement(xNamespace + "TemplateGuid"
                                            //),
                                            new XElement(xNamespace + "OutputType",
                                                new XText(OutputType)
                                            ),
                                            new XElement(xNamespace + "RootNamespace",
                                                new XText(RootNamespace)
                                            ),
                                            new XElement(xNamespace + "AssemblyName",
                                                new XText(AssemblyName)
                                            ),
                                            new XElement(xNamespace + "IPhoneResourcePrefix",
                                                new XText("Resources")
                                            ),
                                            new XElement(xNamespace + "MtouchHttpClientHandler",
                                                new XText("NSUrlSessionHandler")
                                            )
                                        ), // END PROPERTY GROUP
                                        GetBuildConfigurationPropertyGroups(xNamespace),
                                        GetAssemblyReferenceItemGroup(),
                                        GetNugetReferenceItemGroup(),
                                        GetProjectReferenceItemGroup(),
                                        GetCompileFilesItemGroup(),
                                        GetOtherFileItemGroup(),
                                        GetContentFilesItemGroup(),
                                        GetNoneFilesItemGroup(),
                                        //GetImageAssetFilesItemGroup(),
                                        GetProjectFoldersItemGroup(),
                                        GetImportProjectItems(xNamespace)
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
            if (config.Platform.Equals("iPhoneSimulator") && config.Configuration.Equals("Debug"))
            {
                return
                new XElement(xNamespace + "PropertyGroup",
                    new XAttribute("Condition", " '$(Configuration)|$(Platform)' == 'Debug|iPhoneSimulator' "),
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
                        new XText(@"bin\iPhoneSimulator\Debug")
                        ),
                    new XElement(xNamespace + "DefineConstants",
                        new XText("DEBUG")
                        ),
                    new XElement(xNamespace + "ErrorReport",
                        new XText("prompt")
                        ),
                    new XElement(xNamespace + "WarningLevel",
                        new XText("4")
                        ),
                    new XElement(xNamespace + "ConsolePause",
                        new XText("false")
                        ),
                    new XElement(xNamespace + "MtouchArch",
                        new XText("x86_64")
                        ),
                    new XElement(xNamespace + "MtouchLink",
                        new XText("None")
                        ),
                    new XElement(xNamespace + "MtouchDebug",
                        new XText("true")
                        )
                    );
            }
            else if (config.Platform.Equals("iPhoneSimulator") && config.Configuration.Equals("Release"))
            {
                return
                new XElement(xNamespace + "PropertyGroup",
                    new XAttribute("Condition", " '$(Configuration)|$(Platform)' == 'Release|iPhoneSimulator' "),
                    new XElement(xNamespace + "DebugType",
                        new XText("none")
                        ),
                    new XElement(xNamespace + "Optimize",
                        new XText("true")
                        ),
                    new XElement(xNamespace + "OutputPath",
                        new XText(@"bin\iPhoneSimulator\Release")
                        ),
                    new XElement(xNamespace + "ErrorReport",
                        new XText("prompt")
                        ),
                    new XElement(xNamespace + "WarningLevel",
                        new XText("4")
                        ),
                    new XElement(xNamespace + "ConsolePause",
                        new XText("false")
                        ),
                    new XElement(xNamespace + "MtouchArch",
                        new XText("x86_64")
                        ),
                    new XElement(xNamespace + "MtouchLink",
                        new XText("None")
                        )
                    );
            }
            else if (config.Platform.Equals("iPhone") && config.Configuration.Equals("Debug"))
            {
                return
                new XElement(xNamespace + "PropertyGroup",
                    new XAttribute("Condition", " '$(Configuration)|$(Platform)' == 'Debug|iPhone' "),
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
                        new XText(@"bin\iPhone\Debug")
                        ),
                    new XElement(xNamespace + "DefineConstants",
                        new XText("DEBUG")
                        ),
                    new XElement(xNamespace + "ErrorReport",
                        new XText("prompt")
                        ),
                    new XElement(xNamespace + "WarningLevel",
                        new XText("4")
                        ),
                    new XElement(xNamespace + "ConsolePause",
                        new XText("false")
                        ),
                    new XElement(xNamespace + "MtouchArch",
                        new XText("ARM64")
                        ),
                    new XElement(xNamespace + "CodesignKey",
                        new XText("iPhone Developer")
                        ),
                    new XElement(xNamespace + "MtouchDebug",
                        new XText("true")
                        ),
                    new XElement(xNamespace + "CodesignEntitlements",
                        new XText("Entitlements.plist")
                        )
                    );
            }
            else if (config.Platform.Equals("iPhone") && config.Configuration.Equals("Release"))
            {
                return
                new XElement(xNamespace + "PropertyGroup",
                    new XAttribute("Condition", " '$(Configuration)|$(Platform)' == 'Release|iPhone' "),
                    new XElement(xNamespace + "DebugType",
                        new XText("none")
                        ),
                    new XElement(xNamespace + "Optimize",
                        new XText("true")
                        ),
                    new XElement(xNamespace + "OutputPath",
                        new XText(@"bin\iPhone\Release")
                        ),
                    new XElement(xNamespace + "ErrorReport",
                        new XText("prompt")
                        ),
                    new XElement(xNamespace + "WarningLevel",
                        new XText("4")
                        ),
                    new XElement(xNamespace + "MtouchArch",
                        new XText("ARM64")
                        ),
                    new XElement(xNamespace + "ConsolePause",
                        new XText("false")
                        ),
                    new XElement(xNamespace + "CodesignKey",
                        new XText("iPhone Developer")
                        ),
                    new XElement(xNamespace + "CodesignEntitlements",
                        new XText("Entitlements.plist")
                        )
                    );
            }
            else if (config.Platform.Equals("iPhone") && config.Configuration.Equals("Ad-Hoc"))
            {
                return
                new XElement(xNamespace + "PropertyGroup",
                    new XAttribute("Condition", " '$(Configuration)|$(Platform)' == 'Ad-Hoc|iPhone' "),
                    new XElement(xNamespace + "DebugType",
                        new XText("none")
                        ),
                    new XElement(xNamespace + "Optimize",
                        new XText("True")
                        ),
                    new XElement(xNamespace + "OutputPath",
                        new XText(@"bin\iPhone\Ad-Hoc")
                        ),
                    new XElement(xNamespace + "ErrorReport",
                        new XText("prompt")
                        ),
                    new XElement(xNamespace + "WarningLevel",
                        new XText("4")
                        ),
                    new XElement(xNamespace + "MtouchArch",
                        new XText("ARM64")
                        ),
                    new XElement(xNamespace + "ConsolePause",
                        new XText("false")
                        ),
                    new XElement(xNamespace + "CodesignKey",
                        new XText("iPhone Distribution")
                        ),
                    new XElement(xNamespace + "CodesignEntitlements",
                        new XText("Entitlements.plist")
                        )
                    );
            }
            else if (config.Platform.Equals("iPhone") && config.Configuration.Equals("AppStore"))
            {
                return
                new XElement(xNamespace + "PropertyGroup",
                    new XAttribute("Condition", " '$(Configuration)|$(Platform)' == 'AppStore|iPhone' "),
                    new XElement(xNamespace + "DebugType",
                        new XText("none")
                        ),
                    new XElement(xNamespace + "Optimize",
                        new XText("True")
                        ),
                    new XElement(xNamespace + "OutputPath",
                        new XText(@"bin\iPhone\AppStore")
                        ),
                    new XElement(xNamespace + "ErrorReport",
                        new XText("prompt")
                        ),
                    new XElement(xNamespace + "WarningLevel",
                        new XText("4")
                        ),
                    new XElement(xNamespace + "ConsolePause",
                        new XText("false")
                        ),
                    new XElement(xNamespace + "MtouchArch",
                        new XText("ARM64")
                        ),
                    new XElement(xNamespace + "CodesignProvision",
                        new XText("Automatic:AppStore")
                        ),
                    new XElement(xNamespace + "CodesignKey",
                        new XText("iPhone Distribution")
                        ),
                    new XElement(xNamespace + "CodesignEntitlements",
                        new XText("Entitlements.plist")
                        )
                    );
            }
            else
            {
                return null;
            }
        }

        protected XElement GetAssemblyReferenceItemGroup()
        {
            XElement itemGroup = new XElement(xNamespace + "ItemGroup");
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

        protected XElement GetNugetReferenceItemGroup()
        {
            XElement itemGroup = new XElement(xNamespace + "ItemGroup");
            foreach (NugetPackage nugetPackage in NugetPackages)
            {
                XElement packageElement =
                    new XElement(xNamespace + "PackageReference",
                        new XAttribute("Include", nugetPackage.Id),
                        new XAttribute("Version", nugetPackage.Version)
                    );

                itemGroup.Add(packageElement);
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

        /// <remarks>
        /// Probably should not call base when overriding this method. If you're overriding it there's a chance you do not want the default features.
        /// </remarks>
        protected virtual XElement[] GetImportProjectItems(XNamespace xNamespace)
        {
            return new XElement[] {
                new XElement(xNamespace + "Import",
                    new XAttribute("Project", @"$(MSBuildExtensionsPath)\Xamarin\iOS\Xamarin.iOS.CSharp.targets")
                )
            };
        }
    }
}
