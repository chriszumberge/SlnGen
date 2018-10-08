using System.Collections.Generic;
using System.Text;

namespace SlnGen.Core.Code
{
    public class SGMethod
    {
        SGMethodSignature _methodSignature;

        public List<string> Lines { get; set; } = new List<string>();

        public SGMethod(SGMethodSignature methodSignature)
        {
            _methodSignature = methodSignature;
        }

        public SGMethod WithAccessibilityLevel(SGAccessibilityLevel newAccessibilityLevel)
        {
            _methodSignature = _methodSignature.WithAccessibilityLevel(newAccessibilityLevel);
            return this;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(_methodSignature.ToString());
            sb.AppendLine("{");
            Lines.ForEach((l) => sb.AppendLine($"\t{l}"));
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
