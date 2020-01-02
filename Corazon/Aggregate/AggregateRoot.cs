using Corazon.Ports;
using System.Collections.Generic;

namespace Corazon
{
    /// <summary>
    /// Implements the base contract for the Aggregate pattern.
    /// "Aggregate is an abstraction for encapsulating references within a model.  We need a model that leaves high-contention points looser and strict invariants tighter." - Evans
    /// Aggregate is also the source of events from the domain, so this implementation accumulates them in a local store.
    /// At the end of a transaction/uow, these accumulated events can dispatched outside of the domain.
    /// Implementation is based on https://lostechies.com/jimmybogard/2014/05/13/a-better-domain-events-pattern/
    /// </summary>
    /// <typeparam name="TIdentity"></typeparam>
    public abstract class AggregateRoot<TIdentity> : Entity<TIdentity>
        where TIdentity : Identity, new()
    {
        private readonly List<DomainEvent> _events = new List<DomainEvent>();

        protected AggregateRoot()
        {
        }

        protected AggregateRoot(TIdentity identity)
            : base(identity)
        {
        }

        protected void PublishEvent(DomainEvent domainEvent)
        {
            this._events.Add(domainEvent);
        }

        public void DispatchAndClearAccumulatedEvents(IDomainEventDispatcher eventDispatcher)
        {
            foreach (var domainEvent in this._events)
            {
                eventDispatcher.Dispatch(domainEvent);
            }
            this._events.Clear();
        }
    }
}