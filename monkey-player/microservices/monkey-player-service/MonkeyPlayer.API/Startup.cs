using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MonkeyPlayer.Application;
using MonkeyPlayer.Persistence;
using MVNormanNativeKit.Infrastructure.Core;
using MVNormanNativeKit.Infrastructure.Logging;
using MVNormanNativeKit.Infrastructure.MessageBrokers;
using MVNormanNativeKit.Infrastructure.Outbox;
using MVNormanNativeKit.Infrastructure.Swagger;
using Serilog;

namespace MonkeyPlayer.API
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
            services.AddPersistence(_configuration);
            
            services.AddControllers();

            services
               // .AddConsul(Configuration)
                .AddMessageBroker(_configuration)
                .AddOutbox(_configuration)
                .AddSwagger(_configuration)
                .AddCore(typeof(Startup), typeof(EventsExtensions), typeof(MonkeyPlayerDataContext)); 
            // Types are needed for mediator to work the different projects. In this case startup is added 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            UpdateDatabase(app);


            app
                .UseLogging(_configuration, loggerFactory)
                .UseSwagger(_configuration);
                //.UseConsul(lifetime);
            
           // app.UseHttpsRedirection();

            app.UseSwagger(_configuration);

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });

            app.UseSubscribeAllEvents();
        }
        
        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
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