using System;

namespace Corazon.Units.Exceptions
{
    public class IncompatibleUnitException : Exception
    {
        public readonly Unit Unit1;

        public readonly Unit Unit2;

        public IncompatibleUnitException(Unit unit1, Unit unit2)
            : base(string.Format("Mismatched units: {0} and {1}", unit1, unit2))
        {
            this.Unit1 = unit1;
            this.Unit2 = unit2;
        }
    }
}