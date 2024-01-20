namespace Identity.Domain.Entities.Enums
{
    /// <summary> Static roles used in Identity </summary>
    public enum Role
    {
        Admin = 1,
        User = 2,
        ApiClient = 3,
        Guest = 4
    }

    public static class RoleExtensions
    {
        // Formats role to fit standard naming conventions (used for external systems)
        public static string ToTokenString(this Role role)
        {
            return role switch
            {
                Role.Admin => "admin",
                Role.User => "user",
                Role.ApiClient => "api_client",
                Role.Guest => "guest",
                _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
            };
        }

        public static Role FromTokenStringToRole(this string role)
        {
            return role switch
            {
                "admin" => Role.Admin,
                "user" => Role.User,
                "api_client" => Role.ApiClient,
                "guest" => Role.Guest,
                _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
            };
        }
    }
}
