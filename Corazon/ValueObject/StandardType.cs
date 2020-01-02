using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Corazon.Common
{
    /// <summary>
    /// This class represents a Standard Type.  Standard Types are descriptive objects that indicate the type of things.  They are seen as Value Objects.
    /// It is implemented here as a Enumeration class (https://lostechies.com/jimmybogard/2008/08/12/enumeration-classes/).
    /// Enumeration classes provide much of the same usability as regular enums, with the added benefit of becoming a destination for behavior.
    /// </summary>
    public abstract class StandardType : IComparable
    {
        protected StandardType()
        {
        }

        protected StandardType(int value, string displayName)
        {
            this.Value = value;
            this.DisplayName = displayName;
        }

        public int Value { get; private set;  }

        public string DisplayName { get; private set; }

        public override string ToString()
        {
            return this.DisplayName;
        }

        public static IEnumerable<T> GetAll<T>() where T : StandardType, new()
        {
            var type = typeof(T);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            foreach (var info in fields)
            {
                var instance = new T();

                if (info.GetValue(instance) is T locatedValue)
                {
                    yield return locatedValue;
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is StandardType otherValue))
            {
                return false;
            }

            var typeMatches = this.GetType() == obj.GetType();
            var valueMatches = this.Value.Equals(otherValue.Value);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public static int AbsoluteDifference(StandardType firstValue, StandardType secondValue)
        {
            var absoluteDifference = Math.Abs(firstValue.Value - secondValue.Value);
            return absoluteDifference;
        }

        public static T FromValue<T>(int value) where T : StandardType, new()
        {
            var matchingItem = Parse<T, int>(value, "value", item => item.Value == value);
            return matchingItem;
        }

        public static T FromDisplayName<T>(string displayName) where T : StandardType, new()
        {
            var matchingItem = Parse<T, string>(displayName, "display name", item => item.DisplayName == displayName);
            return matchingItem;
        }

        private static T Parse<T, TK>(TK value, string description, Func<T, bool> predicate) where T : StandardType, new()
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
            {
                var message = string.Format("'{0}' is not a valid {1} in {2}", value, description, typeof(T));
                throw new ApplicationException(message);
            }

            return matchingItem;
        }

        public int CompareTo(object other)
        {
            return this.Value.CompareTo(((StandardType)other).Value);
        }
    }
}
