using System;

namespace Corazon
{
    /// <summary>
    /// Implements the base contract for the Entity pattern.
    /// "An entity is anything that has continuity through a life cycle and distinctions independent of attributes." -Evans
    /// </summary>
    /// <typeparam name="TIdentity"></typeparam>
    public abstract class Entity<TIdentity> where TIdentity : Identity, new()
    {
        public TIdentity Id { get; protected set; }

        protected Entity()
        {
            this.Id = new TIdentity();
        }

        protected Entity(TIdentity identity)
        {
            this.Id = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        protected bool Equals(Entity<TIdentity> other)
        {
            return this.Id.Equals(other.Id);
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

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((Entity<TIdentity>)obj);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public static bool operator ==(Entity<TIdentity> x, Entity<TIdentity> y)
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

        public static bool operator !=(Entity<TIdentity> x, Entity<TIdentity> y)
        {
            return !(x == y);
        }
    }
}