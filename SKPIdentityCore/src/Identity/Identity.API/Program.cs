using Identity.Infrastructure;
using Identity.Application;
using Identity.API.Endpoints;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Identity.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


        // Add services to the container.
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Add application and infrastructure services
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(builder.Configuration);

        #region JWT Authentication
        // Get's keys from KeyManager
        var keyManager = builder.Services.BuildServiceProvider().GetService<IKeyManager>();
        var publicRsaKey = await keyManager.GetPublicRsaKeyAsync();
        var hmacKey = await keyManager.HmacKeyAsync;

        #region Symmetric (HMAC) JWT Authentication
        builder.Services.AddAuthentication("HMACJwtBearer")
            .AddJwtBearer("HMACJwtBearer", options =>
            {
                var keyManager = builder.Services.BuildServiceProvider().GetService<IKeyManager>();

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // Use HMAC key for validation
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(hmacKey),
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                    ValidAudience = builder.Configuration["JwtSettings:Audience"]
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
        builder.Services.AddAuthentication("RSAJwtBearer")
            .AddJwtBearer("RSAJwtBearer", options =>
        {
            var keyManager = builder.Services.BuildServiceProvider().GetService<IKeyManager>();

            options.TokenValidationParameters = new TokenValidationParameters
            {
                // Use RSA key for validation
                IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
                {;
                    return new List<RsaSecurityKey> { publicRsaKey };
                },
                ValidateLifetime = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                ValidAudience = builder.Configuration["JwtSettings:Audience"]
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

        #endregion

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseHttpsRedirection();

        // OAuth Endpoints
        app.MapOAuthEndpoints();

        app.Run();
    }
}