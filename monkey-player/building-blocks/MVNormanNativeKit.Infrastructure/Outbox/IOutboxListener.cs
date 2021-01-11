using System.Threading.Tasks;
using MVNormanNativeKit.Infrastructure.Core.Events;

namespace MVNormanNativeKit.Infrastructure.Outbox
{
    public interface IOutboxListener
    {
        Task Commit(OutboxMessage message);
        Task Commit<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}
