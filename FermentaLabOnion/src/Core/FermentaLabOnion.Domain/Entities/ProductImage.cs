using FermentaLabOnion.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Domain.Entities
{
    public class ProductImage:BaseEntity
    {
        public string Url { get; set; } = null!;
        public bool IsPrimary { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
