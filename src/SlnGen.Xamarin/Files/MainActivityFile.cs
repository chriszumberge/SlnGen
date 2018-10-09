using SlnGen.Core;
using SlnGen.Core.Code;

namespace SlnGen.Xamarin.Files
{
    public class MainActivityFile : ProjectFile
    {
        public MainActivityFile(string appName, string rootNamespace) : base("MainActivity.cs")
        {
            SGFile file = new SGFile("MainActivity.cs")
            {
                AssemblyReferences =
                {
                    new SGAssemblyReference("System"),
                    new SGAssemblyReference("Android.App"),
                    new SGAssemblyReference("Android.Content.PM"),
                    new SGAssemblyReference("Android.Runtime"),
                    new SGAssemblyReference("Android.Views"),
                    new SGAssemblyReference("Android.Widget"),
                    new SGAssemblyReference("Android.OS")
                },
                Namespaces =
                {
                    new SGNamespace(rootNamespace)
                    {
                        Classes =
                        {
                            new SGClass("MainActivity", SGAccessibilityLevel.Public)
                            {
                                Attributes =
                                {
                                    new SGAttribute("Activity", $"Label = \"{appName}\"", "Icon = \"@mipmap/icon\"", "Theme = \"@style/MainTheme\"",
                                                                "MainLauncher = true", "ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation")
                                },
                                Methods =
                                {
                                    new SGMethod(new SGMethodSignature("OnCreate", SGAccessibilityLevel.Protected, false, false, true, "void"))
                                    {
                                        Lines =
                                        {
                                            "TabLayoutResource = Resource.Layout.Tabbar;",
                                            "ToolbarResrouce = Resource.Layout.Toolbar;",
                                            "",
                                            "base.OnCreate(savedInstanceState);",
                                            "global::Xamarin.Forms.Forms.Init(this, savedInstanceState);",
                                            "LoadApplication(new App());"
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
