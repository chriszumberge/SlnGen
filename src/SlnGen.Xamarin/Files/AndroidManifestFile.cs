using SlnGen.Core;
using System.IO;
using System.Xml.Linq;

namespace SlnGen.Xamarin.Files
{
    public class AndroidManifestFile : ProjectFile
    {
        public XNamespace AndroidNamespace => "http://schemas.android.com/apk/res/android";
        XElement _manifestNode;

        public XElement ApplicationNode { get; }

        public AndroidManifestFile(string assemblyName, string packageName, int minSdkVersion, int targetSdkVersion) : base("AndroidManifest.xml", false, false)
        {
            _manifestNode = new XElement("manifest",
                new XAttribute(AndroidNamespace + "versionCode", "1"),
                new XAttribute(AndroidNamespace + "versionName", "1.0"),
                new XAttribute("package", packageName),
                new XElement("uses-sdk",
                    new XAttribute(AndroidNamespace + "minSdkVersion", minSdkVersion.ToString()),
                    new XAttribute(AndroidNamespace + "targetSdkVersion", targetSdkVersion.ToString())
                )
            );
            ApplicationNode = new XElement("application",
                    new XAttribute(AndroidNamespace + "label", assemblyName)
                );
            _manifestNode.Add(ApplicationNode);
        }

        public bool AccessNetworkStateEnabled { get; private set; } = false;
        public AndroidManifestFile EnableAccessNetworkState()
        {
            if (!AccessNetworkStateEnabled)
            {
                _manifestNode.Add(new XElement("uses-permission",
                    new XAttribute(AndroidNamespace + "name", "android.permission.ACCESS_NETWORK_STATE")));
                AccessNetworkStateEnabled = true;
            }
            return this;
        }

        public bool ReadExternalStorageEnabled { get; private set; } = false;
        public AndroidManifestFile EnableReadExternalStorage()
        {
            if (!ReadExternalStorageEnabled)
            {
                _manifestNode.Add(new XElement("uses-permission",
                    new XAttribute(AndroidNamespace + "name", "android.permission.READ_EXTERNAL_STORAGE")));
                ReadExternalStorageEnabled = true;
            }
            return this;
        }

        public bool WriteExternalStorageEnabled { get; private set; } = false;
        public AndroidManifestFile EnableWriteExternalStorage()
        {
            if (!WriteExternalStorageEnabled)
            {
                _manifestNode.Add(new XElement("uses-permission",
                    new XAttribute(AndroidNamespace + "name", "android.permission.WRITE_EXTERNAL_STORAGE")));
                WriteExternalStorageEnabled = true;
            }
            return this;
        }

        public AndroidManifestFile Build()
        {
            using (var memoryStream = new MemoryStream())
            {
                _manifestNode.Save(memoryStream);

                memoryStream.Position = 0;
                using (var streamReader = new StreamReader(memoryStream))
                {
                    FileContents = streamReader.ReadToEnd();
                }
            }

            return this;
        }
    }
}
