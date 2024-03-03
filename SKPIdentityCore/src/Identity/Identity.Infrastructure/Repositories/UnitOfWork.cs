using Identity.Application.Common.Services.Interfaces;
using Identity.Domain.Common;
using Identity.Domain.Repositories;
using Identity.Infrastructure.Persistence;
using Identity.Infrastructure.Services;

namespace Identity.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly IdentityDbContext _context;
        public IUserRepository UserRepository { get; }
        public ISessionRepository SessionRepository { get; }
        public IApiClientRepository ApiClientRepository { get; }

        public UnitOfWork(IdentityDbContext context, IDomainEventDispatcher domainEventDispatcher)
        {
            UserRepository = new UserRepository(context);
            SessionRepository = new SessionRepository(context);
            ApiClientRepository = new ApiClientRepository(context);
            _domainEventDispatcher = domainEventDispatcher;
            _context = context;
        }

        public async Task CompleteAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
            await DispatchDomainEventsAsync();
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            await _context.Database.CommitTransactionAsync(cancellationToken);
            await DispatchDomainEventsAsync();
        }

        public void RollbackTransaction()
        {
            _context.Database.RollbackTransaction();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        private async Task DispatchDomainEventsAsync()
        {
            // Dispatches domain events if any
            var domainEntities = this._context.ChangeTracker
                .Entries<BaseEntity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

            await _domainEventDispatcher.DispatchEventsAsync(domainEvents, CancellationToken.None);
        }
    }
}
