using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVNormanNativeKit.Infrastructure.EventStores.Aggregate;
using MVNormanNativeKit.Infrastructure.EventStores.Repository;
using MVNormanNativeKit.Infrastructure.EventStores.Stores.EfCore;
using MVNormanNativeKit.Infrastructure.EventStores.Stores.MongoDb;

namespace MVNormanNativeKit.Infrastructure.EventStores
{
    public static class EventStoresExtensions
    {
        public static IServiceCollection AddEventStore<TAggregate>(
            this IServiceCollection services,
            IConfiguration configuration, 
            Action<DbContextOptionsBuilder> dbContextOptions = null) where TAggregate : IAggregate
        {
            var options = new EventStoresOptions();
            
            configuration.GetSection(nameof(EventStoresOptions)).Bind(options);
            
            services.Configure<EventStoresOptions>(configuration.GetSection(nameof(EventStoresOptions)));

            switch (options.EventStoreType.ToLowerInvariant())
            {
                case "efcore":
                case "ef":
                    services.AddEfCoreEventStore(dbContextOptions);
                    break;
                case "mongo":
                case "mongodb":
                    services.AddMongoDbEventStore(configuration);
                    break;
                default:
                    throw new Exception($"Event store type '{options.EventStoreType}' is not supported");
            }

            services.AddScoped<IRepository<TAggregate>, Repository<TAggregate>>();
            services.AddScoped<IEventStore, EventStore>();

            return services;
        }
    }
}
