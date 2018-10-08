using SlnGen.Core.Files;
using SlnGen.Core.Interfaces;
using SlnGen.Core.Utils;
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
        /// Gets the default namespace.
        /// </summary>
        /// <value>
        /// The default namespace.
        /// </value>
        public string DefaultNamespace { get; }


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
        public NetPlatform TargetFrameworkVersion { get; }

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

        public Project(string assemblyName, string outputType, NetPlatform targetFrameworkVersion) :
            this(assemblyName, Guid.NewGuid(), outputType, targetFrameworkVersion) { }

        public Project(string assemblyName, Guid assemblyGuid, string outputType, NetPlatform targetFrameworkVersion, string defaultNamespace = "")
        {
            AssemblyGuid = assemblyGuid;
            AssemblyName = assemblyName;
            DefaultNamespace = String.IsNullOrEmpty(defaultNamespace) ? assemblyName : defaultNamespace;
            OutputType = outputType;
            TargetFrameworkVersion = targetFrameworkVersion;
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

        internal abstract string GenerateProjectFiles(string solutionDirectoryPath, Guid solutionGuid);

        protected Dictionary<ProjectFile, string> tempFileRelativePathDictionary = new Dictionary<ProjectFile, string>();
        protected string tempCsProjDirectoryPath;
        protected void AddProjectFilesAndFolders(IFileContainer container, string currentPath)
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
