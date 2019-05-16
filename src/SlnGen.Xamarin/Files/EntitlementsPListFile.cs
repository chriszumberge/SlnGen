using SlnGen.Core;
using System.IO;
using System.Xml.Linq;

namespace SlnGen.Xamarin.Files
{
    public class EntitlementsPListFile : ProjectFile
    {
        XElement _plistRoot;

        public EntitlementsPListFile() : base("Entitlements.plist", false, false)
        {
            _plistRoot = new XElement("dict");
        }

        public bool AccessToWiFiInformationEnabled { get; private set; } = false;
        public EntitlementsPListFile Enable_AccessToWiFIInformation()
        {
            if (!AccessToWiFiInformationEnabled)
            {
                _plistRoot.Add(new XElement("key", new XText("com.apple.developer.networking.wifi-info")));
                _plistRoot.Add(new XElement("true"));
                AccessToWiFiInformationEnabled = true;
            }
            return this;
        }

        public bool PushNotificationsEnabled { get; private set; } = false;
        public EntitlementsPListFile Enable_PushNotifications()
        {
            if (!PushNotificationsEnabled)
            {
                _plistRoot.Add(new XElement("key", new XText("aps-environment")));
                _plistRoot.Add(new XElement("string", new XText("development")));
                PushNotificationsEnabled = true;
            }
            return this;
        }

        public bool SiriEnabled { get; private set; } = false;
        public EntitlementsPListFile Enable_Siri()
        {
            if (!SiriEnabled)
            {
                _plistRoot.Add(new XElement("key", new XText("com.apple.developer.siri")));
                _plistRoot.Add(new XElement("true"));
                SiriEnabled = true;
            }
            return this;
        }

        public bool HealthkitEnabled { get; private set; } = false;
        public EntitlementsPListFile Enable_Healthkit()
        {
            if (!HealthkitEnabled)
            {
                _plistRoot.Add(new XElement("key", new XText("com.apple.developer.healthkit")));
                _plistRoot.Add(new XElement("true"));
                HealthkitEnabled = true;
            }
            return this;
        }

        public EntitlementsPListFile Build()
        {
            XDocument entitlementsDoc = new XDocument(
                new XDocumentType("plist", "-//Apple//DTD PLIST 1.0//EN", "http://www.apple.com/DTDs/PropertyList-1.0.dtd", ""),
                new XElement("plist",
                    new XAttribute("version", "1.0"),
                    _plistRoot
                )
            );

            using (var memoryStream = new MemoryStream())
            {
                entitlementsDoc.Save(memoryStream);

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
