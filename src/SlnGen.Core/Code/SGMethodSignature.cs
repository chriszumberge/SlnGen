using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen.Core.Code
{
    public class SGMethodSignature
    {
        string _methodName;
        public string MethodName
        {
            get { return _methodName; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(MethodName), "Cannot set MethodName to null.");
                }
                else if (value.Length == 0)
                {
                    throw new ArgumentException("Cannot set MethodName to empty string.", nameof(MethodName));
                }
                else
                {
                    _methodName = value.Replace(" ", "_");
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

        string _returnType;
        public string ReturnType
        {
            get { return _returnType; }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    _returnType = "void";
                }
                else
                {
                    _returnType = value;
                }
            }
        }
        public bool IsStatic { get; set; }
        public bool IsAsync { get; set; }
        public bool IsOverride { get; set; }

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

        List<SGArgument> _arguments = new List<SGArgument>();
        public List<SGArgument> Arguments
        {
            get { return _arguments; }
            set
            {
                if (value == null)
                {
                    _arguments = new List<SGArgument>();
                }
                else
                {
                    _arguments = value;
                }
            }
        }

        string _constraint;
        public string Constraint
        {
            get { return _constraint; }
            set { _constraint = value; }
        }

        public SGMethodSignature(string methodName, SGAccessibilityLevel accessibilityLevel = null, bool isStatic = false, bool isAsync = false, bool isOverride = false, string returnType = null)
        {
            MethodName = methodName;
            AccessibilityLevel = accessibilityLevel ?? SGAccessibilityLevel.Private;
            IsStatic = isStatic;
            IsAsync = isAsync;
            IsOverride = isOverride;
            ReturnType = returnType;
        }

        public SGMethodSignature(string methodName, Type returnType, SGAccessibilityLevel accessibilityLevel = null, bool isStatic = false, bool isAsync = false, bool isOverride = false)
        {
            MethodName = methodName;
            AccessibilityLevel = accessibilityLevel ?? SGAccessibilityLevel.Private;
            IsStatic = isStatic;
            IsAsync = isAsync;
            IsOverride = isOverride;
            ReturnType = returnType?.Name;
        }

        public SGMethodSignature WithAccessibilityLevel(SGAccessibilityLevel newAccessibilityLevel)
        {
            AccessibilityLevel = newAccessibilityLevel;
            return this;
        }

        public SGMethodSignature WithReturnType(string newReturnType)
        {
            ReturnType = newReturnType;
            return this;
        }

        public SGMethodSignature WithReturnType(Type newReturnType)
        {
            ReturnType = newReturnType.Name;
            return this;
        }

        public SGMethodSignature WithIsStatic(bool newStaticValue)
        {
            IsStatic = newStaticValue;
            return this;
        }

        public SGMethodSignature WithIsAsync(bool newAsyncValue)
        {
            IsAsync = newAsyncValue;
            return this;

        }

        public SGMethodSignature WithIsOverride(bool newOverrideValue)
        {
            IsOverride = newOverrideValue;
            return this;
        }

        public SGMethodSignature WithGenericTypeNames(params string[] genericTypeNames)
        {
            if (genericTypeNames.Any(x => String.IsNullOrEmpty(x)))
            {
                throw new ArgumentException("Generic Type Names cannot be null or empty strings.");
            }

            GenericTypeNames.AddRange(genericTypeNames);
            return this;
        }

        public SGMethodSignature WithArguments(params SGArgument[] args)
        {
            if (args.Any(x => x == null))
            {
                throw new ArgumentException("Method Arguments cannot be null.");
            }

            Arguments.AddRange(args);
            return this;
        }

        public SGMethodSignature WithConstraint(string constraint)
        {
            Constraint = constraint;
            return this;
        }

        public override string ToString()
        {
            // TODO ADD TESTS FOR MULTIPLE TO STRING SCENARIOS
            StringBuilder sb = new StringBuilder();
            if (!AccessibilityLevel.Equals(SGAccessibilityLevel.None))
            {
                sb.Append($"{AccessibilityLevel} ");
            }
            if (IsStatic) { sb.Append("static "); }
            if (IsAsync) { sb.Append("async "); }
            if (IsOverride) { sb.Append("override "); }
            sb.Append($"{ReturnType} {MethodName}");

            if (IsGeneric && GenericTypeNames.Count > 0)
            {
                sb.Append("<");
                sb.Append(String.Join(", ", GenericTypeNames));
                sb.Append(">");
            }

            sb.Append("(");
            sb.Append(String.Join(", ", Arguments.Select(x => x.ToString())));
            sb.Append(")");

            if (!String.IsNullOrEmpty(Constraint))
            {
                sb.Append($" where {Constraint}");
            }

            return sb.ToString();
        }
    }
}
