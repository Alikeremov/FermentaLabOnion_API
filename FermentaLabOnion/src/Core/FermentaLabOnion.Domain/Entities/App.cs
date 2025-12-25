using FermentaLabOnion.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Domain.Entities
{
    public class App:BaseEntity
    {
        public string Tittle { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Image { get; set; } = null!;
        public bool IsMain { get; set; }
        //Relational properties
        public ICollection<AppTranslate>? ApplicationTranslates { get; set; }
    }
}
