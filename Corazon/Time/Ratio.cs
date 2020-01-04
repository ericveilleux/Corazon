using System;
using System.Globalization;

namespace Corazon.Time
{
    public struct Ratio : IComparable<Ratio>, IComparable
    {
        public readonly decimal Value;

        public Ratio(decimal value)
        {
            if (value < 0 || value > 100)
            {
                throw new ArgumentOutOfRangeException("value", "Need to be between 0 and 100.");
            }

            this.Value = value;
        }

        public static readonly Ratio Zero = new Ratio(0.0M);

        public static readonly Ratio OneHundred = new Ratio(100M);

        public Ratio Inverse()
        {
            return new Ratio(OneHundred.Value - this.Value);
        }

        public static Ratio operator +(Ratio p1, Ratio p2)
        {
            return new Ratio(p1.Value + p2.Value);
        }

        public static Ratio operator -(Ratio p1, Ratio p2)
        {
            return new Ratio(p1.Value - p2.Value);
        }

        public static Ratio operator *(Ratio p1, Ratio p2)
        {
            return new Ratio(p1.Value * p2.Value);
        }

        public static bool operator >(Ratio p1, Ratio p2)
        {
            return p1.Value > p2.Value;
        }

        public static bool operator <(Ratio p1, Ratio p2)
        {
            return p1.Value < p2.Value;
        }

        public static bool operator >=(Ratio p1, Ratio p2)
        {
            return p1.Value >= p2.Value;
        }

        public static bool operator <=(Ratio p1, Ratio p2)
        {
            return p1.Value <= p2.Value;
        }

        public static bool operator ==(Ratio p1, Ratio p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Ratio p1, Ratio p2)
        {
            return !p1.Equals(p2);
        }

        public static Ratio operator *(Ratio p1, decimal p2)
        {
            return new Ratio(p1.Value / 100M * p2);
        }

        public static Ratio operator *(decimal p1, Ratio p2)
        {
            return new Ratio(p2.Value / 100M * p1);
        }

        public bool Equals(Ratio other)
        {
            return this.Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            return obj is Ratio && this.Equals((Ratio)obj);
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "Value: {0}", this.Value);
        }

        public int CompareTo(Ratio other)
        {
            return this.Value.CompareTo(other.Value);
        }

        public int CompareTo(object obj)
        {
            // Will throw if wrong type.
            return this.CompareTo((Ratio)obj);
        }
    }
}
