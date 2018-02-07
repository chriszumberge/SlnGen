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
                    throw new Exception("Cannot set AssemblyName to null.");
                }
                else if (value.Length == 0)
                {
                    throw new Exception("Cannot set AssemblyName to empty string.");
                }
                else
                {
                    _assemblyName = value;
                }
            }
        }

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
