using Corazon.Common;
using System.Collections.Generic;

namespace Corazon.Units.Common.Magnitude
{
    public class MagnitudeUnitType : UnitType
    {
        public override string Name => UnitTypes.Magnitude.GetDescription();

        public override IEnumerable<Unit> Units
        {
            get
            {
                {
                    yield return MagnitudeUnit.Unit;
                    yield return MagnitudeUnit.Decade;
                    yield return MagnitudeUnit.Thousand;
                    yield return MagnitudeUnit.TenThousand;
                    yield return MagnitudeUnit.HundredThousand;
                    yield return MagnitudeUnit.Million;
                }
            }
        }
    }
}
