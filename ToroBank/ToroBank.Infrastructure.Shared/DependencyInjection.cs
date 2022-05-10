using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToroBank.Application.Common.Interfaces.Services;
using ToroBank.Infrastructure.Shared.Services;

namespace ToroBank.Infrastructure.Shared
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureShared(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDateTime, DateTimeService>();

            return services;
        }
    }
}
