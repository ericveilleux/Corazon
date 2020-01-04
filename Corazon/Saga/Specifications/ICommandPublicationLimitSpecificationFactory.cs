namespace Corazon.Saga.Specifications
{
    public interface ICommandPublicationLimitSpecificationFactory
    {
        ICommandPublicationLimitSpecification CreateForPolicy(ConcurrencyPolicy policy);
    }
}