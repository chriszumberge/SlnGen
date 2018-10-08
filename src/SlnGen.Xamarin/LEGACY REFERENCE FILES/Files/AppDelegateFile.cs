using SlnGen.Core;
using SlnGen.Core.Code;
using System;

namespace SlnGen.Xamarin.Files
{
    public sealed class AppDelegateFile : ProjectFile
    {
        public AppDelegateFile(string assemblyName) : base("AppDelegate.cs", true, false)
        {
            SGFile file = new SGFile("AppDelegate.cs")
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
                    new SGNamespace(assemblyName)
                    {
                        Classes =
                        {
                            new SGClass("AppDelegate", SGAccessibilityLevel.Public, false, false, true)
                            {
                                BaseClass = "global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate",
                                ClassComments =
                                {
                                    "The UIApplicationDelegate for the application. This class is responsible for launching the",
                                    "User Interface of the application, as well as listening (and optionally responding) to",
                                    "application events from iOS."
                                },
                                Attributes =
                                {
                                    new SGAttribute("[Register(\"AppDelegate\")]")
                                },
                                Methods =
                                {
                                    new SGMethod(new SGMethodSignature("FinishedLaunching", SGAccessibilityLevel.Public, false, false, true, "bool")
                                    {
                                        Arguments =
                                        {
                                            new SGArgument("UIApplication", "app"),
                                            new SGArgument("NSDictionary", "options")
                                        }
                                    })
                                    {
                                        MethodComments =
                                        {
                                            "",
                                            "This method is invoked when the application has loaded and is ready to run. In this",
                                            "method you should instantiate the window, load the UI into it and then make the window",
                                            "visible.",
                                            "",
                                            "You have 17 seconds to return from this method, or iOS will terminate your application.",
                                            ""
                                        },
                                        MethodText = String.Concat(
                                            "global::Xamarin.Forms.Forms.Init();", Environment.NewLine,
                                            "LoadApplication(new App());", Environment.NewLine,
                                            Environment.NewLine,
                                            "return base.FinishedLaunching(app, options);"
                                        )
                                    }
                                }
                            }
                        }
                    }
                }
            };

            this.FileContents = file.ToString();
        }
    }
}
