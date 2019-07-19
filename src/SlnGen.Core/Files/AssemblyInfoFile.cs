using SlnGen.Core.Code;
using SlnGen.Core.Utils;
using System;
using System.Collections.Generic;

namespace SlnGen.Core.Files
{
    public class AssemblyInfoFile : ProjectFile
    {
        readonly string _assembltyTitle;
        readonly Guid _assemblyGuid;
        readonly Version _assemblyVersion;
        readonly Version _assemblyFileVersion;

        public AssemblyInfoFile(string assemblyTitle, Guid assemblyGuid, Version assemblyVersion, Version assemblyFileVersion) : base("AssemblyInfo.cs", true, false)
        {
            _assembltyTitle = assemblyTitle;
            _assemblyGuid = assemblyGuid;
            _assemblyVersion = assemblyVersion;
            _assemblyFileVersion = assemblyFileVersion;

            Build();
        }

        public AssemblyInfoFile WithAdditionalLines(params string[] lines)
        {
            FileContents = String.Concat(FileContents, String.Join(Environment.NewLine, lines));
            Build();
            return this;
        }

        public AssemblyInfoFile WithAssemblyReference(SGAssemblyReference assemblyReference)
        {
            _assemblyInfoFile.AssemblyReferences.Add(assemblyReference);
            Build();
            return this;
        }

        void Build()
        {
            var assemblyInfoTemplate = new FileTemplate(_assemblyInfoFile);

            FileContents = assemblyInfoTemplate.CompileTemplateWithReplacers(
                new List<TemplateFieldReplacer>
                {
                    new TemplateFieldReplacer("AssemblyTitle", _assembltyTitle),
                    new TemplateFieldReplacer("AssemblyGuid", _assemblyGuid.ToString()),
                    new TemplateFieldReplacer("AssemblyVersion", _assemblyVersion.ToString()),
                    new TemplateFieldReplacer("AssemblyFileVersion", _assemblyFileVersion.ToString())
                });
        }

        SGFile _assemblyInfoFile = new SGFile("AssemblyInfo.cs")
        {
            AssemblyReferences =
            {
                new SGAssemblyReference("System.Reflection"),
                new SGAssemblyReference("System.Runtime.CompilerServices"),
                new SGAssemblyReference("System.Runtime.InteropServices")
            },
            Lines =
            {
                "// General Information about an assembly is controlled through the following ",
                "// set of attributes. Change these attribute values to modify the information",
                "// associated with an assembly.",
                "[assembly: AssemblyTitle(\"{{Field::AssemblyTitle}}\")]",
                "[assembly: AssemblyDescription(\"\")]",
                "[assembly: AssemblyConfiguration(\"\")]",
                "[assembly: AssemblyCompany(\"Microsoft\")]",
                "[assembly: AssemblyProduct(\"{{Field::AssemblyTitle}}\")]",
                "[assembly: AssemblyCopyright(\"Copyright © Microsoft 2014\")]",
                "[assembly: AssemblyTrademark(\"\")]",
                "[assembly: AssemblyCulture(\"\")]",
                "",
                "// Setting ComVisible to false makes the types in this assembly not visible",
                "// to COM components.  If you need to access a type in this assembly from ",
                "// COM, set the ComVisible attribute to true on that type.",
                "[assembly: ComVisible(false)]",
                "",
                "// The following GUID is for the ID of the typelib if this project is exposed to COM",
                "[assembly: Guid(\"{{Field::AssemblyGuid}}\")]",
                "",
                "// Version information for an assembly consists of the following four values:",
                "//",
                "//      Major Version",
                "//      Minor Version",
                "//      Build Number",
                "//      Revision",
                "//",
                "// You can specify all the values or you can default the Revision and Build Numbers ",
                "// by using the '*' as shown below:",
                "[assembly: AssemblyVersion(\"{{Field::AssemblyVersion}}\")]",
                "[assembly: AssemblyFileVersion(\"{{Field::AssemblyFileVersion}}\")]",
                ""
            }
        };
    }
}
