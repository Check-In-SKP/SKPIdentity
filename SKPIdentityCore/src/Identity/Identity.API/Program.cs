using Identity.Infrastructure;
using Identity.Application;
using Identity.API.Endpoints;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Add application and infrastructure services
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(builder.Configuration);

        // Configure JWT Authentication
        var jwtKey = builder.Configuration["JwtSettings:Secret"];
        var rsaPublicKey = LoadRsaPublicKey(builder.Configuration);

        if (string.IsNullOrEmpty(jwtKey))
            throw new Exception("JWT Key is missing from configuration file.");

        // Configure Authentication for HMAC-based ID Tokens and RSA-based Access Tokens
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                ValidateAudience = true,
                ValidAudience = builder.Configuration["JwtSettings:Audience"],
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                IssuerSigningKeyResolver = (token, securityToken, keyIdentifier, validationParameters) =>
                {
                    return new[] { new RsaSecurityKey(rsaPublicKey) };
                }
            };
        });

        //// Authentication
        //builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        //    .AddCookie(options => options.LoginPath = "/login");

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        // OAuth Endpoints
        app.MapOAuthEndpoints();

        app.Run();
    }

    private static RSAParameters LoadRsaPublicKey(IConfiguration configuration)
    {
        // Retrieve the public key from configuration
        var publicKey = configuration["JwtSettings:RsaPublicKey"];

        if (string.IsNullOrWhiteSpace(publicKey))
        {
            throw new Exception("RSA public key is missing from the configuration.");
        }

        using var rsa = RSA.Create();

        rsa.ImportFromPem(publicKey.ToCharArray());

        return rsa.ExportParameters(false);
    }
}