using SlnGen.Core.Utils;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml.Linq;

namespace SlnGen.Xamarin.Files
{
    public class AndroidColorsFile : AndroidResourceProjectFile
    {
        public AndroidColorsFile() : base("colors.xml")
        {
            _colors.AddRange(
                new List<ColorDefinition>
                {
                    new ColorDefinition{Name = "launcher_background", Color = "#FFFFFF"},
                    new ColorDefinition{Name = "colorPrimary", Color = "#3F51B5" },
                    new ColorDefinition{Name = "colorPrimaryDark", Color = "#303F9F" },
                    new ColorDefinition{Name = "colorAccent", Color = "#FF4081" }
                }
            );
        }

        public AndroidColorsFile WithColor(string name, Color color)
        {
            // converting to argb guarantees hex string even from known color names
            string colorHexString = color.ToHexString();
            _colors.Add(new ColorDefinition
            {
                Name = name,
                Color = colorHexString
            });
            return this;
        }

        List<ColorDefinition> _colors = new List<ColorDefinition>();

        public AndroidColorsFile Build()
        {
            var resourcesNode = new XElement("resources");
            foreach (var color in _colors)
            {
                resourcesNode.Add(
                    new XElement("color",
                        new XAttribute("name", color.Name),
                        new XText(color.Color)
                    )
                );
            }

            using (var memoryStream = new MemoryStream())
            {
                resourcesNode.Save(memoryStream);

                memoryStream.Position = 0;
                using (var streamReader = new StreamReader(memoryStream))
                {
                    FileContents = streamReader.ReadToEnd();
                }
            }

            return this;
        }

        class ColorDefinition
        {
            public string Name { get; set; }
            public string Color { get; set; }
        }
    }
}
