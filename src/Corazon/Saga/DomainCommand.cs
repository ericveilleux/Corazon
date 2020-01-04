using System;

namespace Corazon.Saga
{
    public class DomainCommand : ValueObject<DomainCommand>
    {
        public string AggregateTypeName { get; set; }

        public Guid AggregateId { get; set; }

        public Guid CorrelationId { get; set; }

        public int Version { get; set; }

        public DateTime SentOn { get; set; }
    }
}