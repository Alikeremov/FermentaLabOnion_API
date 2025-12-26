using FermentaLabOnion.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Domain.Entities
{
    public class ChineseWisdom:BaseEntity
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Image { get; set; } = null!;
        //Relational properties

        public ICollection<ChineseWisdomTranslate>? Translates { get; set; }
    }
}
