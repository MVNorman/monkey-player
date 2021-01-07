using System;
using static MVNormanNativeKit.Tools.Helpers.DateTimeHelper;
using static MVNormanNativeKit.Tools.Helpers.IdHelper;

namespace MVNormanNativeKit.Domain.EventRoot
{
    public abstract class EventBase : IEvent
    {
        public Guid Id { get; protected set; } = NewId();
        public int EventVersion { get; protected set; } = 1; // TODO: Find beater way to reproduce event version
        public DateTime OccurredOn { get; protected set; } = NewDateTime();
    }
}
