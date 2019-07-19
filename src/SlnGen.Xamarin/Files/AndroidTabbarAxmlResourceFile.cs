using System.IO;
using System.Xml.Linq;

namespace SlnGen.Xamarin.Files
{
    public class AndroidTabbarAxmlResourceFile : AndroidResourceProjectFile
    {
        public AndroidTabbarAxmlResourceFile() : base("Tabbar.axml")
        {
            XNamespace xAndroidNamespace = "http://schemas.android.com/apk/res/android";
            XNamespace xAppNamespace = "http://schemas.android.com/apk/res-auto";

            var tabbarNode = new XElement("android.support.design.widget.TabLayout",
                new XAttribute(xAndroidNamespace + "id", "@+id/sliding_tabs"),
                new XAttribute(xAndroidNamespace + "layout_width", "match_parent"),
                new XAttribute(xAndroidNamespace + "layout_height", "wrap_content"),
                new XAttribute(xAndroidNamespace + "background", "?attr/colorPrimary"),
                new XAttribute(xAndroidNamespace + "theme", "@style/ThemeOverlay.AppCompat.Dark.ActionBar"),
                new XAttribute(xAppNamespace + "tabIndicatorColor", "@android:color/white"),
                new XAttribute(xAppNamespace + "tabGravity", "fill"),
                new XAttribute(xAppNamespace + "tabMode", "fixed")
            );

            using (var memoryStream = new MemoryStream())
            {
                tabbarNode.Save(memoryStream);

                memoryStream.Position = 0;
                using (var streamReader = new StreamReader(memoryStream))
                {
                    FileContents = streamReader.ReadToEnd();
                }
            }
        }
    }
}
