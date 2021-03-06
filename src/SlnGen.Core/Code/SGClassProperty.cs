﻿using SlnGen.Core.Utils;
using System;
using System.Text;

namespace SlnGen.Core.Code
{
    public class SGClassProperty
    {
        string _propertyName;
        public string PropertyName
        {
            get { return _propertyName; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(PropertyName), $"Cannot set {nameof(PropertyName)} to null.");
                }
                else if (value.Length == 0)
                {
                    throw new ArgumentException($"Cannot set {nameof(PropertyName)} to empty string.", nameof(PropertyName));
                }
                else
                {
                    _propertyName = value.Replace(" ", "_");
                }
            }
        }

        string _propertyType;
        public string PropertyType
        {
            get { return _propertyType; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(PropertyType), $"Cannot set {nameof(PropertyType)} to null.");
                }
                else if (value.Length == 0)
                {
                    throw new ArgumentException($"Cannot set {nameof(PropertyType)} to empty string.", nameof(PropertyType));
                }
                else
                {
                    _propertyType = value;
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
                    throw new ArgumentNullException(nameof(AccessibilityLevel), $"Cannot set {nameof(AccessibilityLevel)} to null.");
                }
                else
                {
                    _accessibilityLevel = value;
                }
            }
        }

        public bool IsStatic { get; set; }

        SGAccessibilityLevel _getterAccessibilityLevel;
        public SGAccessibilityLevel GetterAccessibilityLevel
        {
            get { return _getterAccessibilityLevel; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(GetterAccessibilityLevel), $"Cannot set {nameof(GetterAccessibilityLevel)} to null.");
                }
                else
                {
                    _getterAccessibilityLevel = value;
                }
            }
        }

        SGAccessibilityLevel _setterAccessibilityLevel;
        public SGAccessibilityLevel SetterAccessibilityLevel
        {
            get { return _setterAccessibilityLevel; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(SetterAccessibilityLevel), $"Cannot set {nameof(SetterAccessibilityLevel)} to null.");
                }
                else
                {
                    _setterAccessibilityLevel = value;
                }
            }
        }

        public string InitializationValue { get; set; }

        public SGClassProperty(string propertyName, Type propertyType, SGAccessibilityLevel accessibilityLevel = null, bool isStatic = false,
            SGAccessibilityLevel getterAccessibilityLevel = null, SGAccessibilityLevel setterAccessibilityLevel = null) :
            this(propertyName, propertyType?.Name ?? throw new ArgumentNullException(nameof(propertyType)), accessibilityLevel, isStatic, getterAccessibilityLevel, setterAccessibilityLevel)
        { }

        public SGClassProperty(string propertyName, string propertyTypeName, SGAccessibilityLevel accessibilityLevel = null, bool isStatic = false,
            SGAccessibilityLevel getterAccessibilityLevel = null, SGAccessibilityLevel setterAccessibilityLevel = null)
        {
            PropertyName = propertyName;
            PropertyType = propertyTypeName;
            AccessibilityLevel = accessibilityLevel ?? SGAccessibilityLevel.Private;
            IsStatic = isStatic;
            GetterAccessibilityLevel = getterAccessibilityLevel ?? SGAccessibilityLevel.None;
            SetterAccessibilityLevel = setterAccessibilityLevel ?? SGAccessibilityLevel.None;
        }

        public SGClassProperty WithPropertyName(string newPropertyName)
        {
            PropertyName = newPropertyName;
            return this;
        }

        public SGClassProperty WithPropertyType(string newPropertyTypeName)
        {
            PropertyType = newPropertyTypeName;
            return this;
        }

        public SGClassProperty WithPropertyType(Type newPropertyType)
        {
            PropertyType = newPropertyType?.Name;
            return this;
        }

        public SGClassProperty WithAccessibilityLevel(SGAccessibilityLevel newAccessibilityLevel)
        {
            AccessibilityLevel = newAccessibilityLevel;
            return this;
        }

        public SGClassProperty WithIsStatic(bool isStatic)
        {
            IsStatic = isStatic;
            return this;
        }

        public SGClassProperty WithGetterAccessibilityLevel(SGAccessibilityLevel newGetterAccessibilityLevel)
        {
            GetterAccessibilityLevel = newGetterAccessibilityLevel;
            return this;
        }

        public SGClassProperty WithSetterAccessibilityLevel(SGAccessibilityLevel newSetterAccessibilityLevel)
        {
            SetterAccessibilityLevel = newSetterAccessibilityLevel;
            return this;
        }

        public SGClassProperty WithInitializationValue(object initializationValue)
        {
            if (initializationValue?.GetType() == typeof(string))
            {
                // Wrap strings in quotes for being evaluated
                InitializationValue = $"\"{initializationValue}\"";
            }
            else
            {
                InitializationValue = initializationValue?.ToString() ?? "null";
            }
            return this;
        }

        // TODO eventually
        string GetterText = String.Empty;
        string SetterText = String.Empty;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"{AccessibilityLevel} ");
            if (IsStatic) { sb.Append("static "); }
            sb.Append($"{PropertyType} ");
            sb.AppendLine(PropertyName);
            sb.AppendLine("{");

            sb.Append("\t");
            if (!GetterAccessibilityLevel.Equals(AccessibilityLevel))
            {
                sb.Append($"{GetterAccessibilityLevel} ");
            }
            sb.Append("get");

            if (GetterText.Length == 0) { sb.AppendLine(";"); }
            else
            {
                sb.AppendLine();
                sb.AppendLine("\t{");
                foreach (string getterTextLine in GetterText.BreakIntoLines())
                {
                    sb.AppendLine($"\t\t{getterTextLine}");
                }
                sb.AppendLine("\t}");
            }

            sb.Append("\t");
            if (!SetterAccessibilityLevel.Equals(AccessibilityLevel))
            {
                sb.Append($"{SetterAccessibilityLevel} ");
            }
            sb.Append("set");

            if (SetterText.Length == 0) { sb.AppendLine(";"); }
            else
            {
                sb.AppendLine();
                sb.AppendLine("\t{");
                foreach (string setterTextLine in SetterText.BreakIntoLines())
                {
                    sb.AppendLine($"\t\t{setterTextLine}");
                }
                sb.AppendLine("\t}");
            }

            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
