using System.Collections.Generic;

namespace Corazon.Units
{
    public abstract class UnitType
    {
        public abstract string Name { get; }

        public abstract IEnumerable<Unit> Units { get; }
    }
}
