using FermentaLabOnion.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Domain.Entities
{
    public class CategoryTranslate:BaseEntityTranslateNameable
    {
        public string? Description { get; set; }
        //Reletional properties
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
