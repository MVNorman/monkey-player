using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using MVNormanNativeKit.Domain.EventRoot;
using static MVNormanNativeKit.Tools.Helpers.DateTimeHelper;

namespace MVNormanNativeKit.Domain.EntityRoot
{
    public abstract class AggregateRootBase<TId> : EntityBase<TId>, IAggregateRoot<TId>
    {
        private readonly IDictionary<Type, Action<object>> _handlers = new ConcurrentDictionary<Type, Action<object>>();
        private readonly List<IEvent> _uncommittedEvents = new List<IEvent>();

        protected AggregateRootBase() : this(default)
        {
        }

        protected AggregateRootBase(TId id) : base(id)
        {
            CreatedAt = NewDateTime();
        }

        public int Version { get; protected set; }

        public IAggregateRoot<TId> AddEvent(IEvent uncommittedEvent)
        {
            _uncommittedEvents.Add(uncommittedEvent);
            ApplyEvent(uncommittedEvent);
            return this;
        }

        public IAggregateRoot<TId> ApplyEvent(IEvent payload)
        {
            if (!_handlers.ContainsKey(payload.GetType()))
                return this;
            _handlers[payload.GetType()]?.Invoke(payload);
            Version++;
            return this;
        }

        public void ClearUncommittedEvents()
        {
            _uncommittedEvents.Clear();
        }

        public List<IEvent> GetUncommittedEvents()
        {
            return _uncommittedEvents;
        }

        public IAggregateRoot<TId> RegisterHandler<T>(Action<T> handler)
        {
            _handlers.Add(typeof(T), e => handler((T)e));
            return this;
        }

        public IAggregateRoot<TId> RemoveEvent(IEvent @event)
        {
            if (_uncommittedEvents.Find(e => e == @event) != null)
                _uncommittedEvents.Remove(@event);
            return this;
        }
    }
}
