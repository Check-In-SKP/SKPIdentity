namespace Identity.Application.Common.Services.Interfaces
{
    public interface IDataProtectorService
    {
        public string Protect(string input);
        public string Unprotect(string input);
    }
}
