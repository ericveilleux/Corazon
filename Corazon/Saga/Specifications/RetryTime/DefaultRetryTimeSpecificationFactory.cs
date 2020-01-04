using System.Collections.Generic;
using NodaTime;

namespace Corazon.Saga.Specifications.RetryTime
{
    public class DefaultRetryTimeSpecificationFactory : IRetryTimeSpecificationFactory
    {
        private struct RetryPolicyPeriodAndDuration
        {
            public Period MinimalRetryInterval;
            public Period AllowableDuration;

            public RetryPolicyPeriodAndDuration(Period minimalRetryInterval, Period allowableDuration)
            {
                this.MinimalRetryInterval = minimalRetryInterval;
                this.AllowableDuration = allowableDuration;
            }
        }

        private static readonly Dictionary<LifetimePolicy, RetryPolicyPeriodAndDuration> PolicyValues = new Dictionary<LifetimePolicy, RetryPolicyPeriodAndDuration>
        {
            // Short: 20 minutes, every minute, 20 retries
            { LifetimePolicy.Short, new RetryPolicyPeriodAndDuration(Period.FromMinutes(1), Period.FromMinutes(20)) },
            // Medium: 1 hour, every 5 minutes, 20 retries
            { LifetimePolicy.Medium, new RetryPolicyPeriodAndDuration(Period.FromMinutes(5), Period.FromHours(1)) },
            // Long: 1 day, every hour, 24 retries
            { LifetimePolicy.Long, new RetryPolicyPeriodAndDuration(Period.FromHours(1), Period.FromDays(1)) },
            // Eternal: infinite, every day
            { LifetimePolicy.Eternal, new RetryPolicyPeriodAndDuration(Period.FromDays(1), Period.FromYears(99)) },
        };

        public IRetryTimeSpecification CreateForPolicy(LifetimePolicy policy)
        {
            if (policy != LifetimePolicy.Once)
            {
                var periodAndDurationForPolicy = PolicyValues[policy];
                return new PeriodBasedRetryTimeSpecification(periodAndDurationForPolicy.MinimalRetryInterval, periodAndDurationForPolicy.AllowableDuration);
            }

            return new PeriodBasedRetryTimeSpecification(Period.Zero, Period.Zero);
        }
    }
}