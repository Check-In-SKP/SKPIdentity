using Identity.Application.Common.Services.Interfaces;
using Identity.Domain.Repositories;
using Identity.Infrastructure.Models;
using Identity.Infrastructure.Persistence;
using Identity.Infrastructure.Repositories;
using Identity.Infrastructure.Services;
using Identity.Infrastructure.Services.Interfaces;
using Identity.Infrastructure.Services.OAuthService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            // DbContext
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<IdentityDbContext>(options => options.UseNpgsql(connectionString));

            // Infrastructure services
            services.AddScoped<IKeyManager, KeyManager>();
            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

            // Infrastructure and Application Services
            services.AddScoped<IDataProtectorService, DataProtectorService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenProvider, TokenProvider>();
            services.AddScoped<IBCryptPasswordHasher, BCryptPasswordHasher>();

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<IApiClientRepository, ApiClientRepository>();

            return services;
        }
    }
}
