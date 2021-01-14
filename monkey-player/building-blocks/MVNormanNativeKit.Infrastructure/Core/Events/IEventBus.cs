using System.Threading.Tasks;
using MVNormanNativeKit.Infrastructure.EventStores;

namespace MVNormanNativeKit.Infrastructure.Core.Events
{
    public interface IEventBus
    {
        Task PublishLocalAsync(params IEvent[] events);
        Task CommitAsync(params IEvent[] events);
        Task CommitAsync(StreamState stream);
    }
}
