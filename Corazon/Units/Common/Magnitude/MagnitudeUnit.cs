using Corazon.Common;
using System;

namespace Corazon.Units.Common.Magnitude
{
    public class MagnitudeUnit : Unit
    {
        public static readonly MagnitudeUnit Unit = new MagnitudeUnit(MagnitudeUnitTypes.Unit);
        public static readonly MagnitudeUnit Decade = new MagnitudeUnit(MagnitudeUnitTypes.Decade);
        public static readonly MagnitudeUnit Hundred = new MagnitudeUnit(MagnitudeUnitTypes.Hundred);
        public static readonly MagnitudeUnit Thousand = new MagnitudeUnit(MagnitudeUnitTypes.Thousand);
        public static readonly MagnitudeUnit TenThousand = new MagnitudeUnit(MagnitudeUnitTypes.TenThousand);
        public static readonly MagnitudeUnit HundredThousand = new MagnitudeUnit(MagnitudeUnitTypes.HundredThousand);
        public static readonly MagnitudeUnit Million = new MagnitudeUnit(MagnitudeUnitTypes.Million);

        public static Unit Units(double multiplier)
        {
            return new MagnitudeUnit(MagnitudeUnitTypes.Unit, multiplier);
        }

        private MagnitudeUnit(MagnitudeUnitTypes unitType)
            : this(unitType, 1)
        {
        }

        private MagnitudeUnit(MagnitudeUnitTypes unitType, double multiplier)
            : base(
                (int)UnitTypes.Magnitude,
                unitType.GetDescription(),
                GetBaseMultiplier(unitType), 
                multiplier)
        {
        }

        private static double GetBaseMultiplier(MagnitudeUnitTypes unitType)
        {
            return Math.Pow(10, (int)unitType);
        }
    }
}
