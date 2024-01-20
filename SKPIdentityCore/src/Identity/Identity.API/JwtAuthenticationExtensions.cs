using Identity.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Identity.API
{
    public static class JwtAuthenticationExtensions
    {
        public static IServiceCollection AddCustomJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            #region Symmetric (HMAC) JWT Authentication
            services.AddAuthentication("HMACJwtBearer")
                .AddJwtBearer("HMACJwtBearer", options =>
                {
                    // Resolve IKeyManager within the service scope for HMAC
                    using var scope = services.BuildServiceProvider().CreateScope();
                    var keyManager = scope.ServiceProvider.GetRequiredService<IKeyManager>();
                    var hmacKey = keyManager.HmacKeyAsync.GetAwaiter().GetResult();

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // Use HMAC key for validation
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(hmacKey),
                        ValidateLifetime = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        ValidAudience = configuration["JwtSettings:Audience"]
                    };

                    // Redirects client to login if not authenticated
                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = context =>
                        {
                            if (!context.Response.HasStarted && context.AuthenticateFailure != null)
                            {
                                // Clear the default response
                                context.HandleResponse();

                                // Redirects to login path
                                context.Response.Redirect("/login");
                            }

                            return Task.CompletedTask;
                        }
                    };
                });
            #endregion

            #region RSA JWT Authentication
            services.AddAuthentication("RSAJwtBearer")
                .AddJwtBearer("RSAJwtBearer", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
                        {
                            // Resolve IKeyManager within the service scope for RSA
                            var keyManager = services.BuildServiceProvider().GetRequiredService<IKeyManager>();
                            var rsaSecurityKey = keyManager.GetPublicRsaSecurityKeyAsync().GetAwaiter().GetResult();
                            return new List<RsaSecurityKey> { rsaSecurityKey };
                        },
                        ValidateLifetime = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        ValidAudience = configuration["JwtSettings:Audience"]
                    };

                    // Redirects client to login if not authenticated
                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = context =>
                        {
                            if (!context.Response.HasStarted && context.AuthenticateFailure != null)
                            {
                                // Clear the default response
                                context.HandleResponse();

                                // Redirects to login path
                                context.Response.Redirect("/login");
                            }

                            return Task.CompletedTask;
                        }
                    };
                });
            #endregion

            return services;
        }   
    }
}
