using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace SlnGen.Xamarin.Files
{
    public class AndroidStylesFile : AndroidResourceProjectFile
    {
        public AndroidStylesFile() : base("styles.xml")
        {
            _styles.Add(new StyleDefinition
            {
                Name = "MainTheme",
                Parent = "MainTheme.Base"
            });
            _styles.Add(new StyleDefinition
            {
                Name = "MainTheme.Base",
                Parent = "Theme.AppCompat.Light.DarkActionBar",
                Comment = "Base theme applied no matter what API",
                Items =
                {
                    new ItemDefinition
                    {
                        Comment = "If you are using revision 22.1 please use just windowNoTitle. Without android:",
                        Name = "windowNoTitle", Value = "true"
                    },
                    new ItemDefinition
                    {
                        Comment = "We will be using the toolbar so no need to show ActionBar",
                        Name = "windowActionBar", Value = "false"
                    },
                    new ItemDefinition
                    {
                        Comment = "Set theme colors from http://www.google.com/design/spec/style/color.html#color-color-palette"
                    },
                    new ItemDefinition
                    {
                        Comment = "colorPrimary is used for the default action bar background",
                        Name = "colorPrimary", Value = "#2196F3"
                    },
                    new ItemDefinition
                    {
                        Comment = "colorPrimaryDark is used for the status bar",
                        Name = "colorPrimaryDark", Value = "#1976D2"
                    },
                    new ItemDefinition
                    {
                        Comment = "colorAccent is used as the default value for colorControlActivated which is used to tint widgets",
                        Name = "colorAccent", Value = "#FF4081"
                    },
                    new ItemDefinition
                    {
                        Comment = "You can also set colorControlNormal, colorControlActivated colorControlHighlight and colorSwitchThumbNormal.",
                        Name = "windowActionModeOverlay", Value = "true"
                    },
                    new ItemDefinition
                    {
                        Name = "android:datePickerDialogTheme", Value = "@style/AppCompatDialogStyle"
                    }
                }
            });
            _styles.Add(new StyleDefinition
            {
                Name = "AppCompatDialogStyle",
                Parent = "Theme.AppCompat.Light.Dialog",
                Items =
                {
                    new ItemDefinition
                    {
                        Name = "colorAccent", Value = "#FF4081"
                    }
                }
            });
        }

        public AndroidStylesFile WithStyle(StyleDefinition styleDefinition)
        {
            _styles.Add(styleDefinition);
            return this;
        }

        public AndroidStylesFile WithStyleItem(string styleName, ItemDefinition itemDefinition)
        {
            var style = _styles.FirstOrDefault(x => x.Name.EndsWith(styleName));

            if (style == null)
                throw new Exception($"No style with the given name '{styleName}'.");
            else
                style.Items.Add(itemDefinition);

            return this;
        }

        List<StyleDefinition> _styles = new List<StyleDefinition>();

        public AndroidStylesFile Build()
        {
            var resourcesNode = new XElement("resources");
            foreach (var style in _styles)
            {
                if (!String.IsNullOrEmpty(style.Comment))
                {
                    resourcesNode.Add(new XComment(style.Comment));
                }

                var styleNode = new XElement("style",
                    new XAttribute("name", style.Name),
                    new XAttribute("parent", style.Parent));

                foreach (var item in style.Items)
                {
                    if (!String.IsNullOrEmpty(item.Comment))
                    {
                        styleNode.Add(new XComment(item.Comment));
                    }

                    if (!String.IsNullOrEmpty(item.Name))
                    {
                        styleNode.Add(
                            new XElement("item",
                                new XAttribute("name", item.Name),
                                new XText(item.Value)
                            )
                        );
                    }
                }

                resourcesNode.Add(styleNode);
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

        public class StyleDefinition
        {
            public string Comment { get; set; }
            public string Name { get; set; }
            public string Parent { get; set; } = "MainTheme.Base";
            public IList<ItemDefinition> Items { get; } = new List<ItemDefinition>();
        }
        public class ItemDefinition
        {
            public string Comment { get; set; }
            public string Name { get; set; }
            public string Value { get; set; }
        }
    }
}
