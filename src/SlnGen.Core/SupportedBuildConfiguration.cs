using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen.Core
{
    public class SupportedBuildConfiguration
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public string Configuration { get; }

        /// <summary>
        /// Gets the platform.
        /// </summary>
        /// <value>
        /// The platform.
        /// </value>
        public string Platform { get; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="SupportedBuildConfiguration"/> is to be built.
        /// </summary>
        /// <value>
        ///   <c>true</c> if to be built; otherwise, <c>false</c>.
        /// </value>
        public bool Build { get; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="SupportedBuildConfiguration"/> is to be deployed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if to be deployed; otherwise, <c>false</c>.
        /// </value>
        public bool Deploy { get; }

        public SupportedBuildConfiguration(string configuration, string platform, bool build = true, bool deploy = false)
        {
            Configuration = configuration;
            Platform = platform;
            Build = build;
            Deploy = deploy;
        }
    }
}
