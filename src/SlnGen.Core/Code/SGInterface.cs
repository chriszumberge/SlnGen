///
/// SGInterface.cs
/// 
/// Author:
///     Chris Zumberge <chriszumberge@gmail.com>
///     
/// 02/06/2018
/// 
using System;
using System.Collections.Generic;
using System.Linq;

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
                    throw new ArgumentNullException(nameof(InterfaceName), "Cannot set InterfaceName to null.");
                }
                else if (value.Length == 0)
                {
                    throw new ArgumentException("Cannot set InterfaceName to empty string.", nameof(InterfaceName));
                }
                else
                {
                    _interfaceName = value.Replace(" ", "_");
                }
            }
        }

        public SGAccessibilityLevel AccessibilityLevel { get; set; }

        public List<string> InterfaceImplementations { get; set; } = new List<string>();

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

        public List<SGMethodSignature> MethodSignatures { get; set; } = new List<SGMethodSignature>();

        public List<SGAttribute> Attributes { get; set; } = new List<SGAttribute>();

        public SGInterface(string interfaceName) : this(SGAccessibilityLevel.Private, interfaceName) { }

        public SGInterface(SGAccessibilityLevel accessibilityLevel, string interfaceName)
        {
            InterfaceName = interfaceName;
            AccessibilityLevel = accessibilityLevel;
        }

        public SGInterface WithImplementations(params string[] interfaceImplementations)
        {
            if (interfaceImplementations.Any(x => String.IsNullOrEmpty(x)))
            {
                throw new ArgumentException("Interface Implementations cannot be null or empty stings.");
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
                throw new ArgumentException("Generic Type Names cannot be null or empty strings.");
            }

            GenericTypeNames.AddRange(genericTypeNames);
            return this;
        }

        public SGInterface WithAccessibilityLevel(SGAccessibilityLevel accessibilityLevel)
        {
            AccessibilityLevel = accessibilityLevel;
            return this;
        }

        public SGInterface WithMethodSignatures(params SGMethodSignature[] methods)
        {
            if (methods == null || methods.Any(x => x == null))
            {
                throw new ArgumentException("Method signatures cannot be null.");
            }

            MethodSignatures.AddRange(methods);
            return this;
        }

        public SGInterface WithAttributes(params SGAttribute[] attributes)
        {
            if (attributes == null || attributes.Any(x => x == null))
            {
                throw new ArgumentException("Attributes cannot be null.");
            }

            Attributes.AddRange(attributes);
            return this;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
