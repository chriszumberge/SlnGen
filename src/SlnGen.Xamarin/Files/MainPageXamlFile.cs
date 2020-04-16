using SlnGen.Core.Code;
using SlnGen.Core.Files;
using System.IO;
using System.Xml.Linq;


namespace SlnGen.Xamarin.Files
{
    public class MainPageXamlFile : XamlProjectFile
    {
        public MainPageXamlFile(string namespaceName) : base("MainPage")
        {
            XNamespace @namespace = "http://xamarin.com/schemas/2014/forms";
            XNamespace xNamespace = "http://schemas.microsoft.com/winfx/2009/xaml";
            XNamespace localNamespace = $"clr-namespace:{namespaceName}";

            _rootXamlNode = new XElement(@namespace + "ContentPage",
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

            _file = new SGFile("MainPage.xaml.cs")
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
        }

        XElement _rootXamlNode;
        SGFile _file;

        public MainPageXamlFile Build()
        {
            using (var memoryStream = new MemoryStream())
            {
                _rootXamlNode.Save(memoryStream);

                memoryStream.Position = 0;
                using (var streamReader = new StreamReader(memoryStream))
                {
                    XamlFileContents = streamReader.ReadToEnd();
                }
            }

            XamlCsFileContents = _file.ToString();

            return this;
        }
    }
}
