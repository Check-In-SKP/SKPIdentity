using Identity.Infrastructure;
using Identity.Application;
using Identity.API.Endpoints;
using Identity.API;
using System.Security.Claims;
using Identity.Domain.Entities.Enums;
using Identity.SharedKernel.Models.Enums;

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

        // Add authentication service
        builder.Services.AddCustomJwtAuthentication(builder.Configuration);
        builder.Services.AddAuthorization(options =>
        {
            // Token types to restrict endpoint access to specific tokens
            options.AddPolicy("RequireIdToken", policy =>
                policy.RequireClaim("token_type", TokenType.IdToken.ToTokenString()));
            options.AddPolicy("RequireAccessToken", policy =>
                policy.RequireClaim("token_type", TokenType.AccessToken.ToTokenString()));
            options.AddPolicy("RequireNFCToken", policy =>
                policy.RequireClaim("token_type", TokenType.NFCToken.ToTokenString()));

            // Roles that are allowed to access Admin authorized endpoints
            options.AddPolicy("RequireAdminRole", policy =>
                policy.RequireAssertion(context => 
                    context.User.HasClaim(ClaimTypes.Role, Role.Admin.ToTokenString())));

            // Roles that are allowed to access User authorized endpoints
            options.AddPolicy("RequireUserRole", policy =>
                //policy.RequireClaim(ClaimTypes.Role, Role.User.ToTokenString()));
                policy.RequireAssertion(context =>
                    context.User.HasClaim(ClaimTypes.Role, Role.User.ToTokenString()) ||
                    context.User.HasClaim(ClaimTypes.Role, Role.Admin.ToTokenString())));

            // Roles that are allowed to access ApiClient authorized endpoints
            options.AddPolicy("RequireApiClientRole", policy =>
                policy.RequireAssertion(context => 
                    context.User.HasClaim(ClaimTypes.Role, Role.Admin.ToTokenString()) || 
                    context.User.HasClaim(ClaimTypes.Role, Role.ApiClient.ToTokenString())));

            // Roles that are allowed to access Guest authorized endpoints
            options.AddPolicy("RequireGuestRole", policy =>
                policy.RequireAssertion(context =>
                    context.User.HasClaim(ClaimTypes.Role, Role.Guest.ToTokenString()) ||
                    context.User.HasClaim(ClaimTypes.Role, Role.User.ToTokenString()) ||
                    context.User.HasClaim(ClaimTypes.Role, Role.Admin.ToTokenString())));
        });

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