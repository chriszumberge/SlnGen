using System;

namespace SlnGen.Core.Files
{
    public class ConfigFile : ProjectFile
    {
        public ConfigFile(string fileName) : 
            base(String.Concat(fileName, ".config"), false, false, null)
        {

        }
    }
}
