using Corazon.Common;
using System.Collections.Generic;

namespace Corazon.Units.Common.Rate
{
    public class RateUnitType : UnitType
    {
        public override string Name => UnitTypes.Rate.GetDescription();

        public override IEnumerable<Unit> Units
        {
            get
            {
                yield return RateUnit.UnitPerSecond;
                yield return RateUnit.UnitPerHour;
                yield return RateUnit.UnitPerDay;
                yield return RateUnit.UnitPerWeek;
                yield return RateUnit.UnitPerMonth;
            }
        }
    }
}
