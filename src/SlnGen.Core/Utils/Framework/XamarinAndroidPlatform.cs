namespace SlnGen.Core.Utils
{
    public sealed class XamarinAndroidPlatform : NetPlatform
    {
        public static readonly XamarinAndroidPlatform v6_0 = new XamarinAndroidPlatform("Xamarin.Android 6.0", "v6.0");
        public static readonly XamarinAndroidPlatform v6_1 = new XamarinAndroidPlatform("Xamarin.Android 6.1", "v6.1");
        public static readonly XamarinAndroidPlatform v7_0 = new XamarinAndroidPlatform("Xamarin.Android 7.0", "v7.0");
        public static readonly XamarinAndroidPlatform v7_1 = new XamarinAndroidPlatform("Xamarin.Android 7.1", "v7.1");
        public static readonly XamarinAndroidPlatform v7_2 = new XamarinAndroidPlatform("Xamarin.Android 7.2", "v7.2");
        public static readonly XamarinAndroidPlatform v7_3 = new XamarinAndroidPlatform("Xamarin.Android 7.3", "v7.3");
        public static readonly XamarinAndroidPlatform v7_4 = new XamarinAndroidPlatform("Xamarin.Android 7.4", "v7.4");
        public static readonly XamarinAndroidPlatform v8_0 = new XamarinAndroidPlatform("Xamarin.Android 8.0", "v8.0");
        public static readonly XamarinAndroidPlatform v8_1 = new XamarinAndroidPlatform("Xamarin.Android 8.1", "v8.1");
        public static readonly XamarinAndroidPlatform v8_2 = new XamarinAndroidPlatform("Xamarin.Android 8.2", "v8.2");
        public static readonly XamarinAndroidPlatform v8_3 = new XamarinAndroidPlatform("Xamarin.Android 8.3", "v8.3");
        public static readonly XamarinAndroidPlatform v9_0 = new XamarinAndroidPlatform("Xamarin.Android 9.0", "v9.0");

        private XamarinAndroidPlatform(string name, string targetVersion) : base(name, targetVersion) { }
    }
}
