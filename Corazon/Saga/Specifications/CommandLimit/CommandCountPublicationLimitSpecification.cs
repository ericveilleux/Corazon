namespace Corazon.Saga.Specifications.CommandLimit
{
    internal class CommandCountPublicationLimitSpecification : ICommandPublicationLimitSpecification
    {
        private int _commandLimit;

        public CommandCountPublicationLimitSpecification(int commandLimit)
        {
            this._commandLimit = commandLimit;
        }

        public int GetMaxCommandCount()
        {
            return _commandLimit;
        }
    }
}