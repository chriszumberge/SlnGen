using System.Collections.Generic;
using System.IO;

namespace SlnGen.Core.Utils
{
    public sealed class RelativePathBuilder
    {
        List<string> _path = new List<string>();

        public RelativePathBuilder AppendPath(string path)
        {
            _path.Add(path);
            return this;
        }

        public override string ToString()
        {
            return Path.Combine(_path.ToArray());
        }

        public string ToPath()
        {
            return this.ToString();
        }
    }

    public static class RelativePath
    {
        public const string Up_Directory = @"..\";
        public const string This_Directory = @".\";
        public const string C_Drive = @"C:\";
    }
}
