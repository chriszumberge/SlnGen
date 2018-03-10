using SlnGen.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlnGen.Core.Code
{
    public class SGClass
    {
        string _className;
        public string ClassName
        {
            get { return _className; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(ClassName), "Cannot set ClassName to null.");
                }
                else if (value.Length == 0)
                {
                    throw new ArgumentException("Cannot set ClassName to empty string.", nameof(ClassName));
                }
                else
                {
                    _className = value.Replace(" ", "_");
                }
            }
        }

        SGAccessibilityLevel _accessibilityLevel;
        public SGAccessibilityLevel AccessibilityLevel
        {
            get { return _accessibilityLevel; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(AccessibilityLevel), "Cannot set AccessibilityLevel to null.");
                }
                else
                {
                    _accessibilityLevel = value;
                }
            }
        }

        public bool IsAbstract { get; set; }
        public bool IsStatic { get; set; }
        public bool IsPartial { get; set; }

        public List<string> InterfaceImplementations { get; set; } = new List<string>();

        public string BaseClass { get; set; } = null;

        public bool IsGeneric => (GenericTypeNames?.Count ?? 0) > 0;
        List<string> _genericTypeNames = new List<string>();
        public List<string> GenericTypeNames
        {
            get { return _genericTypeNames; }
            set
            {
                if (value == null)
                {
                    _genericTypeNames = new List<string>();
                }
                else
                {
                    _genericTypeNames = value;
                }
            }
        }

        public List<SGClassProperty> Properties { get; set; } = new List<SGClassProperty>();

        public List<SGClassField> Fields { get; set; } = new List<SGClassField>();

        public List<SGClassConstructor> Constructors { get; set; } = new List<SGClassConstructor>();

        public List<SGMethod> Methods { get; set; } = new List<SGMethod>();

        public List<SGAttribute> Attributes { get; set; } = new List<SGAttribute>();

        public List<SGClass> NestedClasses { get; set; } = new List<SGClass>();

        public List<SGInterface> NestedInterfaces { get; set; } = new List<SGInterface>();

        public List<SGEnum> NestedEnums { get; set; } = new List<SGEnum>();

        public SGClass(string className, SGAccessibilityLevel accessibilityLevel = null, bool isAbstract = false, bool isStatic = false, bool isPartial = false)
        {
            ClassName = className;
            AccessibilityLevel = accessibilityLevel ?? SGAccessibilityLevel.Private;
            IsAbstract = isAbstract;
            IsStatic = isStatic;
            IsPartial = isPartial;
        }

        public SGClass WithAccessibilityLevel(SGAccessibilityLevel newAccessibilityLevel)
        {
            AccessibilityLevel = newAccessibilityLevel;
            return this;
        }

        public SGClass WithIsAbstract(bool newAbstractValue)
        {
            IsAbstract = newAbstractValue;
            return this;
        }

        public SGClass WithIsStatic(bool newStaticValue)
        {
            IsStatic = newStaticValue;
            return this;
        }

        public SGClass WithIsPartial(bool newPartialValue)
        {
            IsPartial = newPartialValue;
            return this;
        }

        public SGClass WithImplementations(params string[] interfaceImplementations)
        {
            if (interfaceImplementations.Any(x => String.IsNullOrEmpty(x)))
            {
                throw new ArgumentException("Interface Implementations cannot be null or empty strings.");
            }
            InterfaceImplementations.AddRange(interfaceImplementations);
            return this;
        }

        public SGClass WithImplementations(params SGInterface[] interfaceImplementations)
        {
            return this.WithImplementations(interfaceImplementations.Select(x => x.InterfaceName).ToArray());
        }

        public SGClass WithBaseClass(string baseClassName)
        {
            if (String.IsNullOrEmpty(baseClassName))
            {
                throw new ArgumentException("Base Class Name cannot be null or empty string.");
            }
            BaseClass = baseClassName;
            return this;
        }

        public SGClass WithBaseClass(SGClass newBaseClass)
        {
            if (newBaseClass == null)
            {
                throw new ArgumentNullException(nameof(newBaseClass), "Base Class cannot be null.");
            }
            BaseClass = newBaseClass.ClassName;
            return this;
        }

        public SGClass WithGenericTypeNames(params string[] genericTypeNames)
        {
            if (genericTypeNames.Any(x => String.IsNullOrEmpty(x)))
            {
                throw new ArgumentException("Generic Type Names cannot be null or empty strings.");
            }

            GenericTypeNames.AddRange(genericTypeNames);
            return this;
        }

        public SGClass WithProperties(params SGClassProperty[] properties)
        {
            if (properties == null || properties.Any(x => x == null))
            {
                throw new ArgumentNullException("Properties cannot be null.");
            }

            Properties.AddRange(properties);
            return this;
        }

        public SGClass WithFields(params SGClassField[] fields)
        {
            if (fields == null || fields.Any(x => x == null))
            {
                throw new ArgumentNullException("Fields cannot be null.");
            }

            Fields.AddRange(fields);
            return this;
        }

        public SGClass WithConstructors(params SGClassConstructor[] constructors)
        {
            if (constructors == null || constructors.Any(x => x == null))
            {
                throw new ArgumentNullException("Constructors cannot be null.");
            }

            Constructors.AddRange(constructors);
            return this;
        }

        public SGClass WithMethods(params SGMethod[] methods)
        {
            if (methods == null || methods.Any(x => x == null))
            {
                throw new ArgumentNullException("Methods cannot be null.");
            }

            Methods.AddRange(methods);
            return this;
        }

        public SGClass WithAttributes(params SGAttribute[] attributes)
        {
            if (attributes == null || attributes.Any(x => x == null))
            {
                throw new ArgumentNullException("Attributes cannot be null.");
            }

            Attributes.AddRange(attributes);
            return this;
        }

        public SGClass WithNestedClasses(params SGClass[] nestedClasses)
        {
            if (nestedClasses == null || nestedClasses.Any(x => x == null))
            {
                throw new ArgumentNullException("Nested Classes cannot be null.");
            }

            NestedClasses.AddRange(nestedClasses);
            return this;
        }

        public SGClass WithNestedInterfaces(params SGInterface[] nestedInterfaces)
        {
            if (nestedInterfaces == null || nestedInterfaces.Any(x => x == null))
            {
                throw new ArgumentNullException("Nested Interfaces cannot be null.");
            }

            NestedInterfaces.AddRange(nestedInterfaces);
            return this;
        }

        public SGClass WithNestedEnums(params SGEnum[] nestedEnums)
        {
            if (nestedEnums == null || nestedEnums.Any(x => x == null))
            {
                throw new ArgumentNullException("Nested Enums cannot be null.");
            }

            NestedEnums.AddRange(nestedEnums);
            return this;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            // Class Attributes
            sb.Append($"{AccessibilityLevel} ");
            if (IsStatic) { sb.Append("static "); }
            if (IsAbstract) { sb.Append("abstract "); }
            if (IsPartial) { sb.Append("partial "); }
            sb.Append($"class {ClassName}");

            if (IsGeneric)
            {
                sb.Append("<");
                sb.Append(String.Join(", ", GenericTypeNames));
                sb.Append(">");
            }

            if (!String.IsNullOrEmpty(BaseClass) || InterfaceImplementations.Count > 0)
            {
                sb.Append(" : ");
                if (!String.IsNullOrEmpty(BaseClass))
                {
                    sb.Append($"{BaseClass}");

                    if (InterfaceImplementations.Count > 0)
                    {
                        sb.Append(", ");
                    }
                }
                sb.Append(String.Join(", ", InterfaceImplementations));
            }
            sb.AppendLine();
            sb.AppendLine("{");

            foreach (SGClassProperty property in Properties)
            {
                foreach (string propertyLine in property.ToString().BreakIntoLines())
                {
                    sb.AppendLine($"\t{propertyLine}");
                }
            }
            foreach (SGClassField field in Fields)
            {
                foreach (string fieldLine in field.ToString().BreakIntoLines())
                {
                    sb.AppendLine($"\t{fieldLine}");
                }
            }
            foreach (SGClassConstructor constructor in Constructors)
            {
                foreach (string ctorLine in constructor.ToString().BreakIntoLines())
                {
                    sb.AppendLine($"\t{ctorLine}");
                }
            }
            foreach (SGMethod method in Methods)
            {
                foreach (string methodLine in method.ToString().BreakIntoLines())
                {
                    sb.AppendLine($"\t{methodLine}");
                }
            }
            foreach (SGClass nestedClass in NestedClasses)
            {
                foreach (string nestedClassLine in nestedClass.ToString().BreakIntoLines())
                {
                    sb.AppendLine($"\t{nestedClassLine}");
                }
            }
            foreach (SGInterface nestedInterface in NestedInterfaces)
            {
                foreach (string nestedInterfaceLine in nestedInterface.ToString().BreakIntoLines())
                {
                    sb.AppendLine($"\t{nestedInterfaceLine}");
                }
            }
            foreach (SGEnum nestedEnum in NestedEnums)
            {
                foreach (string nestedEnumLine in nestedEnum.ToString().BreakIntoLines())
                {
                    sb.AppendLine($"\t{nestedEnumLine}");
                }
            }

            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
