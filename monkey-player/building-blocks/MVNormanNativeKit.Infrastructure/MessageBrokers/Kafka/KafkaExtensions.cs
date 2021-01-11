using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVNormanNativeKit.Infrastructure.Core.Events;

namespace MVNormanNativeKit.Infrastructure.MessageBrokers.Kafka
{
    public static class KafkaExtensions
    {
        public static IServiceCollection AddKafka(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new KafkaOptions();
            configuration.GetSection(nameof(MessageBrokersOptions)).Bind(options);
            services.Configure<KafkaOptions>(configuration.GetSection(nameof(MessageBrokersOptions)));

            services.AddSingleton<IEventListener, KafkaListener>();

            return services;
        }

        public static IApplicationBuilder UseKafkaSubscribe<T>(this IApplicationBuilder app) where T : IEvent
        {
            app.ApplicationServices.GetRequiredService<IEventListener>().Subscribe<T>();

            return app;
        }
    }
}
