namespace Corazon.Saga.Specifications
{
    public interface IExecutionTimeSpecificationFactory
    {
        IExecutionTimeSpecification CreateForPolicy(PeriodicityPolicy policy);
    }
}