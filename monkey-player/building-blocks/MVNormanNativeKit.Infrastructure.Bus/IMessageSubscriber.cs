using System.Threading.Tasks;
using Google.Protobuf;
using MVNormanNativeKit.Domain.EventRoot;

namespace MVNormanNativeKit.Infrastructure.Bus
{
    public interface IMessageSubscriber
    {
        Task SubscribeAsync<TMessage>(params string[] channels) where TMessage : IIntegrationEvent, IMessage<TMessage>, new();
    }
}
