using FermentaLabOnion.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Domain.Entities
{
    public class ShareSpecial:BaseEntity
    {
        public string Tittle { get; set; } = null!;
        public string Subtittle { get; set; } = null!;
        public string Image { get; set; } = null!;
        //Relational properties

        public ICollection<ShareSpecialTranslate>? Translates { get; set; }
    }
}
