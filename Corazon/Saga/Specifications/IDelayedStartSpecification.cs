using NodaTime;

namespace Corazon.Saga.Specifications
{
    // This class encapsulate what it means for StartDelayInterval depending on the policy specified
    public interface IDelayedStartSpecification
    {
        LocalDateTime ComputeStartTime(LocalDateTime referenceTime);
    }
}