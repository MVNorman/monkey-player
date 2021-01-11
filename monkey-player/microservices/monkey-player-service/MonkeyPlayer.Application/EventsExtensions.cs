using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using MVNormanNativeKit.Infrastructure.Core.Events;
using MVNormanNativeKit.Infrastructure.MessageBrokers;

namespace MonkeyPlayer.Application
{
    public static class EventsExtensions
    {
        public static IApplicationBuilder UseSubscribeAllEvents(this IApplicationBuilder app)
        {
            var types = typeof(EventsExtensions)
                .GetTypeInfo()
                .Assembly
                .GetTypes()
                .Where(mytype => mytype.GetInterfaces().Contains(typeof(IEvent)));

            foreach (var type in types)
            {
                app.UseSubscribeEvent(type);
            }

            return app;
        }
    }
}