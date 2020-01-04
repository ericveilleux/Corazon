using Corazon.Saga.Specifications;
using Corazon.Specification;
using Corazon.Time;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Corazon.Saga
{
    //public class SagaSpecificationFactory
    //{
    //    IStartTimeSpecification GetStartTimeSpecification(LifetimePolicy policy);

    //}
    /// <summary>
    /// A saga represents a state machine wrapped in a long-running process.
    /// Derived classes must implement state-changing methods that set the CurrentState of the saga via Commands.
    /// It should be accessed by using the id of the aggregate that triggers it, or its own id when the operation is not linked to a specific instance.
    /// </summary>
    public abstract class Saga : Entity<SagaId>
    {
        // This is the list of queued commands, once they are published and until they are fetched
        private List<DomainCommand> _queuedCommands;

        private readonly Lazy<IDelayedStartSpecification> _delayedStartSpecification;

        private readonly Lazy<ICommandPublicationLimitSpecification> _commandPublicationLimitSpecification;

        private readonly Lazy<IRetryTimeSpecification> _retryTimeSpecification;

        private readonly Lazy<IExecutionTimeSpecification> _executionTimeSpecification;

        protected abstract LifetimePolicy LifetimePolicy { get; }

        protected abstract DeduplicationPolicy DeduplicationPolicy { get; }

        protected abstract ConcurrencyPolicy ConcurrencyPolicy { get; }

        protected abstract PeriodicityPolicy PeriodicityPolicy { get; }

        //TODO: WOuld be nice to have less public fields...

        public SagaContextId ContextId { get; internal set; }

        // This is the list of the commands not yet published in the current state
        public List<DomainCommand> PendingCommands { get; private set; }

        // This is the list of the commands already published in the current state
        public List<DomainCommand> PublishedCommands { get; private set; }

        public LocalDateTime StartedOn { get; internal set; }

        public LocalDateTime? NextRetryTime { get; internal set; }

        public LocalDateTime NextProcessingTimeDueOn { get; internal set; }

        public bool IsExpired { get; internal set; }

        public bool IsCompleted { get; internal set; }

        public Saga()
        {
        }

        public Saga(
            IDelayedStartSpecificationFactory delayedStartSpecFactory, 
            ICommandPublicationLimitSpecificationFactory commandLimitSpecFactory,
            IRetryTimeSpecificationFactory retryTimeSpecFactory,
            IExecutionTimeSpecificationFactory executionTimeSpecFactory)
        {
            this._delayedStartSpecification = new Lazy<IDelayedStartSpecification>(() => delayedStartSpecFactory.CreateForPolicy(this.DeduplicationPolicy));
            this._commandPublicationLimitSpecification = new Lazy<ICommandPublicationLimitSpecification>(() => commandLimitSpecFactory.CreateForPolicy(this.ConcurrencyPolicy));
            this._retryTimeSpecification = new Lazy<IRetryTimeSpecification>(() => retryTimeSpecFactory.CreateForPolicy(this.LifetimePolicy));
            this._executionTimeSpecification = new Lazy<IExecutionTimeSpecification>(() => executionTimeSpecFactory.CreateForPolicy(this.PeriodicityPolicy));
        }

        public Saga(SagaId identity)
            : base(identity)
        {
        }

        /// <summary>
        /// This method is called at the appropriate moment by the framework so the saga can publish its state, handle retries or timeouts.
        /// </summary>
        /// <param name="referenceTime"></param>
        public void Execute(LocalDateTime referenceTime)
        {
            if (referenceTime < this.NextProcessingTimeDueOn ||
                this.IsExpired || 
                this.IsCompleted)
            {
                return;
            }

            var isRetry = referenceTime >= this.NextRetryTime;
            if (isRetry)
            {
                this.ResetCurrentStateCommands();
            }

            this.PublishCurrentStateCommands();

            // Compute the retry time on the first processing or after each retry
            if (!this.NextRetryTime.HasValue || isRetry)
            {
                this.NextRetryTime = this.GetNextRetryTime(referenceTime);
                if (!this.NextRetryTime.HasValue)
                {
                    this.IsExpired = true;
                    return;
                }

                this.NextProcessingTimeDueOn = this.NextRetryTime.Value;
            }
        }

        /// <summary>
        /// Return then clear the queued commands.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DomainCommand> FetchQueuedCommands()
        {
            if (this._queuedCommands != null)
            {
                var commands = this._queuedCommands.ToList();
                this._queuedCommands.Clear();
                return commands;
            }

            return Enumerable.Empty<DomainCommand>();
        }

        /// <summary>
        /// Derived classes call this method to start the saga with the initial state
        /// </summary>
        /// <param name="step"></param>
        protected void StartWithInitialState(State state)
        {
            if (this.PendingCommands != null)
            {
                throw new Exception("Can only enter initial state once");
            }

            this.PendingCommands = state.Commands.ToList();
            this.PublishedCommands = new List<DomainCommand>();

            var referenceTime = InstantProvider.Current.Now.InUtc().LocalDateTime;
            var nextExecutionTime = this._executionTimeSpecification.Value.ComputeNextExecutionTime(referenceTime);
            if (nextExecutionTime.HasValue)
            {
                this.StartedOn = this.NextProcessingTimeDueOn = nextExecutionTime.Value;
            }
            else
            {
                this.StartedOn = this.NextProcessingTimeDueOn = this._delayedStartSpecification.Value.ComputeStartTime(referenceTime);
            }
        }

        /// <summary>
        /// Derived classes call this method to bring the saga to the next state
        /// </summary>
        protected void NextState(State state)
        {
            if (this.PendingCommands == null)
            {
                throw new Exception("Can only perform next steps once initial step was done");
            }

            this.PendingCommands = state.Commands.ToList();

            // Update the start time so the saga knows there is progression, and the next execution time so the processing of the new state is done
            var referenceTime = InstantProvider.Current.Now.InUtc().LocalDateTime;
            this.StartedOn = this.NextProcessingTimeDueOn = referenceTime;
        }

        /// <summary>
        /// Derived classes call this method to indicate successful completion of the specified command, thus removing it from the current state
        /// </summary>
        /// <param name="state"></param>
        protected void AcknowledgeCommandCompletion(Func<DomainCommand, bool> commandPredicate)
        {
            if (this.PublishedCommands == null)
            {
                throw new Exception("Can only perform ackowledgement when there is a current step");
            }

            var ackedCommands = this.PublishedCommands.Where(commandPredicate).ToArray();
            if (ackedCommands.Any())
            {
                foreach (var command in ackedCommands)
                {
                    this.PublishedCommands.Remove(command);
                }

                // Mark the processing of the state change in case some commands were pending
                this.RequiresProcessingNow();
            }
        }

        /// <summary>
        /// Derived classes call this method to indicate that the saga has completed
        /// </summary>
        protected void MarkAsComplete()
        {
            if (this.PendingCommands == null)
            {
                throw new Exception("Can only complete once initial step was done");
            }

            // In the case of a periodic saga, we need to replay the same state over and over.  We should update the startedon each time so the saga does not time out.
            var referenceTime = InstantProvider.Current.Now.InUtc().LocalDateTime;
            var nextExecutionTime = this._executionTimeSpecification.Value.ComputeNextExecutionTime(referenceTime);
            if (nextExecutionTime.HasValue)
            {
                this.StartedOn = this.NextProcessingTimeDueOn = nextExecutionTime.Value;
                this.NextRetryTime = null;
                this.ResetCurrentStateCommands();
                return;
            }
            
            this.IsCompleted = true;
        }

        private void RequiresProcessingNow()
        {
            this.NextProcessingTimeDueOn = InstantProvider.Current.Now.InUtc().LocalDateTime;
        }

        private LocalDateTime? GetNextRetryTime(LocalDateTime referenceTime)
        {
            return this._retryTimeSpecification.Value.ComputeNextRetryTime(this.StartedOn, referenceTime);
        }

        private void ResetCurrentStateCommands()
        {
            this.PendingCommands.AddRange(this.PublishedCommands);
            this.PublishedCommands.Clear();
        }

        private void PublishCurrentStateCommands()
        {
            if (this._queuedCommands == null)
            {
                this._queuedCommands = new List<DomainCommand>();
            }
            else
            {
                this._queuedCommands.Clear();
            }

            var maxCommandCount = this._commandPublicationLimitSpecification.Value.GetMaxCommandCount();

            var publishedCommands = this.PublishedCommands;
            var availableCount = maxCommandCount - publishedCommands.Count();
            if (availableCount > 0)
            {
                foreach (var command in this.PendingCommands.Take(availableCount).ToList())
                {
                    this._queuedCommands.Add(command);
                    this.PublishedCommands.Add(command);
                    this.PendingCommands.Remove(command);
                }
            }
        }
    }

    // Type of saga that runs a series of steps on an aggregate, where each step is represented by a single command.  
    // There is no persistence of current state, it is assumed that the caller knows about the current state when it calls the state machine.
    // It facilitates the management of the commands for each state.
    // The states are managed by a very simple integer incrementing.  Derived classes can implement the state machine by assigning the value to enum for example.
    //public abstract class SequentialStateSaga<TAggregate, TAggregateIdentity> : Saga
    //    where TAggregate : AggregateRoot<TAggregateIdentity>
    //    where TAggregateIdentity : Identity, new()
    public abstract class SequentialStateSaga : Saga
    {
        protected void RunStateMachine(uint currentState = 0)
        {
            var state = new State(this.BuildCommandForState(currentState));

            if (currentState == 0)
            {
                this.StartWithInitialState(state);
            }
            else
            {
                this.NextState(state);
            }
        }

        protected abstract DomainCommand BuildCommandForState(uint state);
    }

    // Type of saga that has only a single state, but with multiple commands that each represent the same command to run on different aggregate ids.
    public abstract class AggregateBatchOperationSaga<TAggregateIdentity> : Saga
        where TAggregateIdentity : Identity, new()
    {
        protected void StartBatchOperation(IMultiQuerySpecification<TAggregateIdentity> candidates)
        {
            var state = new State(candidates.SelectSatisfying().Select(candidateId => this.BuildCommand(candidateId)).ToArray());

            this.StartWithInitialState(state);
        }
        
        protected abstract DomainCommand BuildCommand(TAggregateIdentity id);
    }

    // This version is a periodic saga that can be used to run periodic long-running operations.
    // The thing with this one is that I was thinking that I could let it run, then complete it once it finished its work.
    // This would delete the saga.  However, doing so removes the knowledge about the fact that the saga was run on which date.
    // Thus it prevents me from being absolutely sure that it does not need to run again.  The only way to know this for sure
    // is to keep it around.  This way, when the scheduler comes and decides which sagas need to run, it knows if it ran or not.
    // Actually, even better once it is started, if I would store the next occurrence in the saga instead of the retry time, it would
    // automatically be grabbed during Processing.  Completing the saga in that case would simply reload the initial state commands as 
    // not published, ready for the next run.
    //public abstract class PeriodicSaga : Saga
    //{
    //    public abstract PeriodicityPolicy PeriodicityPolicy { get; }

    //    /// <summary>
    //    /// This method is called to start the saga, it needs to retrieve the initial state and start
    //    /// </summary>
    //    public void Start()
    //    {
    //        var initialState = this.GetInitialState();

    //        this.StartWithInitialState(initialState);
    //    }

    //    /// <summary>
    //    /// Derived classes implement this method to return the saga's initial state
    //    /// </summary>
    //    /// <returns></returns>
    //    protected abstract State GetInitialState();
    //}
}