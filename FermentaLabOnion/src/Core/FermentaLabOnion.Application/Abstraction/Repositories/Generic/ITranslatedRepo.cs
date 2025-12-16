using FermentaLabOnion.Domain.Entities.Common;
using FermentaLabOnion.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Abstraction.Repositories.Generic
{
    public interface ITranslatedRepo<Ttranslate> : IRepository<Ttranslate> where Ttranslate : BaseEntityTranslate, new()
    {
        IQueryable<Ttranslate> GetAllTranslated(Language language = Language.Azerbaijani,
            bool isTracking = false, bool QueryFilter = false, params string[] includes);
        IQueryable<Ttranslate> GetAllWhereTranslated
            (
                Expression<Func<Ttranslate, bool>>? expression = null,
                Expression<Func<Ttranslate, object>>? orderexpression = null,
                 Language language = Language.Azerbaijani,
                 bool isDescending = false, bool isTracking = false, bool queryFilter = false,
                int skip = 0, int take = 0, params string[] includes
            );
        Task<Ttranslate> GetByIdTranslatedAsync(int id, Language language = Language.Azerbaijani, bool isTracking = false, bool QueryFilter = false, params string[] includes);
        Task<Ttranslate> GetByExpressionTranslatedAsync
            (
                Expression<Func<Ttranslate, bool>> expression, Language language = Language.Azerbaijani, bool isTracking = false,
                bool QueryFilter = false, params string[] includes
            );
    }
}
