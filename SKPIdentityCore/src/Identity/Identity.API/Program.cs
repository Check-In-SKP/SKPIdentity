using Identity.Infrastructure;
using Identity.Application;
using Identity.API.Endpoints;
using Identity.API.Endpoints.OAuth;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Project services
        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddApplicationServices();

        // Add authentication
        builder.Services.AddAuthentication("cookie").AddCookie("cookie", o =>
        {
            o.LoginPath = "/login";
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapGet("/login", GetLogin.Handler);
        app.MapPost("/login", Login.Handler);
        app.MapGet("/oauth/authorize", AuthEndpoint.Handle).RequireAuthorization();
        app.MapPost("/oauth/token", TokenEndpoint.Handle);

        app.Run();
    }
}