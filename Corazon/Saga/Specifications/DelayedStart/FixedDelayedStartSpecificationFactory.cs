using NodaTime;

namespace Corazon.Saga.Specifications.DelayedStart
{
    public class FixedDelayedStartSpecificationFactory : IDelayedStartSpecificationFactory
    {
        private Period _deduplicationPeriodDuration;

        public FixedDelayedStartSpecificationFactory(Period deduplicationPeriodDuration)
        {
            this._deduplicationPeriodDuration = deduplicationPeriodDuration;
        }

        public IDelayedStartSpecification CreateForPolicy(DeduplicationPolicy policy)
        {
            return new TimePeriodDelayedStartSpecification(this._deduplicationPeriodDuration);
        }
    }
}