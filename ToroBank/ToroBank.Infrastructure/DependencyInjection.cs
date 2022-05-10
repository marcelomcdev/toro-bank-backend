using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToroBank.Application.Common.Interfaces.Repositories;
using ToroBank.Application.Features.Users;
using ToroBank.Infrastructure.Persistence.Context;
using ToroBank.Infrastructure.Persistence.Repositories;

namespace ToroBank.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            #region Repositories
            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>))
                    .AddScoped<IUserRepository, UserRepository>();
            #endregion

            return services;
        }
    }
}
