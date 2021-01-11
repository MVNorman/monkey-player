using System.Threading.Tasks;
using MVNormanNativeKit.Infrastructure.EventStores;

namespace MVNormanNativeKit.Infrastructure.Core.Events
{
    public interface IEventBus
    {
        Task PublishLocal(params IEvent[] events);
        Task Commit(params IEvent[] events);
        Task Commit(StreamState stream);
    }
}
