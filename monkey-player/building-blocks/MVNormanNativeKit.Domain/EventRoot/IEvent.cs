using System;

namespace MVNormanNativeKit.Domain.EventRoot
{
    /// <summary>
    ///  Super type for all Event types
    /// </summary>
    public interface IEvent
    {
        Guid Id { get; }
        int EventVersion { get; }
        DateTime OccurredOn { get; }
    }
}
