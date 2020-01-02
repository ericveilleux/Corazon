using System;
using System.Collections.Generic;
using System.Reflection;

namespace Corazon
{
    /// <summary>
    /// Implements the base contract for the Value Object pattern
    /// The attributes that make up a value object should form a conceptual whole.  A value object should be immutable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ValueObject<T> : IEquatable<T>
        where T : ValueObject<T>
    {
        public virtual bool Equals(T other)
        {
            if (other == null)
            {
                return false;
            }

            var t = this.GetType();

            var otherType = other.GetType();

            if (t != otherType)
            {
                return false;
            }

            var fields = t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            foreach (var field in fields)
            {
                var value1 = field.GetValue(other);

                var value2 = field.GetValue(this);

                if (value1 == null)
                {
                    if (value2 != null)
                    {
                        return false;
                    }
                }
                else if (!value1.Equals(value2))
                {
                    return false;
                }
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var other = obj as T;

            return this.Equals(other);
        }

        public override int GetHashCode()
        {
            var fields = this.GetFields();
            const int StartValue = 17;
            const int Multiplier = 59;

            var hashCode = StartValue;

            foreach (var field in fields)
            {
                var value = field.GetValue(this);

                if (value != null)
                {
                    hashCode = hashCode * Multiplier + value.GetHashCode();
                }
            }

            return hashCode;
        }

        private IEnumerable<FieldInfo> GetFields()
        {
            var t = this.GetType();

            var fields = new List<FieldInfo>();

            while (t != typeof(object))
            {
                fields.AddRange(t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public));

                t = t.BaseType;
            }

            return fields;
        }

        public static bool operator ==(ValueObject<T> x, ValueObject<T> y)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (x is null || y is null)
            {
                return false;
            }

            return x.Equals(y);
        }

        public static bool operator !=(ValueObject<T> x, ValueObject<T> y)
        {
            return !(x == y);
        }
    }
}