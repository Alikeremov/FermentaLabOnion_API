using FermentaLabOnion.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Domain.Entities
{
    public class ProductTranslate:BaseEntityTranslateNameable
    {
        public string Slug { get; set; } = null!;
        public string ShortDescription { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string QuantityPerPackage { get; set; } = null!;
        public string Volume { get; set; } = null!;
        public string PackageType { get; set; } = null!;
        public string Ingredients { get; set; } = null!;
        public string Benefits { get; set; } = null!;
        public string UsageInstructions { get; set; } = null!;
        public string Warnings { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string CountryOfOrigin { get; set; } = null!;
        public string ShelfLife { get; set; } = null!;
        //Relational properties
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
