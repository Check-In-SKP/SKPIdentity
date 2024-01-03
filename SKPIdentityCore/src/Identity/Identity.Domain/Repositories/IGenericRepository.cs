using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Repositories
{
    public interface IGenericRepository<T, TKey> where T : class
    {
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task<T?> GetByIdAsync(TKey id);
        Task<IEnumerable<T?>> GetAllAsync();
        Task<IEnumerable<T?>> GetWithPaginationAsync(int page, int pageSize);
        void Remove(TKey id);
        void RemoveRange(IEnumerable<TKey> entities);
        void Update(T entity);
        Task<bool> ExistsAsync(TKey id);
        IQueryable<T?> Query();
    }
}
