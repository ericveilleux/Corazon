using Corazon.Units.Exceptions;

namespace Corazon.Units
{
    /// <summary>
    /// This class represents a value combined with a unit to form a quantity of something.
    /// </summary>
    /// <typeparam name="TUnit"></typeparam>
    public class Quantity<TUnit> where TUnit : Unit
    {
        public double Value { get; private set; }

        public TUnit Unit { get; private set; }

        public Quantity(double value, TUnit unit)
        {
            this.Value = value;
            this.Unit = unit;
        }

        public static OfQuantity Of(double quantity)
        {
            return new OfQuantity(quantity);
        }

        public static Quantity<TUnit> Min(Quantity<TUnit> a, Quantity<TUnit> b)
        {
            ValidateCompatible(a, b);
            return a < b ? a : b;
        }

        public static Quantity<TUnit> Max(Quantity<TUnit> a, Quantity<TUnit> b)
        {
            ValidateCompatible(a, b);
            return b < a ? a : b;
        }

        public static Quantity<TUnit> operator +(Quantity<TUnit> q1, Quantity<TUnit> q2)
        {
            ValidateCompatible(q1, q2);
            var q2InOtherUnit = UnitConverter<TUnit>.ConvertQuantity(q2, q1.Unit);
            return new Quantity<TUnit>(q1.Value + q2InOtherUnit.Value, q1.Unit);
        }

        public static Quantity<TUnit> operator -(Quantity<TUnit> q1, Quantity<TUnit> q2)
        {
            ValidateCompatible(q1, q2);
            var q2InOtherUnit = UnitConverter<TUnit>.ConvertQuantity(q2, q1.Unit);
            return new Quantity<TUnit>(q1.Value - q2InOtherUnit.Value, q1.Unit);
        }

        public static Quantity<TUnit> operator -(Quantity<TUnit> q1)
        {
            return new Quantity<TUnit>(-q1.Value, q1.Unit);
        }

        public static bool operator >(Quantity<TUnit> q1, Quantity<TUnit> q2)
        {
            ValidateCompatible(q1, q2);
            return q1.GetBaseValue() > q2.GetBaseValue();
        }

        public static bool operator <(Quantity<TUnit> q1, Quantity<TUnit> q2)
        {
            ValidateCompatible(q1, q2);
            return q1.GetBaseValue() < q2.GetBaseValue();
        }

        public static bool operator >=(Quantity<TUnit> q1, Quantity<TUnit> q2)
        {
            ValidateCompatible(q1, q2);
            return q1.GetBaseValue() >= q2.GetBaseValue();
        }

        public static bool operator <=(Quantity<TUnit> q1, Quantity<TUnit> q2)
        {
            ValidateCompatible(q1, q2);
            return q1.GetBaseValue() <= q2.GetBaseValue();
        }

        public static bool operator ==(Quantity<TUnit> q1, Quantity<TUnit> q2)
        {
            return q1.Equals(q2);
        }

        public static bool operator !=(Quantity<TUnit> q1, Quantity<TUnit> q2)
        {
            return !q1.Equals(q2);
        }

        public bool Equals(Quantity<TUnit> other)
        {
            return AreCompatible(this, other) && this.GetBaseValue() == other.GetBaseValue();
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }
            return obj is Quantity<TUnit> && this.Equals((Quantity<TUnit>)obj);
        }

        public override int GetHashCode()
        {
            return this.GetBaseValue().GetHashCode();
        }

        public static bool AreCompatible(Quantity<TUnit> q1, Quantity<TUnit> q2)
        {
            return q1.Unit.Value.Equals(q2.Unit.Value);
        }

        private static void ValidateCompatible(Quantity<TUnit> q1, Quantity<TUnit> q2)
        {
            if (!AreCompatible(q1, q2))
            {
                throw new IncompatibleUnitException(q1.Unit, q2.Unit);
            }
        }

        private double GetBaseValue()
        {
            return this.Value * this.Unit.ValueMultiplier;
        }
    }

    public class OfQuantity
    {
        private readonly double _qty;

        public OfQuantity(double quantity)
        {
            this._qty = quantity;
        }

        public Quantity<TUnit> In<TUnit>(TUnit unit) where TUnit : Unit
        {
            return new Quantity<TUnit>(this._qty, unit);
        }
    }
}
