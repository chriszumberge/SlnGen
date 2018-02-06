using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace SlnGen.Core.Utils
{
    /// <summary>
    /// Represents a list of Types that are only of a certain type or are assignable from a certain type.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IList{System.Type}" />
    public class TypeFilteredList : IList<Type>
    {
        /// <summary>
        /// The filtering type
        /// </summary>
        readonly Type mFilterType;

        /// <summary>
        /// The types list.
        /// </summary>
        List<Type> mTypes = new List<Type>();


        /// <summary>
        /// Initializes a new instance of the <see cref="TypeFilteredList"/> class.
        /// </summary>
        /// <param name="filterType">Type of the filter.</param>
        public TypeFilteredList(Type filterType)
        {
            this.mFilterType = filterType;
        }

        /// <summary>
        /// Checks the type.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="ArgumentException">Thrown if the item is not assignable from the specified filter type.</exception>
        void CheckType(Type item)
        {
            if (item != null && !mFilterType.GetTypeInfo().IsAssignableFrom(item.GetTypeInfo()))
            {
                throw new ArgumentException(nameof(item));
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Type"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="Type"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns>The <see cref="Type"/> stored at the specified index.</returns>
        public Type this[int index]
        {
            get
            {
                return this.mTypes[index];
            }

            set
            {
                CheckType(value);
                this.mTypes[index] = value;
            }
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        public int Count => mTypes.Count;

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        public void Add(Type item)
        {
            CheckType(item);
            mTypes.Add(item);
        }

        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        public void Clear() => mTypes.Clear();

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        /// <returns>
        /// true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.
        /// </returns>
        public bool Contains(Type item) => mTypes.Contains(item);

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        public void CopyTo(Type[] array, int arrayIndex) => mTypes.CopyTo(array, arrayIndex);

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<Type> GetEnumerator() => mTypes.GetEnumerator();

        /// <summary>
        /// Determines the index of a specific item in the <see cref="T:System.Collections.Generic.IList`1" />.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.IList`1" />.</param>
        /// <returns>
        /// The index of <paramref name="item" /> if found in the list; otherwise, -1.
        /// </returns>
        public int IndexOf(Type item) => mTypes.IndexOf(item);

        /// <summary>
        /// Inserts an item to the <see cref="T:System.Collections.Generic.IList`1" /> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
        /// <param name="item">The object to insert into the <see cref="T:System.Collections.Generic.IList`1" />.</param>
        public void Insert(int index, Type item)
        {
            CheckType(item);
            mTypes.Insert(index, item);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        /// <returns>
        /// true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </returns>
        public bool Remove(Type item) => mTypes.Remove(item);

        /// <summary>
        /// Removes the <see cref="T:System.Collections.Generic.IList`1" /> item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        public void RemoveAt(int index) => mTypes.RemoveAt(index);

        /// <summary>
        /// Removes all.
        /// </summary>
        /// <param name="match">The match.</param>
        public void RemoveAll(Predicate<Type> match) => mTypes.RemoveAll(match);

        /// <summary>
        /// Removes the range.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        public void RemoveRange(int index, int count) => mTypes.RemoveRange(index, count);

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() => mTypes.GetEnumerator();
    }
}
