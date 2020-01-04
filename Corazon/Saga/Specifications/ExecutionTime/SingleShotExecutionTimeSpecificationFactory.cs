namespace Corazon.Saga.Specifications.ExecutionTime
{
    public class SingleShotExecutionTimeSpecificationFactory : IExecutionTimeSpecificationFactory
    {
        public IExecutionTimeSpecification CreateForPolicy(PeriodicityPolicy policy)
        {
            return new SingleShotExecutionTimeSpecification();
        }
    }
}