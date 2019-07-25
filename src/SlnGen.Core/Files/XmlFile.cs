using SlnGen.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace ZESoft.SlnGen.Core.Files
{
    public class XmlFile : ProjectFile
    {
        public XNamespace Namespace => _xNamespace;
        readonly XNamespace _xNamespace;
        readonly XElement _rootNode;

        public XmlFile(string fileName, string rootNode, string @namespace, params string[] namespaces) 
            : base($"{fileName}{(fileName.EndsWith(".xml") ? string.Empty : ".xml")}", false, true)
        {
            _xNamespace = @namespace;
            _rootNode = new XElement(rootNode);
            Build();
        }

        public XmlFile WithElement(XElement element)
        {
            _rootNode.Add(element);
            Build();
            return this;
        }

        public XmlFile WithElement(string elementName)
        {
            _rootNode.Add(new XElement(_xNamespace + elementName));
            Build();
            return this;
        }

        public XmlFile WithAttribute(XAttribute attribute)
        {
            return WithAttribute(attribute.Name.LocalName, attribute.Value);
        }

        public XmlFile WithAttribute(string attributeName, string attributeValue)
        {
            _rootNode.SetAttributeValue(attributeName, attributeValue);
            Build();
            return this;
        }


        void Build()
        {
            using (var memoryStream = new MemoryStream())
            {
                _rootNode.Save(memoryStream);

                memoryStream.Position = 0;
                using (var streamReader = new StreamReader(memoryStream))
                {
                    FileContents = streamReader.ReadToEnd();
                }
            }
        }
    }
}
