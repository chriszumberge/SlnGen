using SlnGen.Core.Code;
using SlnGen.Core.Files;
using System.IO;
using System.Xml.Linq;

namespace SlnGen.Xamarin.Files
{
    public class DefaultAppXamlFile : XamlProjectFile
    {
        public DefaultAppXamlFile(string namespaceName) : base("App")
        {
            XNamespace @namespace = "http://xamarin.com/schemas/2014/forms";
            XNamespace xNamespace = "http://schemas.microsoft.com/winfx/2009/xaml";

            XElement rootNode = new XElement(@namespace + "Application",
                    //new XAttribute("xmlns", "http://xamarin.com/schemas/2014/forms"),
                    //new XAttribute("xmlns", "http://schemas.microsoft.com/winfx/2009/xaml"),
                    new XAttribute(xNamespace + "Class", $"{namespaceName}.App"),
                    new XElement(@namespace + "Application.Resources"
                    )
                );

            using (var memoryStream = new MemoryStream())
            {
                rootNode.Save(memoryStream);

                memoryStream.Position = 0;
                using (var streamReader = new StreamReader(memoryStream))
                {
                    XamlFileContents = streamReader.ReadToEnd();
                }
            }

            SGFile file = new SGFile("App.xaml.cs")
            {
                AssemblyReferences =
                {
                    new SGAssemblyReference("System"),
                    new SGAssemblyReference("Xamarin.Forms"),
                    new SGAssemblyReference("Xamarin.Forms.Xaml")
                },
                Namespaces =
                {
                    new SGNamespace(namespaceName)
                    {
                        Attributes =
                        {
                            new SGAttribute("XamlCompilation", "XamlCompilationOptions.Compile").WithNamespace("assembly")
                        },
                        Classes =
                        {
                            new SGClass("App", SGAccessibilityLevel.Public, false, false, true)
                            {
                                Constructors =
                                {
                                    new SGClassConstructor("App", SGAccessibilityLevel.Public)
                                    {
                                        Lines =
                                        {
                                            "InitializeComponent();",
                                            "",
                                            "MainPage = new MainPage();"
                                        }
                                    }
                                },
                                Methods =
                                {
                                    new SGMethod(new SGMethodSignature("OnStart", SGAccessibilityLevel.Protected, isOverride: true, returnType: "void"))
                                    {
                                        Lines = { "// Handle when your app starts" }
                                    },
                                    new SGMethod(new SGMethodSignature("OnSleep", SGAccessibilityLevel.Protected, isOverride: true, returnType: "void"))
                                    {
                                        Lines = { "// Handle when your app sleeps" }
                                    },
                                    new SGMethod(new SGMethodSignature("OnResume", SGAccessibilityLevel.Protected, isOverride: true, returnType: "void"))
                                    {
                                        Lines = { "// Handle when your app resumes" }
                                    }
                                }
                            }.WithBaseClass("Application")
                        }
                    }
                }
            };

            XamlCsFileContents = file.ToString();
        }
    }
}
