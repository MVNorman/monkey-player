using System.Threading.Tasks;
using MVNormanNativeKit.Infrastructure.Core.Events;
using MVNormanNativeKit.Infrastructure.MessageBrokers;
using MVNormanNativeKit.Infrastructure.Outbox.Stores;
using Newtonsoft.Json;

namespace MVNormanNativeKit.Infrastructure.Outbox
{
    public sealed class OutboxListener : IOutboxListener
    {
        private readonly IOutboxStore _store;

        public OutboxListener(IOutboxStore store)
        {
            _store = store;
        }

        public async Task Commit(OutboxMessage message)
        {
            await _store.Add(message);
        }

        public async Task Commit<TEvent>(TEvent @event) where TEvent : IEvent
        {
            var outboxMessage = new OutboxMessage
            { 
                Type = MessageBrokersHelper.GetTypeName<TEvent>(),
                Data = @event == null ? "{}" : JsonConvert.SerializeObject(@event, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                })
            };

            await Commit(outboxMessage);
        }
    }
}
