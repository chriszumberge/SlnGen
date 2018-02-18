using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen.Core.Code
{
    public class SGArgument
    {
        string _argumentName;
        public string ArgumentName
        {
            get { return _argumentName; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(ArgumentName), "Cannot set ArgumentName to null.");
                }
                else if (value.Length == 0)
                {
                    throw new ArgumentException("Cannot set ArgumentName to empty string.", nameof(ArgumentName));
                }
                else
                {
                    _argumentName = value.Replace(" ", "_");
                }
            }
        }

        string _argumentTypeName;
        public string ArgumentTypeName
        {
            get { return _argumentTypeName; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(ArgumentTypeName), "Cannot set ArgumentTypeName to null.");
                }
                else if (value.Length == 0)
                {
                    throw new ArgumentException("Cannot set ArgumentTypeName to empty string.", nameof(ArgumentTypeName));
                }
                else
                {
                    _argumentTypeName = value;
                }
            }
        }

        bool _hasDefaultValue => ArgumentDefaultValue != null;
        public string ArgumentDefaultValue { get; set; }

        public SGArgument(string argumentName, string argumentTypeName)
        {
            ArgumentName = argumentName;
            ArgumentTypeName = argumentTypeName;
        }

        public SGArgument(string argumentname, Type argumentType) : this(argumentname, argumentType?.Name) { }

        public SGArgument(string argumentName, string argumentTypeName, object defaultValue) : this(argumentName, argumentTypeName)
        {
            ArgumentDefaultValue = defaultValue?.ToString();
        }

        public SGArgument(string argumentName, Type argumentType, object defaultValue) : this(argumentName, argumentType?.Name, defaultValue) { }

        public SGArgument WithArgumentTypeName(string newArgTypeName)
        {
            ArgumentTypeName = newArgTypeName;
            return this;
        }

        public SGArgument WithArgumentTypeName(Type newArgType)
        {
            ArgumentTypeName = newArgType?.Name;
            return this;
        }

        public SGArgument WithDefaultValue(object defaultValue)
        {
            ArgumentDefaultValue = defaultValue.ToString();
            return this;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{ArgumentTypeName} {ArgumentName}");

            if (_hasDefaultValue)
            {
                sb.Append($" = {ArgumentDefaultValue}");
            }

            return sb.ToString();
        }
    }
}
