namespace ToroBank.WebApi.Extensions.StartupExtensions;

using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prometheus;

public static class HealthChecksExtension
{
    public static IServiceCollection AddHealthChecksExtension(this IServiceCollection services, IConfiguration configuration)
    {
        if (System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
        {
            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }
        else
        {
            services.AddHealthChecks()
                .AddSqlServer(ReplaceConnectionString(configuration.GetConnectionString("ToroBankConnection")));
        }

        return services;
    }

    private static string ReplaceConnectionString(string conn)
    {
        Console.Write("-------------------DATABASE CONNECTION STRING--------------------------------------------------------------------------------------------------------");
        Console.Write(conn);
        Console.Write(conn.Replace("[DB_ENV]", System.Environment.GetEnvironmentVariable("DB_ENV")));
        Console.Write("---------------------------------------------------------------------------------------------------------------------------");
        return conn.Replace("[DB_ENV]", System.Environment.GetEnvironmentVariable("DB_ENV"));
    }
   

    public static IApplicationBuilder UseHealthChecksExtension(this IApplicationBuilder app)
    {
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
        });

        return app;
    }
}