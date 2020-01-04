using Corazon.Common;
using System.Collections.Generic;

namespace Corazon.Units.Common.DataSize
{
    public class DataSizeUnitType : UnitType
    {
        public override string Name => UnitTypes.DataSize.GetDescription();

        public override IEnumerable<Unit> Units
        {
            get
            {
                yield return DataSizeUnit.Byte;
                yield return DataSizeUnit.Kilobyte;
                yield return DataSizeUnit.Megabyte;
                yield return DataSizeUnit.Gigabyte;
                yield return DataSizeUnit.Terabyte;
            }
        }
    }
}
