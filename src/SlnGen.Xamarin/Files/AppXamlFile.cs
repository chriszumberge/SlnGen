using SlnGen.Core.Code;
using SlnGen.Core.Files;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace SlnGen.Xamarin.Files
{
    public class AppXamlFile : XamlProjectFile
    {
        string _namespaceName;

        public static XNamespace XamarinNamespace = "http://xamarin.com/schemas/2014/forms";
        public static XNamespace MicrosoftNamespace = "http://schemas.microsoft.com/winfx/2009/xaml";

        public XElement Resources = new XElement(XamarinNamespace + "Application.Resources");

        public AppXamlFile(string namespaceName) : base("App")
        {
            _namespaceName = namespaceName;

            _rootXamlNode = new XElement(XamarinNamespace + "Application",
                    new XAttribute(MicrosoftNamespace + "Class", $"{namespaceName}.App")
                );
        }

        List<SGAssemblyReference> _assemblyReferences = new List<SGAssemblyReference>
        {
            new SGAssemblyReference("System"),
            new SGAssemblyReference("Xamarin.Forms"),
            new SGAssemblyReference("Xamarin.Forms.Xaml")
        };

        public AppXamlFile WithAssembly(SGAssemblyReference assemblyReference)
        {
            if (!_assemblyReferences.Contains(assemblyReference))
            {
                _assemblyReferences.Add(assemblyReference);
            }
            return this;
        }

        List<string> _constructorCode = new List<string>();
        public AppXamlFile WithConstructorCode(params string[] codeLines)
        {
            _constructorCode.AddRange(codeLines);
            return this;
        }

        public SGMethodSignature OnStartMethodSignature { get; } =
            new SGMethodSignature("OnStart", SGAccessibilityLevel.Protected, isOverride: true, returnType: "void");

        List<string> _onStartCode = new List<string>();
        public AppXamlFile WithOnStartCode(params string[] codeLines)
        {
            _onStartCode.AddRange(codeLines);
            return this;
        }

        public SGMethodSignature OnSleepMethodSignature { get; } =
            new SGMethodSignature("OnSleep", SGAccessibilityLevel.Protected, isOverride: true, returnType: "void");

        List<string> _onSleepCode = new List<string>();
        public AppXamlFile WithOnSleepCode(params string[] codeLines)
        {
            _onSleepCode.AddRange(codeLines);
            return this;
        }

        public SGMethodSignature OnResumeMethodSignature { get; } =
            new SGMethodSignature("OnResume", SGAccessibilityLevel.Protected, isOverride: true, returnType: "void");

        List<string> _onResumeCode = new List<string>();
        public AppXamlFile WithOnResumeCode(params string[] codeLines)
        {
            _onResumeCode.AddRange(codeLines);
            return this;
        }

        List<SGMethod> _additionalMethods = new List<SGMethod>();
        public AppXamlFile WithAdditionalMethod(SGMethod method)
        {
            _additionalMethods.Add(method);
            return this;
        }

        XElement _rootXamlNode;
        //SGFile _file;

        public AppXamlFile Build()
        {
            List<string> ctorCode = new List<string>
            {
                "InitializeComponent();",
            };
            ctorCode.AddRange(_constructorCode);
            ctorCode.Add("MainPage = new MainPage();");

            List<string> onStartCode = new List<string> { "// Handle when your app starts" };
            onStartCode.AddRange(_onStartCode);

            List<string> onSleepCode = new List<string> { "// Handle when your app sleeps" };
            onSleepCode.AddRange(_onSleepCode);

            List<string> onResumeCode = new List<string> { "// Handle when your app resumes" };
            onResumeCode.AddRange(_onResumeCode);

            List<SGMethod> methods = new List<SGMethod>
            {
                new SGMethod(OnStartMethodSignature)
                {
                    Lines = onStartCode
                },
                new SGMethod(OnSleepMethodSignature)
                {
                    Lines = onSleepCode
                },
                new SGMethod(OnResumeMethodSignature)
                {
                    Lines = onResumeCode
                }
            };
            methods.AddRange(_additionalMethods);

            SGFile file = new SGFile("App.xaml.cs")
            {
                AssemblyReferences = _assemblyReferences,
                Namespaces =
                {
                    new SGNamespace(_namespaceName)
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
                                        Lines = ctorCode
                                    }
                                },
                                Methods = methods
                            }.WithBaseClass("Application")
                        }
                    }
                }
            };

            _rootXamlNode.Add(Resources);

            using (var memoryStream = new MemoryStream())
            {
                _rootXamlNode.Save(memoryStream);

                memoryStream.Position = 0;
                using (var streamReader = new StreamReader(memoryStream))
                {
                    XamlFileContents = streamReader.ReadToEnd();
                }
            }

            XamlCsFileContents = file.ToString();

            return this;
        }
    }
}
