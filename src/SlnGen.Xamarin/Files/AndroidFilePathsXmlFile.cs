using System.IO;
using System.Xml.Linq;

namespace SlnGen.Xamarin.Files
{
    public sealed class AndroidFilePathsXmlFile : AndroidResourceProjectFile
    {
        XNamespace _xNamespace = "http://schemas.android.com/apk/res/android";
        XElement _pathsNode = new XElement("paths");

        public AndroidFilePathsXmlFile() : base("file_paths.xml")
        {
            Build();
        }

        public AndroidFilePathsXmlFile WithElement(XElement element)
        {
            _pathsNode.Add(element);
            Build();
            return this;
        }

        void Build()
        {
            using (var memoryStream = new MemoryStream())
            {
                _pathsNode.Save(memoryStream);

                memoryStream.Position = 0;
                using (var streamReader = new StreamReader(memoryStream))
                {
                    FileContents = streamReader.ReadToEnd();
                }
            }
        }
    }
}
