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
        string _assemblyName;
        public string AssemblyName
        {
            get { return _assemblyName; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(AssemblyName), "Cannot set AssemblyName to null.");
                }
                else if (value.Length == 0)
                {
                    throw new ArgumentException("Cannot set AssemblyName to empty string.", nameof(AssemblyName));
                }
                else
                {
                    _assemblyName = value.Replace(" ", String.Empty);
                }
            }
        }

        public SGAssemblyReference(string assemblyName)
        {
            AssemblyName = assemblyName;
        }

        public override string ToString()
        {
            return $"using {AssemblyName};";
        }
    }
}
