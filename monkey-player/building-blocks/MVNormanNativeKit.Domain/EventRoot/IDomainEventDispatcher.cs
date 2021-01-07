using System;
using System.Threading.Tasks;

namespace MVNormanNativeKit.Domain.EventRoot
{
    public interface IDomainEventDispatcher : IDisposable
    {
        Task Dispatch(IEvent @event);
    }
}
