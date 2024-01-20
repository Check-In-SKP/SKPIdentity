using Identity.Domain.Entities;

namespace Identity.Domain.Repositories
{
    public interface IUserRepository : IGenericRepository<User, Guid>
    {
        public Task<User?> GetByUsernameAsync(string username);
        public Task<User?> GetByEmailAsync(string email);
    }
}
