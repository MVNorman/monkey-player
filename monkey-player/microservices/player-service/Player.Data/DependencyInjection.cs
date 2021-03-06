using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVNormanNativeKit.Infrastructure.Data.Dapper;
using MVNormanNativeKit.Infrastructure.Data.Dapper.Core;
using MVNormanNativeKit.Infrastructure.Data.EFCore;
using MVNormanNativeKit.Infrastructure.Data.EFCore.Core;
using Player.Data.Song;
using Player.Domain.Song.Contracts;

namespace Player.Data
{
    public static class DependencyInjection
    {
        public static void AddDataLayer(this IServiceCollection services, IConfiguration configuration)
        {
            var migrationsAssembly = typeof(MonkeyPlayerDataContext).GetTypeInfo().Assembly.GetName().Name;
            var dbOptions = new DbOptions();

            configuration.Bind("ConnectionStrings", dbOptions);
            
            services.AddDbContext<MonkeyPlayerDataContext>(
                options => options.UseSqlServer(configuration.GetConnectionString(ConnectionStringKeys.App),
                    db => db.MigrationsAssembly(migrationsAssembly)));
            
            services.AddScoped<IEfUnitOfWork<MonkeyPlayerDataContext>, EfUnitOfWork<MonkeyPlayerDataContext>>();

            //services.AddDbContext<MessagingDataContext>(options => options.UseSqlServer(dbOptions.MainDb));
            // services.AddScoped<IEfUnitOfWork<MessagingDataContext>, EfUnitOfWork<MessagingDataContext>>();

            services.Configure<DapperDbOptions>(configuration.GetSection("ConnectionStrings"));
            services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();
            services.AddScoped<IDapperUnitOfWork, DapperUnitOfWork>();

            services.AddScoped<ISongRepository, SongRepository>();
        }
    }
}