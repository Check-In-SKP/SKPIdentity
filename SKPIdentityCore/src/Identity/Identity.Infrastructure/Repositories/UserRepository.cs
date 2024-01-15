using Identity.Domain.Entities;
using Identity.Domain.Repositories;
using Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User, Guid>, IUserRepository
    {
        private readonly IdentityDbContext _context;

        public UserRepository(IdentityDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email) => await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

        public async Task<User?> GetByUsernameAsync(string username) => await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
    }
}
