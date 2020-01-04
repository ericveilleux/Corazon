using Corazon.Common;
using System;

namespace Corazon.Units.Common.DataSize
{
    public class DataSizeUnit : Unit
    {
        public static readonly DataSizeUnit Byte = new DataSizeUnit(DataSizeUnitTypes.Byte);
        public static readonly DataSizeUnit Kilobyte = new DataSizeUnit(DataSizeUnitTypes.Kilobyte);
        public static readonly DataSizeUnit Megabyte = new DataSizeUnit(DataSizeUnitTypes.Megabyte);
        public static readonly DataSizeUnit Gigabyte = new DataSizeUnit(DataSizeUnitTypes.Gigabyte);
        public static readonly DataSizeUnit Terabyte = new DataSizeUnit(DataSizeUnitTypes.Terabyte);

        private DataSizeUnit(DataSizeUnitTypes unitType) 
            : this(unitType, 1)
        {
        }

        private DataSizeUnit(DataSizeUnitTypes unitType, double multiplier)
            : base(
                (int)UnitTypes.DataSize,
                unitType.GetDescription(),
                GetBaseMultiplier(unitType), 
                multiplier)
        {
        }

        private static double GetBaseMultiplier(DataSizeUnitTypes unitType)
        {
            return Math.Pow(1024, (int)unitType);
        }
    }
}
