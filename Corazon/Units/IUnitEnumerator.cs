using System.Collections.Generic;

namespace Corazon.Units
{
    public interface IUnitEnumerator
    {
        IEnumerable<Unit> Enumerate();
    }
}
