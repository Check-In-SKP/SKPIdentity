using Identity.Domain.Repositories;
using Identity.Infrastructure.Persistence;

namespace Identity.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IdentityDbContext _context;

        public IDynamicRoleRepository DynamicRoleRepository { get; }
        public IUserRepository UserRepository { get; }
        public ISessionRepository SessionRepository { get; }
        public IApiClientRepository ApiClientRepository { get; }

        public UnitOfWork(IdentityDbContext context)
        {
            DynamicRoleRepository = new DynamicRoleRepository(context);
            UserRepository = new UserRepository(context);
            SessionRepository = new SessionRepository(context);
            ApiClientRepository = new ApiClientRepository(context);
        }

        public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            await _context.Database.CommitTransactionAsync(cancellationToken);
        }

        public void RollbackTransaction()
        {
            _context.Database.RollbackTransaction();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
