using FermentaLabOnion.Application.Abstraction.Repositories;
using FermentaLabOnion.Domain.Entities;
using FermentaLabOnion.Persistence.Contexts;
using FermentaLabOnion.Persistence.Implementations.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Persistence.Implementations.Repositories
{
    public class ProductTranslateRepo: TranslatedRepo<ProductTranslate>, IProductTranslateRepo
    {
        public ProductTranslateRepo(AppDbContext context) : base(context)
        {
        }
    }
}
