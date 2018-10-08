using SlnGen.Core;
using System.IO;
using System.Xml.Linq;

namespace SlnGen.Xamarin.Files
{
    public class AndroidManifestFile : ProjectFile
    {
        public AndroidManifestFile(string assemblyName) : base("AndroidManifest.xml", false, false)
        {
            XNamespace xNamespace = "android";
            var manifestNode = new XElement("manifest",
                    new XAttribute("xmlns" + xNamespace, "http://schemas.android.com/apk/res/android"),
                    new XElement("uses-sdk",
                        new XAttribute(xNamespace + "minSdkVersion", "15")
                    ),
                    new XElement("application",
                        new XAttribute(xNamespace + "label", assemblyName)
                    )
                );

            using (var memoryStream = new MemoryStream())
            {
                manifestNode.Save(memoryStream);

                memoryStream.Position = 0;
                using (var streamReader = new StreamReader(memoryStream))
                {
                    FileContents = streamReader.ReadToEnd();
                }
            }
        }
    }
}
