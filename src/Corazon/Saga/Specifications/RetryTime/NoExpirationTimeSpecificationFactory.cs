using NodaTime;

namespace Corazon.Saga.Specifications.RetryTime
{
    public class NoExpirationTimeSpecificationFactory : IRetryTimeSpecificationFactory
    {
        public IRetryTimeSpecification CreateForPolicy(LifetimePolicy policy)
        {
            return new PeriodBasedRetryTimeSpecification(Period.FromYears(98), Period.FromYears(99));
        }
    }
}