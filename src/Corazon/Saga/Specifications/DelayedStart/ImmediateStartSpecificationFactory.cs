using NodaTime;

namespace Corazon.Saga.Specifications.DelayedStart
{
    public class ImmediateStartSpecificationFactory : IDelayedStartSpecificationFactory
    {
        public IDelayedStartSpecification CreateForPolicy(DeduplicationPolicy policy)
        {
            return new TimePeriodDelayedStartSpecification(Period.Zero);
        }
    }
}