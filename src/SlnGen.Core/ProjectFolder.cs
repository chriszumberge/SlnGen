using SlnGen.Core.Interfaces;
using System.Collections.Generic;

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
    }
}
