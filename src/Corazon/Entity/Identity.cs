using Corazon.Common;
using System;

namespace Corazon
{
    /// <summary>
    /// Wrapper class that represents the identity of an Entity.
    /// It is represented by a Guid, more specifically a sequentially generated one
    /// </summary>
    public abstract class Identity
    {
        public Identity()
        {
            this.Value = SequentialGuid.NewSequentialGuid();
        }

        public Identity(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Identity cannot be empty Guid", nameof(id));
            }

            this.Value = id;
        }

        public Identity(Identity id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            this.Value = id.Value;
        }

        public Guid Value { get; protected set; }

        private bool Equals(Identity other)
        {
            return this.Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var other = obj as Identity;
            return other != null && this.Equals(other);
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }

        public static bool operator ==(Identity left, Identity right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Identity left, Identity right)
        {
            return !Equals(left, right);
        }
    }
}