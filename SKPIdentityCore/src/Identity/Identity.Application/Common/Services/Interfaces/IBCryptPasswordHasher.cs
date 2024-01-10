namespace Identity.Application.Common.Services.Interfaces
{
    public interface IBCryptPasswordHasher
    {
        public string HashPassword(string password);
        public bool VerifyPassword(string password, string hashedPassword);
    }
}
