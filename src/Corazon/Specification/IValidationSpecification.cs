namespace Corazon.Specification
{
    /// <summary>
    /// Implements the base contract for the Specification pattern.  
    /// This version defines the semantics used to validate a domain entity against a specification.
    /// </summary>
    public interface IValidationSpecification<in T>
    {
        bool IsSatisfiedBy(T instance);
    }
}
