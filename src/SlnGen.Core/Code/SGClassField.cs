using System;
using System.Text;

namespace SlnGen.Core.Code
{
    public class SGClassField
    {
        string _fieldName;
        public string FieldName
        {
            get { return _fieldName; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(FieldName), $"Cannot set {nameof(FieldName)} to null.");
                }
                else if (value.Length == 0)
                {
                    throw new ArgumentException($"Cannot set {nameof(FieldName)} to empty string.", nameof(FieldName));
                }
                else
                {
                    _fieldName = value.Replace(" ", "_");
                }
            }
        }

        string _fieldType;
        public string FieldType
        {
            get { return _fieldType; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(FieldType), $"Cannot set {nameof(FieldType)} to null.");
                }
                else if (value.Length == 0)
                {
                    throw new ArgumentException($"Cannot set {nameof(FieldType)} to empty string.", nameof(FieldType));
                }
                else
                {
                    _fieldType = value;
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

        public bool IsConst { get; set; }

        public bool IsReadonly { get; set; }

        public string InitializationValue { get; set; }

        public SGClassField(string fieldName, Type fieldType, SGAccessibilityLevel accessibilityLevel = null, bool isStatic = false,
            bool isConst = false, bool isReadonly = false) :
            this(fieldName, fieldType?.Name ?? throw new ArgumentNullException(nameof(fieldType)), accessibilityLevel, isStatic, isConst, isReadonly)
        { }

        public SGClassField(string fieldName, string fieldTypeName, SGAccessibilityLevel accessibilityLevel = null, bool isStatic = false,
            bool isConst = false, bool isReadonly = false)
        {
            FieldName = fieldName;
            FieldType = fieldTypeName;
            AccessibilityLevel = accessibilityLevel ?? SGAccessibilityLevel.Private;
            IsStatic = isStatic;
            IsConst = isConst;
            IsReadonly = isReadonly;
        }

        public SGClassField WithFieldName(string newFieldName)
        {
            FieldName = newFieldName;
            return this;
        }

        public SGClassField WithTypeValue(string newFieldTypeName)
        {
            FieldType = newFieldTypeName;
            return this;
        }

        public SGClassField WithTypeValue(Type newFieldType)
        {
            FieldType = newFieldType?.Name;
            return this;
        }

        public SGClassField WithAccessibilityLevel(SGAccessibilityLevel newAccessibilityLevel)
        {
            AccessibilityLevel = newAccessibilityLevel;
            return this;
        }

        public SGClassField WithIsStatic(bool isStatic)
        {
            IsStatic = isStatic;
            return this;
        }

        public SGClassField WithIsConst(bool isConst)
        {
            IsConst = isConst;
            return this;
        }

        public SGClassField WithIsReadonly(bool isReadonly)
        {
            IsReadonly = isReadonly;
            return this;
        }

        public SGClassField WithInitializationValue(object initializationValue)
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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{AccessibilityLevel} ");
            if (IsStatic) { sb.Append("static "); }
            if (IsConst) { sb.Append("const "); }
            if (IsReadonly) { sb.Append("readonly "); }
            sb.Append($"{FieldType} ");
            sb.Append($"{FieldName}");

            if (InitializationValue != null)
            {
                sb.Append($" = {InitializationValue}");
            }

            sb.AppendLine(";");

            return sb.ToString();
        }
    }
}
