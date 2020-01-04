using NodaTime;

namespace Corazon.Saga
{
    public interface ISagaProcessor
    {
        void ExecuteNextDue(LocalDateTime referenceTime);
    }
}