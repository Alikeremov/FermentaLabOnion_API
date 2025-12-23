using FermentaLabOnion.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Domain.Entities
{
    public class Category:BaseEntityNameable
    {
        public string? Url { get; set; }
        public string? Description { get; set; }
        //Relational properties
        public ICollection<Product>? Products { get; set; }
        public ICollection<CategoryTranslate>? CategoryTranslates { get; set; }
    }
}
