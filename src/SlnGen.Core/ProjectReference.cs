using System;

namespace SlnGen.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class ProjectReference
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }

        public string Include { get; }

        /// <summary>
        /// Gets the project unique identifier.
        /// </summary>
        /// <value>
        /// The project unique identifier.
        /// </value>
        public Guid ProjectGuid { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectReference"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="include">The include.</param>
        /// <param name="projetGuid">The projet unique identifier.</param>
        public ProjectReference(string name, string include, Guid projetGuid)
        {
            Name = name;
            Include = include;
            ProjectGuid = projetGuid;
        }
    }
}
