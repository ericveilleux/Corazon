using NodaTime;

namespace Corazon.Saga.Specifications
{
    public interface IExecutionTimeSpecification
    {
        LocalDateTime? ComputeNextExecutionTime(LocalDateTime lastExecutionTime);
    }
}