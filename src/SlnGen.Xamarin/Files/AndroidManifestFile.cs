using SlnGen.Core;
using System.IO;
using System.Xml.Linq;

namespace SlnGen.Xamarin.Files
{
    public class AndroidManifestFile : ProjectFile
    {
        public AndroidManifestFile(string assemblyName, string packageName, int minSdkVersion, int targetSdkVersion) : base("AndroidManifest.xml", false, false)
        {
            //XNamespace xNamespace = "android";
            XNamespace xNamespace = "http://schemas.android.com/apk/res/android";
            var manifestNode = new XElement("manifest",
                //new XAttribute("xlmns" + xNamespace, "http://schemas.android.com/apk/res/android"),
                //new XAttribute(xNamespace.ToString(), "http://schemas.android.com/apk/res/android"),
                new XAttribute(xNamespace + "versionCode", "1"),
                new XAttribute(xNamespace + "versionName", "1.0"),
                new XAttribute(xNamespace + "package", packageName),
                new XElement("uses-sdk",
                    new XAttribute(xNamespace + "minSdkVersion", minSdkVersion.ToString()),
                    new XAttribute(xNamespace + "targetSdkVersion", targetSdkVersion.ToString())
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
