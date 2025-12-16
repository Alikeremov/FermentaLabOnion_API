using FermentaLabOnion.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Domain.Entities
{
    public class Tag:BaseEntityNameable
    {
        //Relational properties
        public ICollection<ProductTag>? ProductTags { get; set; }
        public ICollection<TagTranslate>? TagTranslates { get; set; }

    }
}
