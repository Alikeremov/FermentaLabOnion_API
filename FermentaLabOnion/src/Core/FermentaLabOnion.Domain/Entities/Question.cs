using FermentaLabOnion.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Domain.Entities
{
    public class Question:BaseEntity
    {
        public string Tittle { get; set; } = null!;
        public string Description { get; set; } = null!;
        //Relational properties
        public ICollection<QuestionTranslate>? QuestionTranslates { get; set; }
    }
}
