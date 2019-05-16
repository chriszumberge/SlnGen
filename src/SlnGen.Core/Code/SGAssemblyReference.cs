///
/// SGAssemblyReference.cs
/// 
/// Author:
///     Chris Zumberge <chriszumberge@gmail.com>
///     
/// 02/06/2018
/// 
using System;
using System.Collections.Generic;

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

        public SGAssemblyReference(params string[] assemblyPath)
        {
            AssemblyName = String.Join(".", assemblyPath);
        }

        public SGAssemblyReference(Project project)
        {
            AssemblyName = project.RootNamespace;
        }

        public override string ToString()
        {
            return $"using {AssemblyName};";
        }

        public override bool Equals(object obj)
        {
            var reference = obj as SGAssemblyReference;
            return reference != null &&
                   AssemblyName == reference.AssemblyName;
        }

        public override int GetHashCode()
        {
            return -1184256330 + EqualityComparer<string>.Default.GetHashCode(AssemblyName);
        }
    }
}
