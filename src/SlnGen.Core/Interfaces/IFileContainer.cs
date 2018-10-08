﻿using System.Collections.Generic;

namespace SlnGen.Core.Interfaces
{
    public interface IFileContainer
    {
        List<ProjectFile> GetFiles();
        void AddFile(ProjectFile file);

        List<ProjectFolder> GetFolders();
        void AddFolder(ProjectFolder folder);
    }
}
