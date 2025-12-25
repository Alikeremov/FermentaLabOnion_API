using FermentaLabOnion.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Domain.Entities
{
    public class ShareSpecialTranslate:BaseEntityTranslate
    {
        public string Tittle { get; set; } = null!;
        public string Subtittle { get; set; } = null!;

        public int ShareSpecialId { get; set; }
        public ShareSpecial ShareSpecial { get; set; } = null!;
    }
}
