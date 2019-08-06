using System;
using System.Collections.Generic;
using ZESoft.SlnGen.Core.Files;

namespace ZESoft.SlnGen.Web.Files
{
    public class NetCoreAPILaunchSettingsJsonFile : JsonFile
    {
        static Dictionary<string, object> s_DefaultValues = new Dictionary<string, object>
        {
            { "$schema", "http://json.schemastore.org/launchsettings.json" }
        };
        string[] s_DefaultAppUrls = new string[] { "https://localhost:5001", "http://localhost:5000" };

        readonly string _projectName;
        public NetCoreAPILaunchSettingsJsonFile(string projectName, string applicationUrls = null, int sslPort = 44371,
                                                string launchUrl = "api/values", bool windowsAuth = false, bool anonymousAuth = true)
            : base("launchSettings.json", s_DefaultValues)
        {
            _projectName = projectName;
            applicationUrls = applicationUrls ?? String.Join(";", s_DefaultAppUrls);

            WithProperties(
                new Dictionary<string, object>
                {
                    { "iisSettings", new Dictionary<string, object>
                    {
                        { "windowsAuthentication", windowsAuth },
                        { "anonymousAuthentication", anonymousAuth },
                        { "iisExpress", new Dictionary<string, object>
                        {
                            { "applicationUrl", "http://localhost:1937" },
                            { "sslPort", sslPort }
                        }
                        }
                    }
                    },
                    { "profiles", new Dictionary<string, object>
                    {
                        { "IIS Express", new Dictionary<string, object>
                        {
                            { "commandName", "IISExpress" },
                            { "launchBrowser", true },
                            { "launchUrl", launchUrl },
                            { "environmentVariables", new Dictionary<string, object>
                            {
                                { "ASPNETCORE_ENVIRONMENT", "Development" }
                            }
                            }
                        }
                        },
                        { projectName, new Dictionary<string, object>
                        {
                            { "commandName", "Project" },
                            { "launchBrowser", true },
                            { "launchUrl", launchUrl },
                            { "applicationUrl", applicationUrls },
                            { "environmentVariables", new Dictionary<string, object>
                            {
                                { "ASPNETCORE_ENVIRONMENT", "Development" }
                            }
                            }
                        }
                        }
                    }
                    },
                }
            );
        }
    }
}
