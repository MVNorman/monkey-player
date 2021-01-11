using System;
using MVNormanNativeKit.Infrastructure.EventStores.Aggregate;

namespace MVNormanNativeKit.Infrastructure.EventStores.Snapshot
{
    public interface ISnapshot
    {
        Type Handles { get; }
        void Handle(IAggregate aggregate);
    }
}
