using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen.Core.Code
{
    public sealed class SGInterface
    {
        string _interfaceName;
        public string InterfaceName
        {
            get { return _interfaceName; }
            set
            {
                if (value == null)
                {
                    throw new Exception("Cannot set InterfaceName to null.");
                }
                else if (value.Length == 0)
                {
                    throw new Exception("Cannot set Interfacename to empty string.");
                }
                else
                {
                    _interfaceName = value;
                }
            }
        }

        public SGAccessibilityLevel AccessibilityLevel { get; set; }

        public List<string> InterfaceImplementations { get; set; } = new List<string>();

        public bool IsGeneric => GenericTypeNames.Count > 0;
        public List<string> GenericTypeNames { get; set; } = new List<string>();

        public SGInterface(string interfaceName) : this(SGAccessibilityLevel.Private, interfaceName) { }

        public SGInterface(SGAccessibilityLevel accessibilityLevel, string interfaceName)
        {
            if (interfaceName == null)
            {
                throw new ArgumentNullException(nameof(interfaceName));
            }
            if (interfaceName.Length == 0)
            {
                throw new ArgumentException("Argument cannot be an empty string.", nameof(interfaceName));
            }

            InterfaceName = interfaceName.Replace(" ", "_");
            AccessibilityLevel = accessibilityLevel;
        }

        public SGInterface WithImplementations(params string[] interfaceImplementations)
        {
            if (interfaceImplementations.Any(x => String.IsNullOrEmpty(x)))
            {
                throw new Exception("Interface Implementations cannot be null or empty stings.");
            }
            InterfaceImplementations.AddRange(interfaceImplementations);
            return this;
        }

        public SGInterface WithImplementations(params SGInterface[] interfaceImplementations)
        {
            return this.WithImplementations(interfaceImplementations.Select(x => x.InterfaceName).ToArray());
        }

        public SGInterface WithGenericTypeNames(params string[] genericTypeNames)
        {
            if (genericTypeNames.Any(x => String.IsNullOrEmpty(x)))
            {
                throw new Exception("Generic Type Names cannot be null or empty strings.");
            }

            GenericTypeNames.AddRange(genericTypeNames);
            return this;
        }
    }
}
