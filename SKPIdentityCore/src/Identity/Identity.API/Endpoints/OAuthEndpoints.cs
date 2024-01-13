using Identity.Application.Common.Services.Interfaces;
using Identity.Infrastructure.Services.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text.Json;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace Identity.API.Endpoints
{
    public static class OAuthEndpoints
    {
        public static void MapOAuthEndpoints(this WebApplication app)
        {
            app.MapGet("/login", GetLoginHandler);
            app.MapPost("/login", LoginHandler);
            app.MapGet("/oauth/authorize", AuthorizeHandler);
            app.MapPost("/oauth/token", TokenHandler);
        }

        private static async Task<IResult> GetLoginHandler(string returnUrl, HttpResponse response)
        {
            response.ContentType = "text/html";
            await response.WriteAsync($@"
                <html>
                    <head>
                        <title>Login</title>
                        <style>
                            body {{ font-family: Arial, sans-serif; padding: 20px; }}
                            .container {{ max-width: 300px; margin: auto; }}
                            input[type='submit'] {{ margin-top: 20px; }}
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <form action='/login?returnUrl={HttpUtility.UrlEncode(returnUrl)}' method='post'>
                                <div>
                                    <label for='username'>Username:</label><br>
                                    <input type='text' id='username' name='username' required>
                                </div>
                                <div>
                                    <label for='password'>Password:</label><br>
                                    input type='password' id='password' name='password' required>
                                </div>
                                <input type='submit' value='Log In'>
                            </form>
                        </div>
                    </body>
                </html>
            ");
            return Results.Ok();
        }

        private static async Task<IResult> LoginHandler(HttpContext ctx, string returnUrl)
        {
            // Handle user authentication and set cookie
            // For demonstration, auto-generating a user identity
            await ctx.SignInAsync("cookie",
                new ClaimsPrincipal(
                    new ClaimsIdentity(
                        new Claim[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
                        },
                        "cookie"
                    )));
            return Results.Redirect(returnUrl);
        }

        private static IResult AuthorizeHandler(HttpContext ctx, IDataProtectionProvider dataProtectionProvider)
        {
            //// Extract and validate query parameters for the authorization request
            //// ...

            //var authCode = new AuthCode
            //{
            //    // Populate authCode with query parameter values
            //    // ...
            //};

            //var protector = dataProtectionProvider.CreateProtector("oauth");
            //var codeString = protector.Protect(JsonSerializer.Serialize(authCode));

            //return Results.Redirect($"{redirectUri}?code={codeString}&state={state}&iss={HttpUtility.UrlEncode("https://localhost")}");
            
            return Results.Ok();
        }

        private static async Task<IResult> TokenHandler(HttpRequest request, ITokenService tokenService, IAuthService authService)
        {
            //// Parse the request body and validate parameters
            //// ...

            //if (!authService.ValidateCodeVerifier(authCode, codeVerifier))
            //{
            //    return Results.BadRequest("Invalid code verifier");
            //}

            //// Generate access and refresh tokens
            //var accessToken = tokenService.GenerateAccessToken(new List<Claim>
            //{
            //    new Claim(JwtRegisteredClaimNames.Sub, authCode.ClientId)
            //    // Additional claims as needed
            //});

            //var refreshToken = tokenService.GenerateRefreshToken();

            //return Results.Ok(new
            //{
            //    access_token = accessToken,
            //    refresh_token = refreshToken,
            //    token_type = "Bearer",
            //    expires_in = 900 // 15 minutes in seconds
            //});
            
            return Results.Ok();
        }
    }
}
