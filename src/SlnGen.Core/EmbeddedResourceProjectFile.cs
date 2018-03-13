using SlnGen.Core.Code;
using System;

namespace SlnGen.Core
{
    public class EmbeddedResourceProjectFile : ProjectFile
    {
        /// <summary>
        /// Gets the subtype.
        /// </summary>
        /// <value>
        /// The subtype.
        /// </value>
        public string SubType { get; } = String.Empty;

        /// <summary>
        /// Gets the generator.
        /// </summary>
        /// <value>
        /// The generator.
        /// </value>
        public string Generator { get; } = String.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmbeddedResourceProjectFile"/> class.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="subType">The subtype.</param>
        /// <param name="generator">The generator.</param>
        public EmbeddedResourceProjectFile(SGFile file, string subType, string generator) : base(file)
        {
            SubType = subType;
            Generator = generator;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmbeddedResourceProjectFile"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="subType">The subtype.</param>
        /// <param name="generator">The generator.</param>
        /// <param name="fileContents">The file contents.</param>
        public EmbeddedResourceProjectFile(string fileName, string subType, string generator, string fileContents = null) : base(fileName, false, false, fileContents)
        {
            SubType = subType;
            Generator = generator;
        }
    }
}
