using Identity.Application.Common.Services.Interfaces;
using Identity.Domain.Repositories;
using Identity.Infrastructure.Models;
using Identity.Infrastructure.Persistence;
using Identity.Infrastructure.Repositories;
using Identity.Infrastructure.Services;
using Identity.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Get's the directory for storing keys - Defaults to "./Vault/" if no path is defined in configuration
            string keyDirectory = configuration.GetSection("KeyDirectory").Value ?? "./Vault/";

            // Configure JwtSettings with a configuration action
            services.Configure<JwtSettings>(options =>
            {
                options.Issuer = configuration.GetSection("JwtSettings:Issuer")?.Value ?? "default-issuer";
                options.Audience = configuration.GetSection("JwtSettings:Audience")?.Value ?? "default-audience";
            });

            // DbContext
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<IdentityDbContext>(options => options.UseNpgsql(connectionString));

            // Infrastructure services
            services.AddScoped<IKeyManager>(provider => new KeyManager(keyDirectory));

            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(keyDirectory))
                .SetApplicationName("SKPIdentity");

            services.AddScoped<IDataProtectorService, DataProtectorService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenProvider, TokenProvider>();
            services.AddScoped<IBCryptPasswordHasher, BCryptPasswordHasher>();
            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<IApiClientRepository, ApiClientRepository>();

            return services;
        }
    }
}
