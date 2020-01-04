using NodaTime;

namespace Corazon.Saga.Specifications.ExecutionTime
{
    internal class SingleShotExecutionTimeSpecification : IExecutionTimeSpecification
    {
        public LocalDateTime? ComputeNextExecutionTime(LocalDateTime lastExecutionTime)
        {
            return null;
        }
    }
}