using FermentaLabOnion.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Abstraction.Repositories.Generic
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(bool isTracking = false, bool QueryFilter = false, params string[] includes);
        IQueryable<T> GetAllWhere
            (
                Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>? orderexpression = null,
                 bool isDescending = false, bool isTracking = false, bool queryFilter = false,
                int skip = 0, int take = 0, params string[] includes
            );
        Task<bool> IsExistAsync(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(int id, bool isTracking = false, bool QueryFilter = false, params string[] includes);
        Task<T> GetByExpressionAsync
            (
                Expression<Func<T, bool>> expression, bool isTracking = false,
                bool QueryFilter = false, params string[] includes
            );
        Task AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);

        Task SaveChangesAsync();
    }
}
