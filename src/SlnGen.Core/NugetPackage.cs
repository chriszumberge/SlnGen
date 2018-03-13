using System.Collections.Generic;

namespace SlnGen.Core
{
    public class NugetPackage
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public string Version { get; }

        /// <summary>
        /// Gets the target framework.
        /// </summary>
        /// <value>
        /// The target framework.
        /// </value>
        public string TargetFramework { get; }

        /// <summary>
        /// Gets or sets the nuget package assemblies.
        /// </summary>
        /// <value>
        /// The nuget package assemblies.
        /// </value>
        public List<NugetAssembly> Assemblies { get; set; } = new List<NugetAssembly>();

        /// <summary>
        /// Initializes a new instance of the <see cref="NugetPackage"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="version">The version.</param>
        /// <param name="targetFramework">The target framework.</param>
        public NugetPackage(string id, string version, string targetFramework)
        {
            Id = id;
            Version = version;
            TargetFramework = targetFramework;
        }
    }
}
