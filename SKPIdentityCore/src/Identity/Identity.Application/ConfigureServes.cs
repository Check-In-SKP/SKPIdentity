using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;

namespace Identity.Application
{
    public static class ConfigureServes
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Add AutoMapper services
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Add MediatR services
            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            return services;
        }
    }
}
