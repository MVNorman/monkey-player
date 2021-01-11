using System;
using System.Threading.Tasks;
using MediatR;
using MVNormanNativeKit.Infrastructure.EventStores;
using MVNormanNativeKit.Infrastructure.MessageBrokers;
using MVNormanNativeKit.Infrastructure.Outbox;

namespace MVNormanNativeKit.Infrastructure.Core.Events
{
    public sealed class EventBus : IEventBus
    {
        private readonly IMediator _mediator;
        private readonly IOutboxListener _outboxListener;
        private readonly IEventListener _eventListener;

        public EventBus(IMediator mediator, IOutboxListener outboxListener, IEventListener eventListener)
        {
            _mediator = mediator ?? throw new Exception($"Missing dependency '{nameof(IMediator)}'");
            _outboxListener = outboxListener;
            _eventListener = eventListener;
        }

        public async Task PublishLocal(params IEvent[] events)
        {
            foreach (var @event in events)
            {
                await _mediator.Publish(@event);
            }
        }

        public async Task Commit(params IEvent[] events)
        {
            foreach (var @event in events)
            {
                await SendToMessageBroker(@event);
            }
        }

        public async Task Commit(StreamState stream)
        {
            if (_outboxListener != null)
            {
                var message = Mapping.Map<StreamState, OutboxMessage>(stream);
                await _outboxListener.Commit(message);
            }
            else if (_eventListener != null)
            {
                await _eventListener.Publish(stream.Data, stream.Type);
            }
            else
            {
                throw new ArgumentNullException("No event listener found");
            }
        }

        private async Task SendToMessageBroker(IEvent @event)
        {
            if (_outboxListener != null)
            {
                await _outboxListener.Commit(@event);
            }
            else if (_eventListener != null)
            {
                await _eventListener.Publish(@event);
            }
            else
            {
                throw new ArgumentNullException("No event listener found");
            }
        }
    }
}
