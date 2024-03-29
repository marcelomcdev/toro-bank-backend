﻿using Microsoft.OpenApi.Models;
using System.Reflection;


namespace ToroBank.WebApi.Extensions.StartupExtension
{
    public static class SwaggerExtension
    {
        private const string _apiName = "ToroBank API";
        private const string _apiVersionV1 = "v1";

        public static IServiceCollection AddSwaggerExtension(this IServiceCollection services, IConfiguration configuration)
        {
            var oauthAuthority = configuration.GetValue<string>("Authentication:Jwt:Authority");
            
            var oauthScopes = new Dictionary<string, string>
            {
                { configuration.GetValue<string>("Authentication:Swagger:Scopes:Api"), _apiName }
            };

            // https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.1&tabs=visual-studio#customize-and-extend
            return services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    _apiVersionV1,
                    new OpenApiInfo
                    {
                        Title = _apiName,
                        Version = _apiVersionV1,
                        Contact = new OpenApiContact
                        {
                            Name = "Marcelo Martins de Castro",
                            Email = "marcelo.developer@outlook.com",
                            Url = new Uri("https://github.com/marcelomcdev/toro-bank-backend/commits/main"),
                        }
                    });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }

        public static IApplicationBuilder UseSwaggerExtension(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", _apiName);

                options.OAuthClientId(configuration.GetValue<string>("Authentication:Swagger:ClientId"));
                options.OAuthClientSecret(configuration.GetValue<string>("Authentication:Swagger:ClientSecret"));
                options.OAuthUsePkce();
            });
            app.UseReDoc(options =>
            {
                options.DocumentTitle = "REDOC API DOC";
                options.SpecUrl = "/swagger/v1/swagger.json";
                options.RoutePrefix = "redoc";
            });
            return app;
        }
    }
}
