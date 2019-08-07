using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlnGen.Core.Code
{
    public class SGMethod
    {
        SGMethodSignature _methodSignature;

        public List<string> Lines { get; set; } = new List<string>();

        public List<string> Comments { get; set; } = new List<string>();

        public List<SGAttribute> Attributes { get; set; } = new List<SGAttribute>();

        public SGMethod(SGMethodSignature methodSignature)
        {
            _methodSignature = methodSignature;
        }

        public SGMethod WithAccessibilityLevel(SGAccessibilityLevel newAccessibilityLevel)
        {
            _methodSignature = _methodSignature.WithAccessibilityLevel(newAccessibilityLevel);
            return this;
        }

        public SGMethod WithAttributes(params SGAttribute[] attributes)
        {
            if (attributes == null || attributes.Any(x => x == null))
            {
                throw new ArgumentNullException("Attributes cannot be null.");
            }

            Attributes.AddRange(attributes);
            return this;
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            Comments.ForEach((c) => sb.AppendLine($"// {c}"));
            foreach (var attr in Attributes)
            {
                sb.AppendLine(attr.ToString());
            }
            sb.AppendLine(_methodSignature.ToString());
            sb.AppendLine("{");
            Lines.ForEach((l) => sb.AppendLine($"\t{l}"));
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
