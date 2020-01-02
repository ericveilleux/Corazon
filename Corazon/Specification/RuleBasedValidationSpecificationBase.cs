using System.Collections.Generic;
using System.Linq;

namespace Corazon.Specification
{
    /// <summary>
    /// This is an implementation of a ValidationSpecification that uses a set of rules to validate the specification.
    /// It provides a finer-grained mechanism to implement a specification as it becomes possible to know which rule(s)
    /// that was not respected.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RuleBasedValidationSpecification<T> : IValidationSpecification<T>
    {
        public bool IsSatisfiedBy(T instance)
        {
            return !this.CollectBrokenRules(instance).Any();
        }

        public abstract IReadOnlyCollection<Rule> CollectBrokenRules(T instance);
    }
}