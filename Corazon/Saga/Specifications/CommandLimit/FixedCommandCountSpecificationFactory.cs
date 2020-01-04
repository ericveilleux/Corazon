namespace Corazon.Saga.Specifications.CommandLimit
{
    public class FixedCommandCountSpecificationFactory : ICommandPublicationLimitSpecificationFactory
    {
        private int _commandLimit;

        public FixedCommandCountSpecificationFactory(int commandLimit)
        {
            this._commandLimit = commandLimit;
        }

        public ICommandPublicationLimitSpecification CreateForPolicy(ConcurrencyPolicy policy)
        {
            return new CommandCountPublicationLimitSpecification(this._commandLimit);
        }
    }
}