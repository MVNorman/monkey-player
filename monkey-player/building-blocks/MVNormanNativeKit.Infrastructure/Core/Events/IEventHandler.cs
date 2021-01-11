using MediatR;

namespace MVNormanNativeKit.Infrastructure.Core.Events
{
    public interface IEventHandler<TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
    { }
}
