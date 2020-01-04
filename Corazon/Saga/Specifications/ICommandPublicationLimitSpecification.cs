namespace Corazon.Saga.Specifications
{
    // This class encapsulate the maximal of published commands depending on the policy specified
    public interface ICommandPublicationLimitSpecification
    {
        int GetMaxCommandCount();
    }
}