using FermentaLabOnion.Application.Abstraction.Repositories.Generic;
using FermentaLabOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Abstraction.Repositories
{
    public interface IQuestionTranslateRepo: ITranslatedRepo<QuestionTranslate>
    {
    }
}
