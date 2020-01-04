using System;

namespace Corazon
{
    /// <summary>
    /// Implements the base contract for the Domain Event pattern.
    /// A domain event is a occurrence of something that happened in the domain.  Events are like value objects, immutable and side-effects free
    /// </summary>
    public class DomainEvent : ValueObject<DomainEvent>
    {
        public string AggregateTypeName { get; set; }

        public Guid AggregateId { get; set; }

        public int Version { get; set; }

        public DateTime OccurredOn { get; set; }
    }
}
