using System;
using System.Collections.Generic;
using MVNormanNativeKit.Infrastructure.Core.Events;

namespace MVNormanNativeKit.Infrastructure.EventStores.Aggregate
{
    public interface IAggregate
    {
        Guid Id { get; }
        int Version { get; }
        DateTime CreatedUtc { get; }

        IEnumerable<IEvent> DequeueUncommittedEvents();

    }
}
