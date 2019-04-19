using System;

namespace SlnGen.Core.Utils
{
    /// <summary>
    /// Abstract base class for implementing the TypeSafeEnum pattern.
    /// </summary>
    public abstract class TypeSafeEnum : IEquatable<TypeSafeEnum>
    {
        /// <summary>
        /// Gets the value of the instance.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public int Value { get; }

        /// <summary>
        /// Gets the name of the instance.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }

        /// <summary>
        /// Initializes a new instance of the TypeSafeEnum class with the specified int value and string name.
        /// </summary>
        /// <param name="value">Int for the instance's value.</param>
        /// <param name="name">String for the instance's name.</param>
        protected TypeSafeEnum(int value, string name)
        {
            Value = value;
            Name = name.Replace("_", " ");
        }

        /// <summary>
        /// Gets the name of the instance.
        /// </summary>
        /// <returns>
        /// Name property.
        /// </returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// Hash code for value.
        /// </returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object by comparing Type and Value.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        /// True if equal, otherwise false.
        /// </returns>
        public override bool Equals(object obj)
        {
            TypeSafeEnum other = obj as TypeSafeEnum;
            return Equals(other);
        }

        /// <summary>
        /// Retursn a value indicating whether this instance is equal to a specified TypeSafeEnum value.
        /// </summary>
        /// <param name="obj">The <see cref="TypeSafeEnum" /> to compare with this instance.</param>
        /// <returns>
        /// True if equal, otherwise false.
        /// </returns>
        public bool Equals(TypeSafeEnum obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            return this.Value == obj.Value;
        }

        /// <summary>
        /// Implicit cast for TypeSafeEnum as int.
        /// </summary>
        /// <param name="typeSafeEnum">The instance.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator int(TypeSafeEnum typeSafeEnum)
        {
            return typeSafeEnum.Value;
        }
    }
}
