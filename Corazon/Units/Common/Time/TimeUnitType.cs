using Corazon.Common;
using System.Collections.Generic;

namespace Corazon.Units.Common.Time
{
    public class TimeUnitType : UnitType
    {
        public override string Name => UnitTypes.Time.GetDescription();

        public override IEnumerable<Unit> Units
        {
            get
            {
                yield return TimeUnit.Second;
                yield return TimeUnit.Minute;
                yield return TimeUnit.Hour;
                yield return TimeUnit.Day;
                yield return TimeUnit.Week;
                yield return TimeUnit.Month;
            }
        }
    }
}
