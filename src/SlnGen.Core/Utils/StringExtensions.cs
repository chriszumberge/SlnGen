using System;

namespace SlnGen.Core.Utils
{
    public static class StringExtensions
    {
        public static string[] BreakIntoLines(this string str)
        {
            return str.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
        }
    }
}
