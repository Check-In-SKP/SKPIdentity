using Identity.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Repositories
{
    public class GenericRepository<T, TKey> : IGenericRepository<T, TKey> where T : class
    {
        private readonly IdentityContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(IdentityContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<T?> GetByIdAsync(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T?>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }


        public async Task<IEnumerable<T?>> GetWithPaginationAsync(int page, int pageSize)
        {
            return await _dbSet.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public void Remove(TKey id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public void RemoveRange(IEnumerable<TKey> ids)
        {
            foreach (var id in ids)
            {
                var entity = _dbSet.Find(id);
                if (entity != null)
                {
                    _dbSet.Remove(entity);
                }
            }
        }

        public void Update(T entity)
        {
            if (entity == null)
                return;

            _dbSet.Update(entity);
        }

        public async Task<bool> ExistsAsync(TKey id)
        {
            return await _dbSet.FindAsync(id) != null;   
        }

        public IQueryable<T?> Query()
        {
            return _dbSet.AsQueryable();
        }
    }
}
