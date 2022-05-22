using Hellang.Middleware.ProblemDetails.Mvc;
using Serilog;

using ToroBank.Application;
using ToroBank.Application.Common.Interfaces.Services;
using ToroBank.Infrastructure.Persistence;
using ToroBank.Infrastructure.Persistence.Context;
using ToroBank.Infrastructure.Shared;
using ToroBank.WebApi.Extensions.StartupExtension;
using ToroBank.WebApi.Extensions.StartupExtensions;
using ToroBank.WebApi.Services;

namespace ToroBank.WebApi.Extensions
{
    public static class ProgramExtensions
    {
        public static void SetupSerilog(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, loggerConfig) => loggerConfig
                    .WriteTo.Console()
                    .WriteTo.Debug()
                    .ReadFrom.Configuration(context.Configuration));
        }

        public static void SetupServices(this WebApplicationBuilder builder)
        {
            builder.Configuration.AddJsonFile("config.json", optional: false, reloadOnChange: true);

            builder.Services.AddApplication()
                            .AddInfrastructurePersistence(builder.Configuration)
                            .AddInfrastructureShared(builder.Configuration);

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddControllers()
                            .AddProblemDetailsConventions();

            builder.Services.AddRouting(options => options.LowercaseUrls = true);

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

            builder.Services.AddProblemDetailsExtension()
                            .AddAuthenticationExtension(builder.Configuration)
                            .AddHealthChecksExtension(builder.Configuration)
                            .AddSwaggerExtension(builder.Configuration);

        }

        public static void SetupRequestPipeline(this WebApplication app)
        {
            app.UseProblemDetailsExtension();

            app.UseForwardHeadersExtension(app.Configuration);

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSwaggerExtension(app.Configuration);

            app.UseHealthChecksExtension();

            //In the future, if toro app will have an auth page, uncomment this line to require auth in endpoints. 
            app.MapControllers();//.RequireAuthorization(); 
        }
    }
}
