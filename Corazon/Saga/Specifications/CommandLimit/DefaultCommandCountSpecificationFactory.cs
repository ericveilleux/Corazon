using System.Collections.Generic;

namespace Corazon.Saga.Specifications.CommandLimit
{
    public class DefaultCommandCountSpecificationFactory : ICommandPublicationLimitSpecificationFactory
    {
        private static readonly Dictionary<ConcurrencyPolicy, int> PolicyValues = new Dictionary<ConcurrencyPolicy, int>
        {
            // Single: 1
            { ConcurrencyPolicy.Single, 1 },
            // Low: 5
            { ConcurrencyPolicy.Low, 5 },
            // High: 50
            { ConcurrencyPolicy.High, 50 }
        };

        public ICommandPublicationLimitSpecification CreateForPolicy(ConcurrencyPolicy policy)
        {
            var countForPolicy = PolicyValues[policy];
            return new CommandCountPublicationLimitSpecification(countForPolicy);
        }
    }
}