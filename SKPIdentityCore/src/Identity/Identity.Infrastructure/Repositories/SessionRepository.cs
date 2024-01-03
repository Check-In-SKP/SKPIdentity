using Identity.Domain.Entities;
using Identity.Domain.Repositories;
using Identity.Infrastructure.Persistence;

namespace Identity.Infrastructure.Repositories
{
    public class SessionRepository : GenericRepository<Session, Guid>, ISessionRepository
    {
        public SessionRepository(IdentityDbContext context) : base(context)
        {
        }
    }
}
