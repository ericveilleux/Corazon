namespace Corazon.Saga.Specifications
{
    public interface IDelayedStartSpecificationFactory
    {
        IDelayedStartSpecification CreateForPolicy(DeduplicationPolicy policy);
    }
}