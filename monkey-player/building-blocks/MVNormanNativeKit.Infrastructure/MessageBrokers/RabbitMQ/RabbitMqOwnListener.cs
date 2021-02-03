using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MVNormanNativeKit.Infrastructure.Core.Events;
using RawRabbit;

namespace MVNormanNativeKit.Infrastructure.MessageBrokers.RabbitMQ
{
    public sealed class RabbitMqOwnListener : IEventListener
    {
        private readonly IBusClient _busClient;
        private readonly IServiceScopeFactory _serviceFactory;
        private readonly RabbitMqOptions _options;

        public RabbitMqOwnListener(
            IBusClient busClient,
            IOptions<RabbitMqOptions> options,
            IServiceScopeFactory serviceFactory)
        {
            _busClient = busClient;
            _serviceFactory = serviceFactory;
            _options = options.Value;
        }

        public void Subscribe<TEvent>() where TEvent : IEvent
        {
            Subscribe(typeof(TEvent));
        }

        public void Subscribe(Type type)
        {
            _busClient.SubscribeAsync(
                (Func<IEvent, Task>)(async (msg) =>
                {
                    using (var scope = _serviceFactory.CreateScope())
                    {
                        var eventBus = scope.ServiceProvider.GetService<IEventBus>();
                        await eventBus.PublishLocalAsync(msg);
                    }
                }),
                cfg => cfg.UseSubscribeConfiguration(
                    c => c
                    .OnDeclaredExchange(GetExchangeDeclaration(type))
                    // .FromDeclaredQueue(q =>
                    //     q.WithName(
                    //         (_options.Queue.Name ?? 
                    //          AppDomain.CurrentDomain.FriendlyName).Trim().Trim('_') + "_" + type.Name))
                    )
            );
        }


        // public async Task Publish<TEvent>(TEvent @event, string queue) where TEvent : IEvent
        // {
        //     _busClient.su
        // }
        public async Task Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            if (@event == null)
            {
                throw new ArgumentNullException(nameof(@event), "Event can not be null.");
            }

            await _busClient.PublishAsync(
                @event,
                cfg => cfg.UsePublishConfiguration(
                    c => c
                    .OnDeclaredExchange(GetExchangeDeclaration<TEvent>())
                )
            );
        }

        public async Task Publish(string message, string type)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message), "Event message can not be null.");
            }

            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentNullException(nameof(type), "Event type can not be null.");
            }

            await _busClient.PublishAsync(
                message,
                cfg => cfg.UsePublishConfiguration(
                    c => c
                    .OnDeclaredExchange(GetExchangeDeclaration(type))
                )
            );
        }

        private Action<RawRabbit.Configuration.Exchange.IExchangeDeclarationBuilder> GetExchangeDeclaration<T>()
        {
            return GetExchangeDeclaration(typeof(T));
        }

        private Action<RawRabbit.Configuration.Exchange.IExchangeDeclarationBuilder> GetExchangeDeclaration(Type type)
        {
            var name = MessageBrokersHelper.GetTypeName(type);

            return GetExchangeDeclaration(name);
        }

        private Action<RawRabbit.Configuration.Exchange.IExchangeDeclarationBuilder> GetExchangeDeclaration(string name)
        {
            return e => e
                .WithName(_options.Exchange.Name)
                .WithArgument("key", name);
        }
    }
}
