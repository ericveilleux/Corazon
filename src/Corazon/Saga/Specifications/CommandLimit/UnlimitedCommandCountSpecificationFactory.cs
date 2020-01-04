namespace Corazon.Saga.Specifications.CommandLimit
{
    public class UnlimitedCommandCountSpecificationFactory : ICommandPublicationLimitSpecificationFactory
    {
        public ICommandPublicationLimitSpecification CreateForPolicy(ConcurrencyPolicy policy)
        {
            return new CommandCountPublicationLimitSpecification(int.MaxValue);
        }
    }
}