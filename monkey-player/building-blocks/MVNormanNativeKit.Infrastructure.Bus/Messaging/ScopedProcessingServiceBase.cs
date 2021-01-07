using Microsoft.Extensions.Logging;

namespace MVNormanNativeKit.Infrastructure.Bus.Messaging
{
    public abstract class ScopedProcessingServiceBase
    {
        protected ScopedProcessingServiceBase(IMessageBus messageBus, ILogger<ScopedProcessingServiceBase> logger)
        {
            MessageBus = messageBus;
            Logger = logger;
        }

        protected IMessageBus MessageBus { get; }
        protected ILogger<ScopedProcessingServiceBase> Logger { get; }
    }
}
