﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SlnGen.Core.Utils
{
    /// <summary>
    /// Class for LINQ extensions
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        /// Fluent IEnumerable Extention for filtering a list based on a distinct property.
        /// </summary>
        /// <typeparam name="TSource">Type for the source collection.</typeparam>
        /// <typeparam name="TKey">Type for the property being distincted by.</typeparam>
        /// <param name="source">Source collection.</param>
        /// <param name="keySelector">Function for selecting the distinct property.</param>
        /// <returns>Distincted IEnumerable from Source</returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static bool ContainsAll<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> values)
        {
            foreach(TSource value in values)
            {
                if (!source.Contains(value))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool ContainsAll<TSource>(this IEnumerable<TSource> source, params TSource[] values)
        {
            return source.ContainsAll(values.ToList());
        }
    }
}
