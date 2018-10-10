using SlnGen.Core;
using System.IO;
using System.Xml.Linq;

namespace SlnGen.Xamarin.Files
{
    public class EntitlementsPListFile : ProjectFile
    {
        public EntitlementsPListFile() : base("Entitlements.plist", false, false)
        {
            XDocument entitilementsDoc = new XDocument(
                new XDocumentType("plist", "-//Apple//DTD PLIST 1.0//EN", "http://www.apple.com/DTDs/PropertyList-1.0.dtd", ""),
                new XElement("plist",
                    new XAttribute("version", "1.0"),
                    new XElement("dict")
                )
            );

            using (var memoryStream = new MemoryStream())
            {
                entitilementsDoc.Save(memoryStream);

                memoryStream.Position = 0;
                using (var streamReader = new StreamReader(memoryStream))
                {
                    FileContents = streamReader.ReadToEnd();
                }
            }
        }
    }
}
