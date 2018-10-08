using SlnGen.Core.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class ProjectFile
    {
        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; }

        /// <summary>
        /// Gets a value indicating whether [should compile] the file.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [should compile]; otherwise, <c>false</c>.
        /// </value>
        public bool ShouldCompile { get; }

        /// <summary>
        /// Gets a value indicating whether the file is content.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the file is content; otherwise, <c>false</c>.
        /// </value>
        public bool IsContent { get; }

        /// <summary>
        /// Gets or sets the file contents.
        /// </summary>
        /// <value>
        /// The file contents.
        /// </value>
        public string FileContents { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the list of files this file is dependent upon.
        /// </summary>
        /// <value>
        /// The dependent upon files.
        /// </value>
        public List<string> DependentUpon { get; set; } = new List<string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectFile"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="shouldCompile">if set to <c>true</c> [should compile].</param>
        /// <param name="isContent">if set to <c>true</c> [is content].</param>
        /// <param name="fileContents">The file contents.</param>
        public ProjectFile(string fileName, bool shouldCompile = true, bool isContent = false, string fileContents = null)
        {
            FileName = fileName;
            ShouldCompile = shouldCompile;
            IsContent = isContent;
            FileContents = fileContents ?? String.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectFile"/> class.
        /// </summary>
        /// <param name="file">The file.</param>
        public ProjectFile(SGFile file) : 
            this(String.Concat(file.FileName, ".", file.FileExtension), true, false, file.ToString()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectFile"/> class.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="shouldCompile">if set to <c>true</c> [should compile].</param>
        /// <param name="isContent">if set to <c>true</c> [is content].</param>
        public ProjectFile(SGFile file, bool shouldCompile, bool isContent) :
            this(String.Concat(file.FileName, ".", file.FileExtension), shouldCompile, isContent, file.ToString()) { }
    }
}
