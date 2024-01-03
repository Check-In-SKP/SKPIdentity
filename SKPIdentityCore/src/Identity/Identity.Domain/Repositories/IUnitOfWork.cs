using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IDynamicRoleRepository DynamicRoleRepository { get; }
        IUserRepository UserRepository { get; }
        ISessionRepository SessionRepository { get; }
        IApiClientRepository ApiClientRepository { get; }
        Task<int> CompleteAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        void RollbackTransaction();
    }
}
