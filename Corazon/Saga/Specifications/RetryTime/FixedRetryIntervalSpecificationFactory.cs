using NodaTime;

namespace Corazon.Saga.Specifications.RetryTime
{
    public class FixedRetryIntervalSpecificationFactory : IRetryTimeSpecificationFactory
    {
        private readonly Period _minimalRetryInterval;

        private readonly Period _allowableDuration;

        public FixedRetryIntervalSpecificationFactory(Period minimalRetryInterval, Period allowableDuration)
        {
            this._minimalRetryInterval = minimalRetryInterval;
            this._allowableDuration = allowableDuration;
        }

        public IRetryTimeSpecification CreateForPolicy(LifetimePolicy policy)
        {
            return new PeriodBasedRetryTimeSpecification(this._minimalRetryInterval, this._allowableDuration);
        }
    }
}