///
/// SGNamespace.cs
/// 
/// Author:
///     Chris Zumberge <chriszumberge@gmail.com>
///     
/// 02/06/2018
/// 
using SlnGen.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlnGen.Core.Code
{
    public sealed class SGNamespace
    {
        public string NamespaceName { get; set; }

        public List<SGInterface> Interfaces { get; set; } = new List<SGInterface>();

        public List<SGClass> Classes { get; set; } = new List<SGClass>();

        public List<SGEnum> Enums { get; set; } = new List<SGEnum>();

        public List<SGStruct> Structs { get; set; } = new List<SGStruct>();

        public SGNamespace(string namespaceName)
        {
            if (namespaceName == null)
            {
                throw new ArgumentNullException(nameof(namespaceName));
            }
            if (namespaceName.Length == 0)
            {
                throw new ArgumentException("Argument cannot be an empty string.", nameof(namespaceName));
            }

            NamespaceName = namespaceName.Replace(" ", ".");
        }

        public SGNamespace WithInterfaces(params SGInterface[] interfaces)
        {
            Interfaces.AddRange(interfaces);
            return this;
        }

        public SGNamespace WithClasses(params SGClass[] classes)
        {
            Classes.AddRange(classes);
            return this;
        }

        public SGNamespace WithEnums(params SGEnum[] enums)
        {
            Enums.AddRange(enums);
            return this;
        }

        public SGNamespace WithStructs(params SGStruct[] structs)
        {
            Structs.AddRange(structs);
            return this;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"namespace {NamespaceName}");
            sb.AppendLine("{");
            foreach (string interfaceLine in Interfaces.BreakIntoLines())
            {
                sb.AppendLine($"\t{interfaceLine}");
            }
            foreach (string classLine in Classes.BreakIntoLines())
            {
                sb.AppendLine($"\t{classLine}");
            }
            foreach (string enumLine in Enums.BreakIntoLines())
            {
                sb.AppendLine($"\t{enumLine}");
            }
            foreach (string structLine in Structs.BreakIntoLines())
            {
                sb.AppendLine($"\t{structLine}");
            }
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
