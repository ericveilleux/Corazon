using NodaTime;

namespace Corazon.Saga.Specifications.RetryTime
{
    internal class PeriodBasedRetryTimeSpecification : IRetryTimeSpecification
    {
        private readonly Period _minimalRetryInterval;

        private readonly Period _allowableDuration;

        public PeriodBasedRetryTimeSpecification(Period minimalRetryInterval, Period allowableDuration)
        {
            this._minimalRetryInterval = minimalRetryInterval;
            this._allowableDuration = allowableDuration;
        }

        public LocalDateTime? ComputeNextRetryTime(LocalDateTime firstExecutionTime, LocalDateTime lastExecutionTime)
        {
            var nextExecutionTime = lastExecutionTime.Plus(this._minimalRetryInterval);
            if (nextExecutionTime < firstExecutionTime.Plus(this._allowableDuration))
            {
                return nextExecutionTime;
            }
        
            return null;
        }
    }
}