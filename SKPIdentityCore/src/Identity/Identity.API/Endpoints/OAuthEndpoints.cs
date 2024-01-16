using Identity.Application.Common.Services.Interfaces;
using Identity.Infrastructure.Models;
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
            //app.MapPost("/oauth/token", TokenHandler);
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
            // Simulate user authentication - in real scenario, validate user credentials
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, "SampleUser")
            };

            var claimsIdentity = new ClaimsIdentity(claims, "cookie");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await ctx.SignInAsync("cookie", claimsPrincipal);

            return Results.Redirect(string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl);
        }

        private static IResult AuthorizeHandler(HttpContext ctx, IDataProtectionProvider dataProtectionProvider)
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

        private static async Task<IResult> TokenHandler(HttpRequest request, ITokenProvider tokenService, IAuthService authService, IDataProtectionProvider dataProtectionProvider)
        {
            var bodyContent = await new StreamReader(request.Body).ReadToEndAsync();
            var parsedContent = HttpUtility.ParseQueryString(bodyContent);

            var grantType = parsedContent["grant_type"];
            var code = parsedContent["code"];
            var redirectUri = parsedContent["redirect_uri"];
            var codeVerifier = parsedContent["code_verifier"];

            if (grantType != "authorization_code")
            {
                return Results.BadRequest("Unsupported grant type");
            }

            var protector = dataProtectionProvider.CreateProtector("OAuth");
            AuthCode authCode;
            try
            {
                var unprotectedCode = protector.Unprotect(code);
                authCode = JsonSerializer.Deserialize<AuthCode>(unprotectedCode);
            }
            catch
            {
                return Results.BadRequest("Invalid code");
            }

            if (authCode == null || !authService.ValidateCodeVerifier(authCode, codeVerifier))
            {
                return Results.BadRequest("Invalid code verifier");
            }

            // Ensure that the auth code hasn't expired and redirectUri matches
            if (authCode.Expiry < DateTimeOffset.UtcNow || authCode.RedirectUri != redirectUri)
            {
                return Results.BadRequest("Invalid or expired authorization code");
            }

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, authCode.ClientId)
            };

            var accessToken = tokenService.GenerateAccessToken(claims);
            var refreshToken = tokenService.GenerateRefreshToken();

            return Results.Ok(new
            {
                access_token = accessToken,
                refresh_token = refreshToken,
                token_type = "Bearer",
                expires_in = 900 // 15 minutes
            });
        }
    }
}
