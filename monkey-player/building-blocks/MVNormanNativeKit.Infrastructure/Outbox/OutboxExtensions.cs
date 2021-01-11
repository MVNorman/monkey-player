using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVNormanNativeKit.Infrastructure.Outbox.Stores.Dapper;
using MVNormanNativeKit.Infrastructure.Outbox.Stores.EfCore;
using MVNormanNativeKit.Infrastructure.Outbox.Stores.MongoDb;

namespace MVNormanNativeKit.Infrastructure.Outbox
{
    public static class OutboxExtensions
    {
        public static IServiceCollection AddOutbox(
            this IServiceCollection services, 
            IConfiguration configuration, 
            Action<DbContextOptionsBuilder> dbContextOptions = null)
        {
            var options = new OutboxOptions();
            
            configuration.GetSection(nameof(OutboxOptions)).Bind(options);
            
            services.Configure<OutboxOptions>(configuration.GetSection(nameof(OutboxOptions)));

            switch (options.OutboxType.ToLowerInvariant())
            {
                case "efcore":
                case "ef":
                    services.AddEfCoreOutboxStore(dbContextOptions);
                    break;
                case "dapr":
                    services.AddDapperOutbox(configuration);
                    break;
                case "mongo":
                case "mongodb":
                    services.AddMongoDbOutbox(configuration);
                    break;
                default:
                    throw new Exception($"Outbox type '{options.OutboxType}' is not supported");
            }

            services.AddScoped<IOutboxListener, OutboxListener>();
            
            services.AddHostedService<OutboxProcessor>();

            return services;
        }
    }
}
