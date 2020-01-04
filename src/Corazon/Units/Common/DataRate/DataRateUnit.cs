using Corazon.Common;
using Corazon.Units.Common.DataSize;
using Corazon.Units.Common.Time;
using System;

namespace Corazon.Units.Common.DataRate
{
    public class DataRateUnit : Unit
    {
        public static readonly DataRateUnit BytePerSecond = new DataRateUnit(DataSizeUnitTypes.Byte, TimeUnitTypes.Second);
        public static readonly DataRateUnit KilobytePerSecond = new DataRateUnit(DataSizeUnitTypes.Kilobyte, TimeUnitTypes.Second);
        public static readonly DataRateUnit MegabytePerSecond = new DataRateUnit(DataSizeUnitTypes.Megabyte, TimeUnitTypes.Second);
        public static readonly DataRateUnit GigabytePerSecond = new DataRateUnit(DataSizeUnitTypes.Gigabyte, TimeUnitTypes.Second);
        public static readonly DataRateUnit MegabytePerHour = new DataRateUnit(DataSizeUnitTypes.Megabyte, TimeUnitTypes.Hour);
        public static readonly DataRateUnit GigabytePerHour = new DataRateUnit(DataSizeUnitTypes.Gigabyte, TimeUnitTypes.Hour);

        public static DataRateUnit BytesPerSecond(double multiplier)
        {
            return new DataRateUnit(DataSizeUnitTypes.Byte, TimeUnitTypes.Second, multiplier);
        }

        public static DataRateUnit GigabytesPerHour(double multiplier)
        {
            return new DataRateUnit(DataSizeUnitTypes.Gigabyte, TimeUnitTypes.Hour, multiplier);
        }

        private DataRateUnit(DataSizeUnitTypes sizeUnitType, TimeUnitTypes timeUnitType)
            : this(sizeUnitType, timeUnitType, 1)
        {
        }

        private DataRateUnit(DataSizeUnitTypes sizeUnitType, TimeUnitTypes timeUnitType, double multiplier)
            : base(
                (int)UnitTypes.DataRate,
                $"{sizeUnitType.GetDescription()} {timeUnitType.GetDescription()}",
                GetBaseMultiplier(sizeUnitType, timeUnitType), 
                multiplier)
        {
        }

        private static double GetBaseMultiplier(DataSizeUnitTypes sizeUnitType, TimeUnitTypes timeUnitType)
        {
            return Math.Pow(1024, (int)sizeUnitType) * (double)timeUnitType;
        }
    }
}
