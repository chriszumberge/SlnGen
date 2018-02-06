using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen.Core.Code
{
    public sealed class SGFile
    {
        readonly string _fileName;
        public string FileName => _fileName;

        readonly string _fileExtension;
        public string FileExtension => _fileExtension;

        public SGFile(string fileName, string fileExtension)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            if (fileName.Length == 0)
            {
                throw new ArgumentException("Argument cannot be an empty string.", nameof(fileName));
            }
            if (fileName.StartsWith("."))
            {
                throw new ArgumentException("Argument cannot start with a period.", nameof(fileName));
            }
            if (fileName.EndsWith("."))
            {
                throw new ArgumentException("Argument cannot end with a period.", nameof(fileName));
            }

            if (fileExtension == null)
            {
                throw new ArgumentNullException(nameof(fileExtension));
            }
            if (fileExtension.Length == 0)
            {
                throw new ArgumentException("Argument cannot be an empty string.", nameof(fileExtension));
            }
            if (fileExtension.Contains("."))
            {
                throw new ArgumentException("Argument cannot contain a period.", nameof(fileExtension));
            }

            _fileName = fileName;
            _fileExtension = fileExtension;
        }

        public SGFile(string fileNameWithExtension)
        {
            if (fileNameWithExtension == null)
            {
                throw new ArgumentNullException(nameof(fileNameWithExtension));
            }
            if (fileNameWithExtension.Length == 0)
            {
                throw new ArgumentException("Argument cannot be an empty string.", nameof(fileNameWithExtension));
            }
            if (fileNameWithExtension.StartsWith("."))
            {
                throw new ArgumentException("Argument cannot start with a period.", nameof(fileNameWithExtension));
            }
            if (fileNameWithExtension.EndsWith("."))
            {
                throw new ArgumentException("Argument cannot end with a period.", nameof(fileNameWithExtension));
            }

            int idx = fileNameWithExtension.LastIndexOf(".");

            if (idx < 0)
            {
                throw new ArgumentException("File extension could not be derived from filename string.", nameof(fileNameWithExtension));
            }

            string fileName = fileNameWithExtension.Substring(0, idx);
            string fileExtension = fileNameWithExtension.Substring(idx + 1, fileNameWithExtension.Length - idx - 1);

            _fileName = fileName;
            _fileExtension = fileExtension;
        }
    }
}
