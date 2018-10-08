using SlnGen.Core;
using SlnGen.Core.Code;

namespace SlnGen.Xamarin
{
    public class AndroidResourceProjectFile : ProjectFile
    {
        public AndroidResourceProjectFile(SGFile file) : base(file, false, false)
        {
        }

        public AndroidResourceProjectFile(string fileName) : base(fileName, false, false)
        {
        }

        public AndroidResourceProjectFile(string fileName, string fileContents) : base(fileName, false, false, fileContents)
        {
        }
    }
}
