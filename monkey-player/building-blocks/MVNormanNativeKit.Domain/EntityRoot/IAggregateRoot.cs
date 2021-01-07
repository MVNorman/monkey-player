using System;
using System.Collections.Generic;
using MVNormanNativeKit.Domain.EventRoot;

namespace MVNormanNativeKit.Domain.EntityRoot
{
    public interface IAggregateRoot<TId> : IEntity<TId>
    {
        IAggregateRoot<TId> ApplyEvent(IEvent payload);
        List<IEvent> GetUncommittedEvents();
        void ClearUncommittedEvents();
        IAggregateRoot<TId> RemoveEvent(IEvent @event);
        IAggregateRoot<TId> AddEvent(IEvent uncommittedEvent);
        IAggregateRoot<TId> RegisterHandler<T>(Action<T> handler);
    }
}
