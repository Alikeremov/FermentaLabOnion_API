using FermentaLabOnion.Domain.Entities.Common;
using FermentaLabOnion.Domain.Enums;
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
        public ImageType ImageType { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
