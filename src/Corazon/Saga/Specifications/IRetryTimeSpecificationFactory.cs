namespace Corazon.Saga.Specifications
{
    public interface IRetryTimeSpecificationFactory
    {
        IRetryTimeSpecification CreateForPolicy(LifetimePolicy policy);
    }
}