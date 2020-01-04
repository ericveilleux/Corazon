using Corazon.Common;

namespace Corazon.Units.Common.Time
{
    public class TimeUnit : Unit
    {
        public static readonly TimeUnit Second = new TimeUnit(TimeUnitTypes.Second);
        public static readonly TimeUnit Minute = new TimeUnit(TimeUnitTypes.Minute);
        public static readonly TimeUnit Hour = new TimeUnit(TimeUnitTypes.Hour);
        public static readonly TimeUnit Day = new TimeUnit(TimeUnitTypes.Day);
        public static readonly TimeUnit Week = new TimeUnit(TimeUnitTypes.Week);
        public static readonly TimeUnit Month = new TimeUnit(TimeUnitTypes.Month);

        public static TimeUnit Seconds(double multiplier)
        {
            return new TimeUnit(TimeUnitTypes.Second, multiplier);
        }

        public static TimeUnit Hours(double multiplier)
        {
            return new TimeUnit(TimeUnitTypes.Hour, multiplier);
        }

        private TimeUnit(TimeUnitTypes unitType)
            : this(unitType, 1)
        {
        }

        private TimeUnit(TimeUnitTypes unitType, double multiplier)
            : base(
                (int)UnitTypes.Time,
                unitType.GetDescription(),
                GetBaseMultiplier(unitType), 
                multiplier)
        {
        }

        private static double GetBaseMultiplier(TimeUnitTypes unitType)
        {
            return (double)unitType;
        }
    }
}
