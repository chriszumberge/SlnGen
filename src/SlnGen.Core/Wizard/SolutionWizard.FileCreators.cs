using SlnGen.Core.Code;

namespace SlnGen.Core.Wizard
{
    public sealed partial class SolutionWizard
    {
        internal static SGFile CreateEmpty_NetFramework_ClassFile(string namespaceName)
        {
            string className = "Class1";

            return new SGFile(className, "cs")
            {
                AssemblyReferences =
                {
                    new SGAssemblyReference("System"),
                    new SGAssemblyReference("System.Collections.Generic"),
                    new SGAssemblyReference("System.Linq"),
                    new SGAssemblyReference("System.Text"),
                    new SGAssemblyReference("System.Threading.Tasks")
                },
                Namespaces =
                {
                    new SGNamespace(namespaceName)
                    {
                        Classes =
                        {
                            new SGClass(className, SGAccessibilityLevel.Public)
                        }
                    }
                }
            };
        }

        internal static SGFile CreateDefault_NetFramework_ConsoleProgram(string namespaceName)
        {
            string className = "Program";

            return new SGFile(className, "cs")
            {
                AssemblyReferences =
                {
                    new SGAssemblyReference("System"),
                    new SGAssemblyReference("System.Collections.Generic"),
                    new SGAssemblyReference("System.Linq"),
                    new SGAssemblyReference("System.Text"),
                    new SGAssemblyReference("System.Threading.Tasks")
                },
                Namespaces =
                {
                    new SGNamespace(namespaceName)
                    {
                        Classes =
                        {
                            new SGClass(className, SGAccessibilityLevel.None)
                            {
                                Methods =
                                {
                                    new SGMethod(new SGMethodSignature("Main", accessibilityLevel: SGAccessibilityLevel.None, isStatic: true, returnType: "void")
                                    {
                                        Arguments =
                                        {
                                            new SGArgument("string[]", "args")
                                        }
                                    })
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
