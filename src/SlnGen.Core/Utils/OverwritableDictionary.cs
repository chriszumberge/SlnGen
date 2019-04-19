using System;
using System.Collections.Generic;

namespace SlnGen.Core.Utils
{
    /// <summary>
    /// Subclass of System.Collections.Generic Dictionary that allows the automatic overwriting of
    /// existing keys.
    /// </summary>
    /// <typeparam name="TKey">Type for dictionary keys.</typeparam>
    /// <typeparam name="TValue">Type for dictionary values.</typeparam>
    /// <seealso cref="System.Collections.Generic.Dictionary{TKey, TValue}" />
    public sealed class OverwritableDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        /// <summary>
        /// Adds the specified key and value to the dictionary. If there exists an item with a matching key, it removes the existing
        /// item and adds the new one.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add. The value can be null for reference types.</param>
        /// <exception cref="ArgumentNullException">Thrown if the provided key value is null.</exception>
        new public void Add(TKey key, TValue value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (this.ContainsKey(key))
            {
                this.Remove(key);
            }

            base.Add(key, value);
        }
    }
}
