using SlnGen.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SlnGen.Core
{
    public class ProjectFolder : IFileContainer
    {
        public string FolderName { get; }

        List<ProjectFile> _files = new List<ProjectFile>();
        public List<ProjectFile> Files => _files;

        List<ProjectFolder> _folders = new List<ProjectFolder>();
        public List<ProjectFolder> Folders => _folders;

        public ProjectFolder(string folderName)
        {
            FolderName = folderName;
        }

        List<ProjectFile> IFileContainer.GetFiles() => _files;

        void IFileContainer.AddFile(ProjectFile file) => _files.Add(file);

        List<ProjectFolder> IFileContainer.GetFolders() => _folders;

        void IFileContainer.AddFolder(ProjectFolder folder) => _folders.Add(folder);

        public ProjectFolder WithFolders(params ProjectFolder[] folders)
        {
            foreach (var folder in folders)
            {
                (this as IFileContainer).AddFolder(folder);
            }
            return this;
        }

        public ProjectFolder WithFolders(params string[] folderNames)
        {
            foreach (var folderName in folderNames)
            {
                (this as IFileContainer).AddFolder(new ProjectFolder(folderName));
            }
            return this;
        }
    }
}
