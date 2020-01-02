namespace Corazon.Specification
{
    /// <summary>
    /// Implements the base contract for the Specification pattern.
    /// This version defines the semantics used to query which domain entity satisfy a specification.
    /// </summary>
    public interface IQuerySpecification<out T>
    {
        T FindSatisfying();
    }
}