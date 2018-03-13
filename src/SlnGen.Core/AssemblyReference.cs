using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen.Core
{
    public class AssemblyReference
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }

        /// <summary>
        /// Gets the hint path.
        /// </summary>
        /// <value>
        /// The hint path.
        /// </value>
        public string HintPath { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is private.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is private; otherwise, <c>false</c>.
        /// </value>
        public bool IsPrivate { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyReference"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public AssemblyReference(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyReference"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="hintPath">The hint path.</param>
        /// <param name="isPrivate">if set to <c>true</c> [is private].</param>
        public AssemblyReference(string name, string hintPath, bool isPrivate)
        {
            Name = name;
            HintPath = hintPath;
            IsPrivate = isPrivate;
        }
    }
}
