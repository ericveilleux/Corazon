using Corazon.Units.Common.DataRate;
using Corazon.Units.Common.DataSize;
using Corazon.Units.Common.Magnitude;
using Corazon.Units.Common.Rate;
using Corazon.Units.Common.Time;
using System.Collections.Generic;

namespace Corazon.Units.Common
{
    /// <summary>
    /// Static access to units built into the library.
    /// </summary>
    public static class UnitTypeProvider
    {
        public static IEnumerable<UnitType> GetAll()
        {
            yield return new DataSizeUnitType();
            yield return new DataRateUnitType();
            yield return new MagnitudeUnitType();
            yield return new RateUnitType();
            yield return new TimeUnitType();
        }

        public static UnitType DataSize => new DataSizeUnitType();
        public static UnitType DataRate => new DataRateUnitType();
        public static UnitType Magnitude => new MagnitudeUnitType();
        public static UnitType Rate => new RateUnitType();
        public static UnitType Time => new TimeUnitType();
    }
}
