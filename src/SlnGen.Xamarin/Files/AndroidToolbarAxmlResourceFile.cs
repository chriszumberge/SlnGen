using System.IO;
using System.Xml.Linq;

namespace SlnGen.Xamarin.Files
{
    public class AndroidToolbarAxmlResourceFile : AndroidResourceProjectFile
    {
        public AndroidToolbarAxmlResourceFile() : base("Toolbar.axml")
        {
            XNamespace xNamespace = "http://schemas.android.com/apk/res/android";
            var toolbarNode = new XElement("android.support.v7.widget.Toolbar",
                new XAttribute(xNamespace + "id", "@+id/toolbar"),
                new XAttribute(xNamespace + "layout_width", "match_parent"),
                new XAttribute(xNamespace + "layout_height", "wrap_content"),
                new XAttribute(xNamespace + "background", "?attr/colorPrimary"),
                new XAttribute(xNamespace + "theme", "@style/ThemeOverlay.AppCompat.Dark.ActionBar"),
                new XAttribute(xNamespace + "popupTheme", "@style/ThemeOverlay.AppCompat.Light")
            );

            using (var memoryStream = new MemoryStream())
            {
                toolbarNode.Save(memoryStream);

                memoryStream.Position = 0;
                using (var streamReader = new StreamReader(memoryStream))
                {
                    FileContents = streamReader.ReadToEnd();
                }
            }
        }
    }
}
