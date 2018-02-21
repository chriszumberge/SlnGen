using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    throw new ArgumentNullException(nameof(FieldName), "Cannot set FieldName to null.");
                }
                else if (value.Length == 0)
                {
                    throw new ArgumentException("Cannot set FieldName to empty string.", nameof(FieldName));
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
                    throw new ArgumentNullException(nameof(FieldType), "Cannot set FieldType to null.");
                }
                else if (value.Length == 0)
                {
                    throw new ArgumentException("Cannot set FieldType to empty string.", nameof(FieldType));
                }
                else
                {
                    _fieldType = value;
                }
            }
        }

        public SGAccessibilityLevel AccessibilityLevel { get; set; }

        public bool IsStatic { get; set; }

        public bool IsConst { get; set; }

        public bool IsReadonly { get; set; }

        public SGClassField(string fieldName, Type fieldType, SGAccessibilityLevel accessibilityLevel = null, bool @static = false,
            bool @const = false, bool @readonly = false)
        {
            FieldName = fieldName;

            if (fieldType == null)
            {
                throw new ArgumentNullException(nameof(fieldType));
            }
            FieldType = fieldType.Name;

            AccessibilityLevel = accessibilityLevel ?? SGAccessibilityLevel.Private;
            IsStatic = @static;
            IsConst = @const;
            IsReadonly = @readonly;
        }

        public SGClassField(string fieldName, string fieldType, SGAccessibilityLevel accessibilityLevel = null, bool @static = false,
            bool @const = false, bool @readonly = false)
        {
            FieldName = fieldName;
            FieldType = fieldType;
            AccessibilityLevel = accessibilityLevel ?? SGAccessibilityLevel.Private;
            IsStatic = @static;
            IsConst = @const;
            IsReadonly = @readonly;
        }
    }
}
