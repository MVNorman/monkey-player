using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVNormanNativeKit.Infrastructure.Core.Events;
using MVNormanNativeKit.Infrastructure.MessageBrokers.Kafka;
using MVNormanNativeKit.Infrastructure.MessageBrokers.RabbitMQ;

namespace MVNormanNativeKit.Infrastructure.MessageBrokers
{
    public static class MessageBrokersExtensions
    {
        public static IServiceCollection AddMessageBroker(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new MessageBrokersOptions();
            
            configuration.GetSection(nameof(MessageBrokersOptions)).Bind(options);
            
            services.Configure<MessageBrokersOptions>(configuration.GetSection(nameof(MessageBrokersOptions)));

            return options.MessageBrokerType.ToLowerInvariant() switch
            {
                // case "dapr":
                //     return services.AddDapr(configuration);
                "rabbitmq" => services.AddRabbitMq(configuration),
                "kafka" => services.AddKafka(configuration),
                _ => throw new Exception($"Message broker type '{options.MessageBrokerType}' is not supported")
            };
        }

        public static IApplicationBuilder UseSubscribeEvent<T>(this IApplicationBuilder app) where T : IEvent
        {
            app.ApplicationServices.GetRequiredService<IEventListener>().Subscribe<T>();

            return app;
        }

        public static IApplicationBuilder UseSubscribeEvent(this IApplicationBuilder app, Type type)
        {
            app.ApplicationServices.GetRequiredService<IEventListener>().Subscribe(type);

            return app;
        }
    }
}
