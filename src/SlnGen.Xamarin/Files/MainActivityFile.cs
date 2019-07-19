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

        public SGMethodSignature OnCreateMethodSignature { get; } =
            new SGMethodSignature("OnCreate", SGAccessibilityLevel.Protected, false, false, true, "void")
            {
                Arguments =
                {
                    new SGArgument("Bundle", "savedInstanceState")
                }
            };

        List<string> _onRequestPermissionLines = new List<string>();
        public MainActivityFile WithOnRequestPermissionResultCode(params string[] codeLines)
        {
            _onRequestPermissionLines.AddRange(codeLines);
            return this;
        }

        public SGMethodSignature OnRequestPermissionsResultMethodSignature { get; } =
            new SGMethodSignature("OnRequestPermissionsResult", SGAccessibilityLevel.Public, isOverride: true, returnType: "void")
            {
                Arguments =
                {
                    new SGArgument("int", "requestCode"),
                    new SGArgument("string[]", "permissions"),
                    new SGArgument("[GeneratedEnum] Permission[]", "grantResults")
                }
            };

        public MainActivityFile Build()
        {
            List<string> onCreateMethodLines = new List<string>
            {
                "TabLayoutResource = Resource.Layout.Tabbar;",
                "ToolbarResource = Resource.Layout.Toolbar;",
                "",
                "base.OnCreate(savedInstanceState);",
                "global::Xamarin.Forms.Forms.Init(this, savedInstanceState);",
                ""
            };
            onCreateMethodLines.AddRange(_initializationCode);
            onCreateMethodLines.AddRange(new List<string>
            {
                "",
                "LoadApplication(new App());"
            });

            List<string> onRequestPermissionResultMethodLines = new List<string>();
            onRequestPermissionResultMethodLines.AddRange(_onRequestPermissionLines);
            onRequestPermissionResultMethodLines.AddRange(new List<string>
            {
                "",
                "base.OnRequestPermissionsResult(requestCode, permissions, grantResults);"
            });

            List<SGMethod> methods = new List<SGMethod>
            {
                new SGMethod(OnCreateMethodSignature)
                {
                    Lines = onCreateMethodLines
                },
                new SGMethod(OnRequestPermissionsResultMethodSignature)
                {
                    Lines = onRequestPermissionResultMethodLines
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
                                BaseClass = "global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity",
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
