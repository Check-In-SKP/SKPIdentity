using Identity.Infrastructure.Services.Interfaces;
using Identity.SharedKernel.Models;
using System.Security.Cryptography;


//using Identity.Infrastructure.Models;
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
            app.MapGet("/login", LoginPage);
            app.MapPost("/login", AuthorizationHandler);
            app.MapGet("/oauth/token/verification-key", GetVerificationKey);
            //app.MapGet("/oauth/authorize", AuthorizeHandler);
            //app.MapPost("/oauth/token", TokenHandler);
        }

        private static async Task<IResult> LoginPage(string returnUrl, HttpResponse response)
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
                                    <input type='password' id='password' name='password' required>
                                </div>
                                <input type='submit' value='Log In'>
                            </form>
                        </div>
                    </body>
                </html>
            ");
            return Results.Ok();
        }

         private static async Task<IResult> AuthorizationHandler(HttpContext ctx, IDataProtectionProvider dataProtectionProvider)
        {
            var queryParams = ctx.Request.Query;
            var clientId = queryParams["client_id"];
            var codeChallenge = queryParams["code_challenge"];
            var codeChallengeMethod = queryParams["code_challenge_method"];
            var redirectUri = queryParams["redirect_uri"];
            var scope = queryParams["scope"];
            var state = queryParams["state"];

            if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(codeChallenge) || string.IsNullOrEmpty(redirectUri))
            {
                return Results.BadRequest("Missing required parameters");
            }

            var authCode = new AuthCode
            {
                ClientId = clientId,
                CodeChallenge = codeChallenge,
                CodeChallengeMethod = codeChallengeMethod,
                RedirectUri = redirectUri,
                Expiry = DateTime.UtcNow.AddMinutes(5)
            };

            var protector = dataProtectionProvider.CreateProtector("OAuth");
            var protectedCode = protector.Protect(JsonSerializer.Serialize(authCode));

            var responseUri = $"{redirectUri}?code={protectedCode}&state={state}";
            return Results.Redirect(responseUri);
        }
        private static async Task<IResult> GetVerificationKey(IKeyManager keyManager)
        {
            var rsaPublicKey = await keyManager.GetSerializedPublicKeyAsync();

            return Results.Ok(rsaPublicKey);
        }
    }
}
