using Corazon.Common;
using Corazon.Units.Common.Magnitude;
using Corazon.Units.Common.Time;
using System;

namespace Corazon.Units.Common.Rate
{
    public class RateUnit : Unit
    {
        public static readonly RateUnit UnitPerSecond = new RateUnit(MagnitudeUnitTypes.Unit, TimeUnitTypes.Second);
        public static readonly RateUnit UnitPerHour = new RateUnit(MagnitudeUnitTypes.Unit, TimeUnitTypes.Hour);
        public static readonly RateUnit UnitPerDay = new RateUnit(MagnitudeUnitTypes.Unit, TimeUnitTypes.Day);
        public static readonly RateUnit UnitPerWeek = new RateUnit(MagnitudeUnitTypes.Unit, TimeUnitTypes.Week);
        public static readonly RateUnit UnitPerMonth = new RateUnit(MagnitudeUnitTypes.Unit, TimeUnitTypes.Month);

        private RateUnit(MagnitudeUnitTypes magnitudeUnitType, TimeUnitTypes timeUnitType)
            : this(magnitudeUnitType, timeUnitType, 1)
        {
        }

        private RateUnit(MagnitudeUnitTypes magnitudeUnitType, TimeUnitTypes timeUnitType, double multiplier)
            : base(
                (int)UnitTypes.Rate,
                $"{magnitudeUnitType.GetDescription()} {timeUnitType.GetDescription()}",
                GetBaseMultiplier(magnitudeUnitType, timeUnitType), 
                multiplier)
        {
        }

        private static double GetBaseMultiplier(MagnitudeUnitTypes magnitudeUnitType, TimeUnitTypes timeUnitType)
        {
            return Math.Pow(10, (int)magnitudeUnitType) * (double)timeUnitType;
        }
    }
}
