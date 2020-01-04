using NodaTime;

namespace Corazon.Saga.Specifications.ExecutionTime
{
    public class SpecificExecutionTimeSpecificationFactory : IExecutionTimeSpecificationFactory
    {
        private readonly LocalDateTime _executionTime;

        public SpecificExecutionTimeSpecificationFactory(LocalDateTime executionTime)
        {
            this._executionTime = executionTime;
        }

        public IExecutionTimeSpecification CreateForPolicy(PeriodicityPolicy policy)
        {
            return new SpecificExecutionTimeSpecification(this._executionTime);
        }
    }
}