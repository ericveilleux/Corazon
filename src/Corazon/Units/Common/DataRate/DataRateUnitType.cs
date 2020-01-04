using Corazon.Common;
using System.Collections.Generic;

namespace Corazon.Units.Common.DataRate
{
    public class DataRateUnitType : UnitType
    {
        public override string Name => UnitTypes.DataRate.GetDescription();

        public override IEnumerable<Unit> Units
        {
            get
            {
                yield return DataRateUnit.BytePerSecond;
                yield return DataRateUnit.KilobytePerSecond;
                yield return DataRateUnit.MegabytePerSecond;
                yield return DataRateUnit.GigabytePerSecond;
                yield return DataRateUnit.MegabytePerHour;
                yield return DataRateUnit.GigabytePerHour;
            }
        }
    }
}
