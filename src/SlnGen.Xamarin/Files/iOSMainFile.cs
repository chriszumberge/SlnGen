using SlnGen.Core;
using SlnGen.Core.Code;

namespace SlnGen.Xamarin.Files
{
    public class iOSMainFile : ProjectFile
    {
        static string s_fileName = "Main.cs";
        string _rootNamespace;
        public iOSMainFile(string rootNamespace) : base(s_fileName, true, false)
        {
            _rootNamespace = rootNamespace;
        }

        public iOSMainFile Build()
        {
            FileContents = new SGFile(s_fileName)
            {
                AssemblyReferences =
                {
                    new SGAssemblyReference("System"),
                    new SGAssemblyReference("System", "Collections", "Generic"),
                    new SGAssemblyReference("System.Linq"),
                    new SGAssemblyReference("Foundation"),
                    new SGAssemblyReference("UIKit")
                },
                Namespaces =
                {
                    new SGNamespace(_rootNamespace)
                    {
                        Classes =
                        {
                            new SGClass("Application", SGAccessibilityLevel.Public)
                            {
                                Methods =
                                {
                                    new SGMethod(new SGMethodSignature("Main", SGAccessibilityLevel.None, true)
                                    {
                                        Arguments =
                                        {
                                            new SGArgument(typeof(string[]), "args")
                                        }
                                    })
                                    {
                                        //Comments = { }
                                        Lines =
                                        {
                                            "// if you want to use a different Application Delegate class from \"AppDelegate\"",
                                            "// you can specify it here.",
                                            "UIApplication.Main(args, null, \"AppDelegate\");"
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }.ToString();

            return this;
        }
    }
}
