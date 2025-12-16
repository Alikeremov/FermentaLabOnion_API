using FermentaLabOnion.Application.Abstraction.Repositories.Generic;
using FermentaLabOnion.Domain.Entities.Common;
using FermentaLabOnion.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Persistence.Implementations.Repositories.Generic
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;
        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveChangesAsync();
        }
        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await SaveChangesAsync();
        }


        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }
        public IQueryable<T> GetAll(bool isTracking = false, bool QueryFilter = false, params string[] includes)
        {
            IQueryable<T> query = _dbSet;

            query = isTracking ? query : query.AsNoTracking();
            query = QueryFilter ? query : query.IgnoreQueryFilters();
            query = Includes(query, includes);
            return query;
        }
        public IQueryable<T> GetAllWhere(Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>? orderexpression = null,
             bool isDescending = false, bool isTracking = false, bool queryFilter = false, int skip = 0, int take = 0, params string[] includes)
        {
            IQueryable<T> query = _dbSet;
            if (expression != null) query = query.Where(expression);
            if (orderexpression != null)
            {
                query = isDescending ? query.OrderByDescending(orderexpression) : query.OrderBy(orderexpression);
            }

            if (skip != 0) query = query.Skip(skip);
            if (take != 0) query = query.Take(take);
            query = isTracking ? query : query.AsNoTracking();
            query = queryFilter ? query : query.IgnoreQueryFilters();
            query = Includes(query, includes);
            return query;
        }

        public async Task<T> GetByExpressionAsync(Expression<Func<T, bool>> expression, bool isTracking = false,
            bool QueryFilter = false, params string[] includes)
        {
            IQueryable<T> query = _dbSet.Where(expression);

            query = isTracking ? query : query.AsNoTracking();
            query = QueryFilter ? query : query.IgnoreQueryFilters();
            query = Includes(query, includes);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<T> GetByIdAsync(int id, bool isTracking = false, bool QueryFilter = false, params string[] includes)
        {
            IQueryable<T> query = _dbSet.Where(t => t.Id == id);

            query = isTracking ? query : query.AsNoTracking();
            query = QueryFilter ? query : query.IgnoreQueryFilters();
            query = Includes(query, includes);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<T> GetByIdWithoutDeletedAsync(int id, bool isTracking = false, bool QueryFilter = false, params string[] includes)
        {
            IQueryable<T> query = _dbSet.Where(t => t.Id == id);
            query = isTracking ? query : query.AsNoTracking();
            query = QueryFilter ? query : query.IgnoreQueryFilters();
            query = Includes(query, includes);
            return await query.FirstOrDefaultAsync();
        }
        private static IQueryable<T> Includes(IQueryable<T> query, params string[] includes)
        {
            if (includes != null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }
            return query;
        }
    }
}
