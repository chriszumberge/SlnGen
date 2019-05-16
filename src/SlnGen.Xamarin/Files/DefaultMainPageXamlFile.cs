using SlnGen.Core.Code;
using SlnGen.Core.Files;
using System.IO;
using System.Xml.Linq;


namespace SlnGen.Xamarin.Files
{
    public class DefaultMainPageXamlFile : XamlProjectFile
    {
        public DefaultMainPageXamlFile(string namespaceName) : base("MainPage")
        {
            XNamespace @namespace = "http://xamarin.com/schemas/2014/forms";
            XNamespace xNamespace = "http://schemas.microsoft.com/winfx/2009/xaml";
            XNamespace localNamespace = $"clr-namespace:{namespaceName}";

            XElement rootNode = new XElement(@namespace + "ContentPage",
                    //new XAttribute(@namespace+"xmlns", "http://xamarin.com/schemas/2014/forms"),
                    //new XAttribute(xNamespace+"xmlns", "http://schemas.microsoft.com/winfx/2009/xaml"),
                    //new XAttribute(localNamespace+"xmlns", $"clr-namespace:{assemblyName}"),
                    new XAttribute(xNamespace + "Class", $"{namespaceName}.MainPage"),
                    new XElement(@namespace + "StackLayout",
                        new XComment("Place new controls here"),
                        new XElement(@namespace + "Label",
                            new XAttribute("Text", "Welcome to Xamarin Forms!"),
                            new XAttribute("VerticalOptions", "CenterAndExpand"),
                            new XAttribute("HorizontalOptions", "Center")
                        )
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
                    new SGAssemblyReference("System.Collections.Generic"),
                    new SGAssemblyReference("System.Linq"),
                    new SGAssemblyReference("System.Text"),
                    new SGAssemblyReference("System.Threading.Tasks"),
                    new SGAssemblyReference("Xamarin.Forms")
                },
                Namespaces =
                {
                    new SGNamespace(namespaceName)
                    {
                        Classes =
                        {
                            new SGClass("MainPage", SGAccessibilityLevel.Public, isPartial: true)
                            {
                                Constructors =
                                {
                                    new SGClassConstructor("MainPage", SGAccessibilityLevel.Public)
                                    {
                                        Lines = { "InitializeComponent();" }
                                    }
                                }
                            }.WithBaseClass("ContentPage")
                        }
                    }
                }
            };

            XamlCsFileContents = file.ToString();
        }
    }
}
