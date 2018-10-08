using SlnGen.Core.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen.Xamarin.Factories
{
    public class DefaultAndroidResourceDesignerFactory
    {
        public static SGFile GetFile(string assemblyName)
        {
            return new SGFile("Resource.Designer.cs")
            {
                Namespaces =
                {
                    new SGNamespace(assemblyName)
                    {
                        Classes =
                        {
                            new SGClass("Resource", SGAccessibilityLevel.Public, isPartial: true)
                            {
                                NestedClasses =
                                {
                                    new SGClass("Attribute", SGAccessibilityLevel.Public, isPartial: true)
                                    {
                                        ClassConstructors =
                                        {
                                            new CGClassConstructor(AccessibilityLevel.Private, "Attribute")
                                        }
                                    },
                                    new SGClass("Drawable", SGAccessibilityLevel.Public, isPartial: true)
                                    {
                                        ClassFields =
                                        {
                                            new ConstCGClassField("int", "Icon", "2130837504")
                                        },
                                        ClassConstructors =
                                        {
                                            new CGClassConstructor(AccessibilityLevel.Private, "Drawable")
                                        }
                                    },
                                    new SGClass("Id", SGAccessibilityLevel.Public, isPartial: true)
                                    {
                                        ClassFields =
                                        {
                                            new ConstCGClassField("int", "MyButton", "2131034112")
                                        },
                                        ClassConstructors =
                                        {
                                            new CGClassConstructor(AccessibilityLevel.Private, "Id")
                                        }
                                    },
                                    new PartialCGClass("Layout")
                                    {
                                        ClassFields =
                                        {
                                            new ConstCGClassField("int", "Main", "2130903040")
                                        },
                                        ClassConstructors =
                                        {
                                            new CGClassConstructor(AccessibilityLevel.Private, "Layout")
                                        }
                                    },
                                    new PartialCGClass("String")
                                    {
                                        ClassFields =
                                        {
                                            new ConstCGClassField("int", "ApplicationName", "2130968577"),
                                            new ConstCGClassField("int", "Hello", "2130968576")
                                        },
                                        ClassConstructors =
                                        {
                                            new CGClassConstructor(AccessibilityLevel.Private, "String")
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
