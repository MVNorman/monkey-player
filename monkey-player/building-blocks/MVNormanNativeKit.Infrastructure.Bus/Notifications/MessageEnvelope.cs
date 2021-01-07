using MediatR;
using MVNormanNativeKit.Domain.EventRoot;

namespace MVNormanNativeKit.Infrastructure.Bus.Notifications
{
    /// <summary>
    /// This class contains the Protobuf message with the idea that we will use Protobuf
    /// for inter-communication bus via event broker like Redis/Kafka
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MessageEnvelope<TEvent> : INotification
        where TEvent : IEvent
    {
        public MessageEnvelope(TEvent message)
        {
            Message = message;
        }

        public TEvent Message { get; }
    }
}
