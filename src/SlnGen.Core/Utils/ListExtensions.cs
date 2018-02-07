using System;
using System.Collections.Generic;

namespace SlnGen.Core.Utils
{
    /// <summary>
    /// Class for List extensions
    /// </summary>
    public static class ListExtensions
    {
        public static string[] BreakIntoLines<T>(this List<T> objs)
        {
            List<string> listLines = new List<string>();
            foreach (var obj in objs)
            {
                string[] lines = obj.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (string line in lines)
                {
                    listLines.Add(line);
                }
            }
            return listLines.ToArray();
        }
    }
}
