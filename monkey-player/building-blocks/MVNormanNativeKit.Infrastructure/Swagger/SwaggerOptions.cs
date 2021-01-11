using System;
using Microsoft.OpenApi.Models;

namespace MVNormanNativeKit.Infrastructure.Swagger
{
    public class SwaggerOptions : OpenApiInfo
    {
        public string VersionName { get; set; } = "v1";
        public string RoutePrefix { get; set; } = "";

        public SwaggerOptions()
        {
            Contact = new OpenApiContact
            {
                Name = "MVNorman GitHub",
                Email = string.Empty,
                Url = new Uri("https://github.com/MVNorman"),
            };
        }
    }
}