using SlnGen.Core;
using SlnGen.Core.Code;
using System.Collections.Generic;

namespace SlnGen.Xamarin.Files
{
    public class AppDelegateFile : ProjectFile
    {
        static string s_fileName = "AppDelegate.cs";
        string _rootNamespace;
        public AppDelegateFile(string rootNamespace) : base(s_fileName, true, false)
        {
            _rootNamespace = rootNamespace;
        }

        List<SGAssemblyReference> _assemblyReferences = new List<SGAssemblyReference>()
        {
            new SGAssemblyReference("System"),
            new SGAssemblyReference("System.Collections.Generic"),
            new SGAssemblyReference("System.Linq"),
            new SGAssemblyReference("Foundation"),
            new SGAssemblyReference("UIKit")
        };

        public AppDelegateFile WithAssembly(SGAssemblyReference assemblyReference)
        {
            if (!_assemblyReferences.Contains(assemblyReference))
            {
                _assemblyReferences.Add(assemblyReference);
            }
            return this;
        }

        List<string> _initializationCode = new List<string>();
        public AppDelegateFile WithInitializationCode(params string[] codeLines)
        {
            _initializationCode.AddRange(codeLines);
            return this;
        }

        List<SGMethod> _additionalMethods = new List<SGMethod>();
        public AppDelegateFile WithAdditionalMethod(SGMethod method)
        {
            _additionalMethods.Add(method);
            return this;
        }

        public AppDelegateFile Build()
        {
            List<string> methodLines = new List<string>
            {
                "global::Xamarin.Forms.Forms.Init();",
                ""
            };
            methodLines.AddRange(_initializationCode);
            methodLines.AddRange(new List<string>
            {
                "",
                "LoadApplication(new App());",
                "",
                "return base.FinishedLaunching(app, options);"
            });

            List<SGMethod> methods = new List<SGMethod>
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
                    //Comments = 
                    Lines = methodLines
                }
            };
            methods.AddRange(_additionalMethods);

            FileContents = new SGFile(s_fileName)
            {
                AssemblyReferences = _assemblyReferences,
                Namespaces =
                {
                    new SGNamespace(_rootNamespace)
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
                                Methods = methods
                            }
                        }
                    }
                }
            }.ToString();

            return this;
        }
    }
}
