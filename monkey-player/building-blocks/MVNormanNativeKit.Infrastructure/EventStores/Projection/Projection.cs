using System;
using System.Collections.Generic;
using System.Linq;
using MVNormanNativeKit.Infrastructure.Core.Events;

namespace MVNormanNativeKit.Infrastructure.EventStores.Projection
{
    public abstract class Projection : IProjection
    {
        private readonly Dictionary<Type, Action<IEvent>> _handlers = new Dictionary<Type, Action<IEvent>>();

        public Type[] Handles => _handlers.Keys.ToArray();

        protected virtual void Projects<TEvent>(Action<IEvent> action)
        {
            _handlers.Add(typeof(IEvent), @event => action(@event));
        }

        public virtual void Handle(IEvent @event)
        {
            _handlers[@event.GetType()](@event);
        }
    }
}
