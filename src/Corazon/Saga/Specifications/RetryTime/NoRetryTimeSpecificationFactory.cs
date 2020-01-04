using NodaTime;

namespace Corazon.Saga.Specifications.RetryTime
{
    public class NoRetryTimeSpecificationFactory : IRetryTimeSpecificationFactory
    {
        public IRetryTimeSpecification CreateForPolicy(LifetimePolicy policy)
        {
            return new PeriodBasedRetryTimeSpecification(Period.Zero, Period.Zero);
        }
    }
}