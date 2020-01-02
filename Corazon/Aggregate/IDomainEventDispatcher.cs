﻿namespace Corazon.Ports
{
    /// <summary>
    /// This interface is used to dispatch domain events generated by the domain to interested parties.
    /// </summary>
    public interface IDomainEventDispatcher
    {
        void Dispatch(DomainEvent domainEvent);
    }
}