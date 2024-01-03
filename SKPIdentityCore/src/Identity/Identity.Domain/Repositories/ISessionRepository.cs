using Identity.Domain.Entities;

namespace Identity.Domain.Repositories
{
    public interface ISessionRepository : IGenericRepository<Session, Guid>
    {
    }
}
