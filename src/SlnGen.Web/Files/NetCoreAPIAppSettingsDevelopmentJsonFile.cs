﻿using System.Collections.Generic;
using SlnGen.Core.Files;

namespace SlnGen.Web.Files
{
    public class NetCoreAPIAppSettingsDevelopmentJsonFile : JsonFile
    {
        static Dictionary<string, object> s_DefaultValues = new Dictionary<string, object>
        {
            { "Logging",
                new Dictionary<string, object>
                {
                    { "LogLevel", new Dictionary<string, object>
                        {
                            { "Default", "Warning" },
                            { "System", "Information" },
                            { "Microsoft", "Information" }
                        }
                    }
                }
            }
        };

        public NetCoreAPIAppSettingsDevelopmentJsonFile() :
            base("appsettings.Development.json", s_DefaultValues)
        { }
    }
}