using SlnGen.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen.Core
{
    public class ProjectFolder : IFileContainer
    {
        readonly string _folderName;
        public string FolderName => _folderName;

        List<ProjectFile> _files = new List<ProjectFile>();
        public List<ProjectFile> Files => _files;

        List<ProjectFolder> _folders = new List<ProjectFolder>();
        public List<ProjectFolder> Folders => _folders;

        public ProjectFolder(string folderName)
        {
            _folderName = folderName;
        }

        List<ProjectFile> IFileContainer.GetFiles() => _files;

        void IFileContainer.AddFile(ProjectFile file) => _files.Add(file);

        List<ProjectFolder> IFileContainer.GetFolders() => _folders;

        void IFileContainer.AddFolder(ProjectFolder folder) => _folders.Add(folder);
    }
}
