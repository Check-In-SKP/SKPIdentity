using System.Text;

namespace Identity.API.Endpoints.OAuth
{
    public static class TokenEndpoint
    {
        public static async Task<IResult> Handle(HttpRequest request)
        {
            var bodyBytes = await request.BodyReader.ReadAsync();
            var bodyContent = Encoding.UTF8.GetString(bodyBytes.Buffer);

            string grantType = "", code = "", redirectUri = "", codeVerifier = "";
            foreach (var item in bodyContent.Split('&'))
            {
                var subParts = item.Split('=');
                var key = subParts[0];
                var value = subParts[1];
                if (key == "grant_type") grantType = value;
                else if (key == "code") code = value;
                else if (key == "redirect_uri") redirectUri = value;
                else if (key == "code_verifier") codeVerifier = value;
            }

            return Results.Ok();
        }
    }
}
