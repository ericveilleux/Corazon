using System.Collections.Generic;
using NodaTime;

namespace Corazon.Saga.Specifications.DelayedStart
{
    public class DefaultDelayedStartSpecificationFactory : IDelayedStartSpecificationFactory
    {
        private static Dictionary<DeduplicationPolicy, Period> PolicyValues = new Dictionary<DeduplicationPolicy, Period>
        {
            // None: instant
            { DeduplicationPolicy.None, Period.Zero },
            // High: 1 minute
            { DeduplicationPolicy.High, Period.FromMinutes(1) },
            // Medium: 15 minutes
            { DeduplicationPolicy.Medium, Period.FromMinutes(15) },
            // Low: 1 hour
            { DeduplicationPolicy.Low, Period.FromHours(1) }
        };

        public IDelayedStartSpecification CreateForPolicy(DeduplicationPolicy policy)
        {
            var durationForPolicy = PolicyValues[policy];
            return new TimePeriodDelayedStartSpecification(durationForPolicy);
        }
    }
}