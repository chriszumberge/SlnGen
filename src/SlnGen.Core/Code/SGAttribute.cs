namespace SlnGen.Core.Code
{
    // TODO temp implementation
    public class SGAttribute
    {
        public string AttributeText { get; }

        public SGAttribute(string attributeText)
        {
            AttributeText = attributeText;
        }

        public override string ToString()
        {
            return AttributeText;
        }
    }
}
