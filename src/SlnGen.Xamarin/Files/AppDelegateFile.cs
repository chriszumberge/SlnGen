using SlnGen.Core;
using SlnGen.Core.Code;

namespace SlnGen.Xamarin.Files
{
    public class AppDelegateFile : ProjectFile
    {
        static string s_fileName = "AppDelegate.cs";
        public AppDelegateFile(string rootNamespace) : base(s_fileName, true, false)
        {
            FileContents = new SGFile(s_fileName)
            {
                AssemblyReferences =
                {
                    new SGAssemblyReference("System"),
                    new SGAssemblyReference("System.Collections.Generic"),
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
                            new SGClass("AppDelegate", SGAccessibilityLevel.Public, false, false, true)
                            {
                                BaseClass = "global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate",
                                //Comments = 
                                Attributes =
                                {
                                    new SGAttribute("Register", "\"AppDelegate\"")
                                },
                                Methods =
                                {
                                    new SGMethod(new SGMethodSignature("FinishedLaunching", SGAccessibilityLevel.Public, false, false, true, "bool"))
                                    {
                                        //Comments = 
                                        Lines =
                                        {
                                            "global::Xamarin.Forms.Forms.Init();",
                                            "LoadApplication(new App());",
                                            "",
                                            "return base.FinishedLaunching(app, options);"
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
