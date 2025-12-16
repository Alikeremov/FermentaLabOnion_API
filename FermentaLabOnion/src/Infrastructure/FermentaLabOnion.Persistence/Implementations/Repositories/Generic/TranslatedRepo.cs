using FermentaLabOnion.Application.Abstraction.Repositories.Generic;
using FermentaLabOnion.Domain.Entities.Common;
using FermentaLabOnion.Domain.Enums;
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
    public class TranslatedRepo<Ttranslate> : Repository<Ttranslate>, ITranslatedRepo<Ttranslate> where Ttranslate : BaseEntityTranslate, new()
    {

        public TranslatedRepo(AppDbContext context) : base(context)
        {
        }
        public IQueryable<Ttranslate> GetAllTranslated(Language language = Language.Azerbaijani,
    bool isTracking = false, bool QueryFilter = false, params string[] includes)
        {
            var query = _dbSet.AsQueryable().Where(x => x.Language == language);

            foreach (var include in includes)
                query = query.Include(include);

            if (!isTracking)
                query = query.AsNoTracking();

            return query;
        }

        public IQueryable<Ttranslate> GetAllWhereTranslated(
            Expression<Func<Ttranslate, bool>>? expression = null,
            Expression<Func<Ttranslate, object>>? orderexpression = null,
            Language language = Language.Azerbaijani,
            bool isDescending = false, bool isTracking = false, bool queryFilter = false,
            int skip = 0, int take = 0, params string[] includes)
        {
            var query = _dbSet.AsQueryable().Where(x => x.Language == language);

            if (expression != null)
                query = query.Where(expression);

            if (orderexpression != null)
                query = isDescending ? query.OrderByDescending(orderexpression) : query.OrderBy(orderexpression);

            foreach (var include in includes)
                query = query.Include(include);

            query = query.Skip(skip).Take(take);

            if (!isTracking)
                query = query.AsNoTracking();

            return query;
        }

        public async Task<Ttranslate> GetByIdTranslatedAsync(int id, Language language = Language.Azerbaijani,
    bool isTracking = false, bool QueryFilter = false, params string[] includes)
        {
            var query = _dbSet.AsQueryable().Where(x => x.Id == id && x.Language == language);

            foreach (var include in includes)
                query = query.Include(include);

            if (!isTracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Ttranslate> GetByExpressionTranslatedAsync(
    Expression<Func<Ttranslate, bool>> expression,
    Language language = Language.Azerbaijani,
    bool isTracking = false, bool QueryFilter = false, params string[] includes)
        {
            var query = _dbSet.AsQueryable().Where(x => x.Language == language).Where(expression);
            foreach (var include in includes)
                query = query.Include(include);
            if (!isTracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync();
        }
    }
}
