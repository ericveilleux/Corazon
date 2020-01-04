using NodaTime;

namespace Corazon.Saga.Specifications.ExecutionTime
{
    internal class SpecificExecutionTimeSpecification : IExecutionTimeSpecification
    {
        private readonly LocalDateTime _executionTime;

        public SpecificExecutionTimeSpecification(LocalDateTime executionTime)
        {
            this._executionTime = executionTime;
        }

        public LocalDateTime? ComputeNextExecutionTime(LocalDateTime lastExecutionTime)
        {
            return this._executionTime;
        }
    }
}