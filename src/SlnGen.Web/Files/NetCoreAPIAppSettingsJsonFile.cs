using System.Collections.Generic;
using ZESoft.SlnGen.Core.Files;

namespace ZESoft.SlnGen.Web.Files
{
    public class NetCoreAPIAppSettingsJsonFile : JsonFile
    {
        static Dictionary<string, object> s_DefaultValues = new Dictionary<string, object>
        {
            { "Logging",
                new Dictionary<string, object>
                {
                    { "LogLevel", new Dictionary<string, object>
                        {
                            { "Default", "Warning" }
                        }
                    }
                }
            },
            { "AllowedHosts", "*" }
        };

        public NetCoreAPIAppSettingsJsonFile() :
            base("appsettings", s_DefaultValues)
        { }
    }
}
