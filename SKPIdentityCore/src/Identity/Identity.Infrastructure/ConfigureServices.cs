using Identity.Application.Common.Services.Interfaces;
using Identity.Domain.Repositories;
using Identity.Infrastructure.Persistence;
using Identity.Infrastructure.Repositories;
using Identity.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // DbContext
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<IdentityDbContext>(options => options.UseNpgsql(connectionString));

            // Infrastructure and Application Services
            services.AddScoped<IDataProtectorService, DataProtectorService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IBCryptPasswordHasher, BCryptPasswordHasher>();
            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<IApiClientRepository, ApiClientRepository>();
            services.AddScoped<IDynamicRoleRepository, DynamicRoleRepository>();

            return services;
        }
    }
}
