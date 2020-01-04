using NodaTime;

namespace Corazon.Saga.Specifications.DelayedStart
{
    internal class TimePeriodDelayedStartSpecification : IDelayedStartSpecification
    {
        private Period _deduplicationPeriodDuration;

        public TimePeriodDelayedStartSpecification(Period deduplicationPeriodDuration)
        {
            this._deduplicationPeriodDuration = deduplicationPeriodDuration;
        }

        public LocalDateTime ComputeStartTime(LocalDateTime referenceTime)
        {
            return referenceTime.Plus(this._deduplicationPeriodDuration);
        }
    }
}