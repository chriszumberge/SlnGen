using SlnGen.Core;
using SlnGen.Core.Code;
using System.Collections.Generic;

namespace SlnGen.Xamarin.Files
{
    public class MainActivityFile : ProjectFile
    {
        static string s_fileName = "MainActivity.cs";
        string _appName;
        string _rootNamespace;
        public MainActivityFile(string appName, string rootNamespace) : base(s_fileName)
        {
            _appName = appName;
            _rootNamespace = rootNamespace;
        }

        List<SGAssemblyReference> _assemblyReferences = new List<SGAssemblyReference>()
        {
            new SGAssemblyReference("System"),
            new SGAssemblyReference("Android.App"),
            new SGAssemblyReference("Android.Content.PM"),
            new SGAssemblyReference("Android.Runtime"),
            new SGAssemblyReference("Android.Views"),
            new SGAssemblyReference("Android.Widget"),
            new SGAssemblyReference("Android.OS")
        };

        public MainActivityFile WithAssembly(SGAssemblyReference assemblyReference)
        {
            if (!_assemblyReferences.Contains(assemblyReference))
            {
                _assemblyReferences.Add(assemblyReference);
            }
            return this;
        }

        List<string> _initializationCode = new List<string>();
        public MainActivityFile WithInitializationCode(params string[] codeLines)
        {
            _initializationCode.AddRange(codeLines);
            return this;
        }

        List<SGMethod> _additionalMethods = new List<SGMethod>();
        public MainActivityFile WithAdditionalMethod(SGMethod method)
        {
            _additionalMethods.Add(method);
            return this;
        }

        public MainActivityFile Build()
        {
            List<string> methodLines = new List<string>
            {
                "TabLayoutResource = Resource.Layout.Tabbar;",
                "ToolbarResrouce = Resource.Layout.Toolbar;",
                "",
                "base.OnCreate(savedInstanceState);",
                "global::Xamarin.Forms.Forms.Init(this, savedInstanceState);",
                ""
            };
            methodLines.AddRange(_initializationCode);
            methodLines.AddRange(new List<string>
            {
                "",
                "LoadApplication(new App());"
            });

            List<SGMethod> methods = new List<SGMethod>
            {
                new SGMethod(new SGMethodSignature("OnCreate", SGAccessibilityLevel.Protected, false, false, true, "void")
                {
                    Arguments =
                    {
                        new SGArgument("Bundle", "savedInstanceState")
                    }
                })
                {
                    Lines = methodLines
                }
            };
            methods.AddRange(_additionalMethods);

            FileContents = new SGFile("MainActivity.cs")
            {
                AssemblyReferences = _assemblyReferences,
                Namespaces =
                {
                    new SGNamespace(_rootNamespace)
                    {
                        Classes =
                        {
                            new SGClass("MainActivity", SGAccessibilityLevel.Public)
                            {
                                Attributes =
                                {
                                    new SGAttribute("Activity", $"Label = \"{_appName}\"", "Icon = \"@mipmap/icon\"", "Theme = \"@style/MainTheme\"",
                                                                "MainLauncher = true", "ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation")
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
