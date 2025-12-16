using FermentaLabOnion.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Domain.Entities
{
    public class TagTranslate:BaseEntityTranslateNameable
    {
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
