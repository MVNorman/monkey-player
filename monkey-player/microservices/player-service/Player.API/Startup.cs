using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MP.Shared.Events;
using MVNormanNativeKit.Infrastructure.Consul;
using MVNormanNativeKit.Infrastructure.Core;
using MVNormanNativeKit.Infrastructure.Logging;
using MVNormanNativeKit.Infrastructure.MessageBrokers;
using MVNormanNativeKit.Infrastructure.Outbox;
using MVNormanNativeKit.Infrastructure.Swagger;
using Player.Application.Song.Commands;
using Player.Data;
using Serilog;

namespace Player.API
{
    public class Startup
    {
        private readonly IConfigurationRoot _configuration;
        private readonly IWebHostEnvironment _environment;
        
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", reloadOnChange: true, optional: true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();

            Log.Logger = LoggingExtensions.AddLogging(_configuration);

            _environment = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataLayer(_configuration);
            
            services
                .AddConsul(_configuration)
                .AddMessageBroker(_configuration)
                .AddOutbox(_configuration)
                .AddSwagger(_configuration)
                .AddCore(typeof(Startup), typeof(CreateSongCommand), typeof(EventsExtensions), typeof(MonkeyPlayerDataContext)); 
            // Types are needed for mediator to work the different projects. In this case startup is added 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder applicationBuilder,
            IWebHostEnvironment webHostEnvironment,
            ILoggerFactory loggerFactory,
            IHostApplicationLifetime hostApplicationLifetime)
        {
            if (webHostEnvironment.IsDevelopment())
            {
                applicationBuilder.UseDeveloperExceptionPage();
            }

            UpdateDatabase(applicationBuilder);

            applicationBuilder
                .UseLogging(_configuration, loggerFactory)
                .UseSwagger(_configuration);
               // .UseConsul(hostApplicationLifetime);
            
           // app.UseHttpsRedirection();

            applicationBuilder.UseSwagger(_configuration);

            applicationBuilder.UseRouting();

            //app.UseAuthorization();

            applicationBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });

            applicationBuilder.UseSubscribeAllEvents();
        }
        
        private static void UpdateDatabase(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<MonkeyPlayerDataContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}