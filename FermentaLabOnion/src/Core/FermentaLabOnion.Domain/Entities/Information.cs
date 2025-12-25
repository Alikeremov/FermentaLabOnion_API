using FermentaLabOnion.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Domain.Entities
{
    public class Information:BaseEntity
    {
        public string Image { get; set; } = null!;
        public string Tittle { get; set; } = null!;
        //Relational properties
        public ICollection<InformationTranslate> Translates { get; set; }
    }
}
