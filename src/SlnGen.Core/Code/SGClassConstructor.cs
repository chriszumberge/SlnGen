using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlnGen.Core.Code
{
    public class SGClassConstructor
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

        List<SGArgument> _baseArguments = new List<SGArgument>();
        public List<SGArgument> BaseArguments
        {
            get { return _baseArguments; }
            set
            {
                if (value == null)
                {
                    _baseArguments = new List<SGArgument>();
                }
                else
                {
                    _baseArguments = value;
                }
            }
        }

        List<SGArgument> _thisArguments = new List<SGArgument>();
        public List<SGArgument> ThisArguments
        {
            get { return _thisArguments; }
            set
            {
                if (value == null)
                {
                    _thisArguments = new List<SGArgument>();
                }
                else
                {
                    _thisArguments = value;
                }
            }
        }

        public List<string> Lines = new List<string>();

        public SGClassConstructor(string className, SGAccessibilityLevel accessibilityLevel = null)
        {
            ClassName = className;
            AccessibilityLevel = accessibilityLevel ?? SGAccessibilityLevel.Private;
        }

        public SGClassConstructor WithAccessibilityLevel(SGAccessibilityLevel newAccessibilityLevel)
        {
            AccessibilityLevel = newAccessibilityLevel;
            return this;
        }

        public SGClassConstructor WithBaseConstructorArguments(params SGArgument[] baseCtorArgs)
        {
            if (baseCtorArgs.Any(x => x == null))
            {
                throw new ArgumentException("Base Constructor Arguments cannot be null.");
            }

            if (ThisArguments.Count > 0)
            {
                throw new Exception("Cannot specify both Base constructor arguments and This constructor arguments.");
            }

            BaseArguments.AddRange(baseCtorArgs);
            return this;
        }

        public SGClassConstructor WithThisContstructorArguments(params SGArgument[] thisCtorArgs)
        {
            if (thisCtorArgs.Any(x => x == null))
            {
                throw new ArgumentException("This Constructor Arguments cannot be null.");
            }

            if (BaseArguments.Count > 0)
            {
                throw new Exception("Cannot specify both This constructor arguments and Base constructor arguments.");
            }

            ThisArguments.AddRange(thisCtorArgs);
            return this;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{AccessibilityLevel} ");
            sb.Append($"{ClassName} (");
            sb.Append(String.Join(", ", Arguments.Select(x => x.ToString())));
            sb.Append(")");
            if (BaseArguments.Count > 0)
            {
                sb.Append(" : base(");
                sb.Append(String.Join(", ", BaseArguments.Select(x => x.ArgumentName)));
                sb.Append(")");
            }
            else if (ThisArguments.Count > 0)
            {
                sb.Append(" : this(");
                sb.Append(String.Join(", ", ThisArguments.Select(x => x.ArgumentName)));
                sb.Append(")");
            }
            sb.AppendLine();

            sb.AppendLine("{");
            Lines.ForEach((l) => sb.AppendLine($"\t{l}"));
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}