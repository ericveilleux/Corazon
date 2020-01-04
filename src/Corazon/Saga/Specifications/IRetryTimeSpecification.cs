using NodaTime;

namespace Corazon.Saga.Specifications
{
    // This interface represents a specification that encapsulates the computation of retry times based on a policy and execution times.
    public interface IRetryTimeSpecification
    {
        LocalDateTime? ComputeNextRetryTime(LocalDateTime firstExecutionTime, LocalDateTime lastExecutionTime);
    }
}