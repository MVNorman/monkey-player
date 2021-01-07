using System.Threading;
using System.Threading.Tasks;

namespace MVNormanNativeKit.Infrastructure.Bus.Messaging
{
    public interface IScopedProcessingService
    {
        Task DoWork(CancellationToken stoppingToken);
    }
}
