using System;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace MVNormanNativeKit.Infrastructure.Serilog
{
    public static class SerilogExtension
    {
        public static void ConfigureSerilog(this IConfiguration configuration, SerilogCredentials credentials)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(credentials.ElasticSearchUrl))
                {
                    AutoRegisterTemplate = true,
                    IndexFormat = credentials.SerilogIndex
                })

                .CreateLogger();
        }
    }
}
