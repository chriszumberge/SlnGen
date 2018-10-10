using SlnGen.Core;
using SlnGen.Core.Code;

namespace SlnGen.Xamarin.Files
{
    public class iOSMainFile : ProjectFile
    {
        static string s_fileName = "Main.cs";
        public iOSMainFile(string rootNamespace) : base(s_fileName, true, false)
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
                    new SGNamespace(rootNamespace)
                    {
                        Classes =
                        {
                            new SGClass("Application", SGAccessibilityLevel.Public)
                            {
                                Methods =
                                {
                                    new SGMethod(new SGMethodSignature("Main", SGAccessibilityLevel.None, true))
                                    {
                                        //Comments = { }
                                        Lines =
                                        {
                                            "// if you want to use a different Application Delegate class from \"AppDelegate\"",
                                            "// you can specify it here.",
                                            "UIApplication.Main(args, null, \"AppDelegate\""
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }.ToString();
        }
    }
}
