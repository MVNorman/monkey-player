using MediatR;
using MVNormanNativeKit.Domain.EventRoot;

namespace MVNormanNativeKit.Infrastructure.Bus.Notifications
{
    /// <summary>
    /// This class contains the domain event which published from domain entity.
    /// We use DomainEventBus to handle and publish it back to the specific project,
    /// then it will handle and use some of popular event brokers like Redis/Kafka to handle it
    /// </summary>
    public class NotificationEnvelope : INotification
    {
        public NotificationEnvelope(IEvent @event)
        {
            Event = @event;
        }

        public IEvent Event { get; }
    }
}
