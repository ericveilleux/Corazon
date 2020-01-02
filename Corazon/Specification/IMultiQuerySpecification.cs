using System.Collections.Generic;

namespace Corazon.Specification
{
    /// <summary>
    /// Implements the base contract for the Specification pattern.
    /// This version defines the semantics used to query which domain entity(ies) satisfy a specification.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMultiQuerySpecification<out T>
    {
        IEnumerable<T> SelectSatisfying();
    }
}