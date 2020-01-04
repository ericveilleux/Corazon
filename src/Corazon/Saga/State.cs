using System.Collections.Generic;
using System.Linq;

namespace Corazon.Saga
{
    /// <summary>
    /// Each state of a saga represent a series of commands that needs to be published in that state.
    /// </summary>
    public class State
    {
        public IEnumerable<DomainCommand> Commands { get; private set; }

        public State(params DomainCommand[] commands)
        {
            this.Commands = commands.ToList();
        }
    }
}