using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlnGen.Core.Code
{
    // TODO temp implementation
    public class SGAttribute
    {
        string _attributeName;
        public string AttributeName
        {
            get { return _attributeName; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(AttributeName), $"Cannot set {nameof(AttributeName)} to null.");
                }
                else if (value.Length == 0)
                {
                    throw new ArgumentException($"Cannot set {nameof(AttributeName)} to empty string.", nameof(AttributeName));
                }
                else
                {
                    _attributeName = value.Replace(" ", "_");
                }
            }
        }

        public string AttributeNamespace { get; set; }

        public List<string> AttributeArguments { get; set; } = new List<string>();

        public SGAttribute(string attributeName, params string[] attributeArguments)
        {
            AttributeName = attributeName;
            AttributeArguments = attributeArguments.ToList();
        }

        public SGAttribute WithNamespace(string namespaceName)
        {
            AttributeNamespace = namespaceName;
            return this;
        }

        public SGAttribute WithArguments(params string[] arguments)
        {
            foreach (string arg in arguments)
            {
                if (arg == null)
                {
                    AttributeArguments.Add("null");
                }
                else
                {
                    AttributeArguments.Add(arg);
                }
            }

            return this;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            if (!String.IsNullOrEmpty(AttributeNamespace))
            {
                sb.Append($"{AttributeNamespace}: ");
            }
            sb.Append($"{AttributeName}");
            if (AttributeArguments.Count > 0)
            {
                sb.Append("(");
                sb.Append(String.Join(", ", AttributeArguments));
                sb.Append(")");
            }
            sb.Append("]");
            return sb.ToString();
        }
    }
}
