using FermentaLabOnion.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Domain.Entities
{
    public class InformationTranslate:BaseEntityTranslate
    {
        public string Title { get; set; } = null!;
        //Relational properties
        public int InformationId { get; set; }
        public Information Information { get; set; } = null!;
    }
}
