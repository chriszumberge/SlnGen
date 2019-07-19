using SlnGen.Core;
using SlnGen.Core.Code;
using SlnGen.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen.Xamarin.Files
{
    public class AndroidResourceDesignerFile : ProjectFile
    {
        public AndroidResourceDesignerFile(string rootNamespace) : base("Resource.designer.cs", true, false)
        {
            FileContents = ResourceDesignerTemplate.CompileTemplateWithReplacers(
                new List<TemplateFieldReplacer>
                {
                    new TemplateFieldReplacer("RootNamespace", rootNamespace)
                    //{{Field::RootNamespace}}
                }
            );
        }

        private FileTemplate ResourceDesignerTemplate = new FileTemplate(
            new ProjectFile(
                new SGFile("Resource.Designer.cs")
                {
                    Lines = ZESoft.SlnGen.Xamarin.Properties.Resources.AndroidResourceDesignerTemplate.BreakIntoLines().ToList()
                }
            )
        );

        //private static SGFile GetResourceDesignerFile(string rootNamespace)
        //{
        //    return new SGFile("Resource.Designer.cs")
        //    {
        //        Namespaces =
        //        {
        //            new SGNamespace(rootNamespace)
        //            {
        //                Attributes =
        //                {
        //                    new SGAttribute("global::Android.Runtime.ResourceDesignerAttribute", $"\"{rootNamespace}.Resource\"", "IsApplication=true").WithNamespace("assembly")
        //                },
        //                Classes =
        //                {
        //                    new SGClass("Resource", SGAccessibilityLevel.Public, false, false, true)
        //                    {
        //                        Attributes =
        //                        {
        //                            new SGAttribute("System.CodeDom.Compiler.GeneratedCodeAttributes", "\"Xamarin.Android.Build.Tasks\"", "\"1.0.0.0\"")
        //                        },
        //                        Methods =
        //                        {
        //                            new SGMethod(new SGMethodSignature("Resource", SGAccessibilityLevel.None, true))
        //                            {
        //                                Lines = { "global::Android.Runtime.ResourceIdManager.UpdateIdValues();"}
        //                            },
        //                            new SGMethod(new SGMethodSignature("UpdateIdValues", SGAccessibilityLevel.Public, true, false, false, "void"))
        //                            {
        //                                Lines = { $"global::Xamarin.Forms.Platform.Android.Resource.Attribute.actionBarSize = global::{rootNamespace}.Resource.Attribute.actionBarSize;"}
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    };
        //}
    }
}
