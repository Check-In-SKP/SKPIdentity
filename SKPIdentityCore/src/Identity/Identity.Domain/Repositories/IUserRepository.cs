using Identity.Domain.Entities;

namespace Identity.Domain.Repositories
{
    public interface IUserRepository : IGenericRepository<User, Guid>
    {
    }
}
