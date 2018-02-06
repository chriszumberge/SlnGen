///
/// SGAssemblyReference.cs
/// 
/// Author:
///     Chris Zumberge <chriszumberge@gmail.com>
///     
/// 02/06/2018
/// 
using System;

namespace SlnGen.Core.Code
{
    public sealed class SGAssemblyReference
    {
        public string AssemblyName { get; set; }

        public SGAssemblyReference(string assemblyName)
        {
            if (assemblyName == null)
            {
                throw new ArgumentNullException(nameof(assemblyName));
            }
            if (assemblyName.Length == 0)
            {
                throw new ArgumentException("Argument cannot be an empty string.", nameof(assemblyName));
            }

            AssemblyName = assemblyName.Replace(" ", String.Empty);
        }

        public override string ToString()
        {
            return $"using {AssemblyName};";
        }
    }
}
