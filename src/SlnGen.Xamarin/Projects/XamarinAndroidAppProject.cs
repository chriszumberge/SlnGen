using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using SlnGen.Core;
using SlnGen.Core.Files;
using SlnGen.Core.Utils;
using SlnGen.Xamarin.Files;

namespace SlnGen.Xamarin.Projects
{
    public class XamarinAndroidAppProject : Project
    {
        public XamarinAndroidAppProject(string assemblyName, string packageName, XamarinAndroidPlatform targetFrameworkVersion, int minSdkVersion, int targetSdkVersion, 
            NugetPackage xamarinPackage, string rootNamespace = "") :
            this(assemblyName, packageName, Guid.NewGuid(), targetFrameworkVersion, minSdkVersion, targetSdkVersion, xamarinPackage, rootNamespace)
        { }

        public XamarinAndroidAppProject(string assemblyName, string packageName, Guid assemblyGuid, XamarinAndroidPlatform targetFrameworkVersion, int minSdkVersion, int targetSdkVersion, 
            NugetPackage xamarinPackage, string rootNamespace = "") :
            base(assemblyName, assemblyGuid, "Library", targetFrameworkVersion, rootNamespace)
        {
            _minSdkVersion = minSdkVersion;
            _targetSdkVersion = targetSdkVersion;
            _packageName = packageName;

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

            WithNugetPackage(References.Nuget.Xamarin_Android_Support_Design__27_0_2_1);
            WithNugetPackage(References.Nuget.Xamarin_Android_Support_v7_AppCompat__27_0_2_1);
            WithNugetPackage(References.Nuget.Xamarin_Android_Support_v4__27_0_2_1);
            WithNugetPackage(References.Nuget.Xamarin_Android_Support_v7_CardView__27_0_2_1);
            WithNugetPackage(References.Nuget.Xamarin_Android_Support_v7_MediaRouter__27_0_2_1);

            AddDefaultAssemblyReferences();
            AddDefaultFoldersAndFiles();
        }

        protected int _minSdkVersion;
        protected int _targetSdkVersion;
        protected string _packageName;

        private void AddDefaultAssemblyReferences()
        {
            AssemblyReferences.Add(SlnGen.Xamarin.References.Assemblies.Mono_Android);
            AssemblyReferences.Add(SlnGen.Core.References.Assemblies.System);
            AssemblyReferences.Add(SlnGen.Core.References.Assemblies.System_Core);
            AssemblyReferences.Add(SlnGen.Core.References.Assemblies.System_Xml_Linq);
            AssemblyReferences.Add(SlnGen.Core.References.Assemblies.System_Xml);
        }

        private void AddDefaultFoldersAndFiles()
        {
            Files.Add(new MainActivityFile(AssemblyName, RootNamespace));
            Folders.Add(new ProjectFolder("Properties")
            {
                Files =
                {
                    new AssemblyInfoFile(AssemblyName, AssemblyGuid, new Version(1, 0, 0, 0), new Version(1, 0, 0, 0)),
                    new AndroidManifestFile(AssemblyName, _packageName, _minSdkVersion, _targetSdkVersion)
                }
            });
            Folders.Add(new ProjectFolder("Assets")
            {
                Files =
                {
                    new ProjectFile("AboutAssets.txt", false, false,
                    String.Join(Environment.NewLine,
                    "Any raw assets you want to be deployed with your application can be placed in",
                    "this directory (and child directories) is given a Build Action of \"AndroidAsset\".",
                    "",
                    "These files will be deployed with your package and will be accessible using Android's",
                    "AssetManager, like this:",
                    "",
                    "public class ReadAsset : Activity",
                    "{",
                    "\tprotected override void OnCreate (Bundle bundle)",
                    "\t{",
                    "\t\tbase.OnCreate (bundle);",
                    "",
                    "\t\tInputStream input = Assets.Open (\"my_asset.txt\");",
                    "\t}",
                    "}",
                    "",
                    "Additionally, some Android functions will automatically load asset files:",
                    "",
                    "Typeface tf = Typeface.CreateFromAsset (Context.Assets, \"fonts/samplefont.tff\");"
                    ))
                }
            });
            Folders.Add(new ProjectFolder("Resources")
            {
                Folders =
                {
                    new ProjectFolder("layout")
                    {
                        Files = { }
                    },
                    new ProjectFolder("mipmap-anydpi-v26")
                    {
                        Files ={ }
                    },
                    new ProjectFolder("mipmap-hdpi")
                    {
                        Files = { }
                    },
                    new ProjectFolder("mipmap-mdpi")
                    {
                        Files = { }
                    },
                    new ProjectFolder("mipmap-xhdpi")
                    {
                        Files = { }
                    },
                    new ProjectFolder("mipmap-xxhdpi")
                    {
                        Files = { }
                    },
                    new ProjectFolder("mipmap-xxxhdpi")
                    {
                        Files = { }
                    },
                    new ProjectFolder("values")
                    {
                        Files = { }
                    }
                },
                Files =
                {
                    new ProjectFile("AboutResources.txt", false, false, String.Empty),
                    new AndroidResourceDesignerFile(RootNamespace)
                }
            }.WithFolders("drawable", "drawable-hdpi", "drawable-xhdpi", "drawable-xxhdpi", "drawable-xxxhdpi"));
        }

        protected XNamespace xNamespace = "http://schemas.microsoft.com/developer/msbuild/2003";

        internal override string GenerateProjectFiles(string solutionDirectoryPath, Guid solutionGuid)
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
                                                new XText(DEFAULT_BUILD_PLATFORM)
                                            ),
                                            new XElement(xNamespace + "ProjectGuid",
                                                new XText(String.Concat("{", AssemblyGuid.ToString(), "}"))
                                            ),
                                            new XElement(xNamespace + "ProjectTypeGuids",
                                                new XText($"{{{ProjectTypeGuids.Mono_for_Android}}};{{{ProjectTypeGuids.Cs}}}")
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
                                            new XElement(xNamespace + "AndroidApplication",
                                                new XText("True")
                                            ),
                                            new XElement(xNamespace + "AndroidResgenFile",
                                                new XText(@"Resources\Resource.designer.cs")
                                            ),
                                            new XElement(xNamespace + "AndroidResgenClass",
                                                new XText("Resource")
                                            ),
                                            new XElement(xNamespace + "AndroidManifest",
                                                new XText(@"Properties\AndroidManifest.xml")
                                            ),
                                            new XElement(xNamespace + "MonoAndroidResourcePrefix",
                                                new XText("Resources")
                                            ),
                                            new XElement(xNamespace + "MonoAndroidAssetsPrefix",
                                                new XText("Assets")
                                            ),
                                            new XElement(xNamespace + "AndroidUseLatestPlatformSdk",
                                                new XText("false")
                                            ),
                                            new XElement(xNamespace + "TargetFrameworkVersion",
                                                new XText(TargetFrameworkVersion.TargetVersion)
                                            ),
                                            new XElement(xNamespace + "AndroidHttpClientHandlerType",
                                                new XText("Xamarin.Android.Net.AndroidClientHandler")
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
                                        GetAndroidResourceFilesItemGroup(),
                                        GetProjectFoldersItemGroup(),
                                        //GetCustomFilesItemGroups(xNamespace),
                                        GetImportProjectItems(xNamespace)
                                        //GetTargetItems(xNamespace),
                                        //GetPreBuildEventPropertyGroups(xNamespace),
                                        //GetPostBuildEventPropertyGroups(xNamespace)
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

        protected XElement GetAndroidResourceFilesItemGroup()
        {
            XElement itemGroup = new XElement(xNamespace + "ItemGroup");

            List<KeyValuePair<ProjectFile, string>> androidResourceFiles = _tempFileRelativePathDictionary.Where(x => x.Key is AndroidResourceProjectFile).ToList();
            foreach (KeyValuePair<ProjectFile, string> androidResourceFile in androidResourceFiles)
            {
                AndroidResourceProjectFile typeCastedFile = androidResourceFile.Key as AndroidResourceProjectFile;

                XElement resourceElement =
                    new XElement(xNamespace + "AndroidResource",
                        new XAttribute("Include", androidResourceFile.Value)
                    );
                itemGroup.Add(resourceElement);
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
                    new XAttribute("Project", @"$(MSBuildExtensionsPath)\Xamarin\Android\Xamarin.Android.CSharp.targets")
                )
            };
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
                    new XText("portable")
                    ),
                new XElement(xNamespace + "Optimize",
                    new XText("false")
                    ),
                new XElement(xNamespace + "OutputPath",
                    new XText(@"bin\Debug\")
                    ),
                new XElement(xNamespace + "DefineConstants",
                    new XText("DEBUG;")
                    ),
                new XElement(xNamespace + "ErrorReport",
                    new XText("prompt")
                    ),
                new XElement(xNamespace + "WarningLevel",
                    new XText("4")
                    ),
                new XElement(xNamespace + "AndroidLinkMode",
                    new XText("None")
                    )
                );

        }

        protected XElement GetReleaseAnyCPUPropertyGroup()
        {
            return
            new XElement(xNamespace + "PropertyGroup",
                new XAttribute("Condition", " '$(Configuration)|$(Platform)' == 'Release|AnyCPU' "),
                new XElement(xNamespace + "DebugSymbols",
                    new XText("true")
                    ),
                new XElement(xNamespace + "DebugType",
                    new XText("pdbonly")
                    ),
                new XElement(xNamespace + "Optimize",
                    new XText("true")
                    ),
                new XElement(xNamespace + "OutputPath",
                    new XText(@"bin\Release\")
                    ),
                new XElement(xNamespace + "ErrorReport",
                    new XText("prompt")
                    ),
                new XElement(xNamespace + "WarningLevel",
                    new XText("4")
                    ),
                new XElement(xNamespace + "AndroidManagedSymbols",
                    new XText("true")
                    ),
                new XElement(xNamespace + "AndroidUseSharedRuntime",
                    new XText("false")
                    )
                );
        }
    }
}
