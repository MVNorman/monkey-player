using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MVNormanNativeKit.Infrastructure.Outbox.Stores.Dapper
{
    public static class DapperOutboxExtensions
    {
        public static IServiceCollection AddDapperOutbox(this IServiceCollection services, IConfiguration configuration)
        {
            throw new NotImplementedException("Dapr is not ready yet");

            var options = new DapperOutboxOptions();
            configuration.GetSection(nameof(OutboxOptions)).Bind(options);
            services.Configure<DapperOutboxOptions>(configuration.GetSection(nameof(OutboxOptions)));

            return services;
        }
    }
}
