using System.Threading.Tasks;
using Google.Protobuf;
using MVNormanNativeKit.Domain.EventRoot;

namespace MVNormanNativeKit.Infrastructure.Bus
{
    public interface IMessagePublisher
    {
        Task PublishAsync<TMessage>(TMessage msg, params string[] channels) where TMessage : IIntegrationEvent, IMessage<TMessage>;
    }
}
