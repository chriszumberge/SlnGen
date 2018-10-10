namespace SlnGen.Core.Utils
{
    public sealed class XamariniOSPlatform : NetPlatform
    {
        public static readonly XamariniOSPlatform v8_0 = new XamariniOSPlatform("Xamarin.iOS 8.0", "8.0.30703", "2.0");

        public static readonly XamariniOSPlatform v10_0 = new XamariniOSPlatform("Xamarin.iOS 10.0", "", "");

        public static readonly XamariniOSPlatform v10_14 = new XamariniOSPlatform("Xamarin.iOS 10.14", "", "");

        public string SchemaVersion { get; }
        private XamariniOSPlatform(string name, string productVersion, string schemaVersion) : base(name, productVersion) {
            SchemaVersion = schemaVersion;
        }
    }
}
