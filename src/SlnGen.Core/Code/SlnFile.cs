using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen.Core.Code
{
    public sealed class SlnFile
    {
        readonly string _fileName;
        public string FileName => _fileName;

        readonly string _fileExtension;
        public string FileExtension => _fileExtension;

        public SlnFile(string fileNameWithExtension)
        {
            int idx = fileNameWithExtension.LastIndexOf(".");
            string fileName = fileNameWithExtension.Substring(0, idx);
            string fileExtension = fileNameWithExtension.Substring(idx + 1, fileNameWithExtension.Length - idx - 1);


        }
    }
}
