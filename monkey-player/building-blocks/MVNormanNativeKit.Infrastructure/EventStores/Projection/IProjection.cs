using System;
using MVNormanNativeKit.Infrastructure.Core.Events;

namespace MVNormanNativeKit.Infrastructure.EventStores.Projection
{
    public interface IProjection
    {
        Type[] Handles { get; }
        void Handle(IEvent @event);
    }
}
