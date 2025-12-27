using FermentaLabOnion.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Domain.Entities
{
    public class Product:BaseEntityNameable
    {
        public string Slug { get; set; } = null!;
        public string ShortDescription { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public string SKU { get; set; } = null!;
        public int Stock { get; set; }
        public bool IsInStock { get; set; }
        public string QuantityPerPackage { get; set; }=null!;
        public string Volume { get; set; } = null!;
        public string PackageType { get; set; } = null!;
        public string Ingredients { get; set; }=null !;
        public string Benefits { get; set; } = null!;
        public string UsageInstructions { get; set; } = null!;
        public string Warnings { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string CountryOfOrigin { get; set; } = null!;
        public string ShelfLife { get; set; } = null!;
        //Relational properties
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<ProductTranslate>? ProductTranslates { get; set; }
        public ICollection<ProductTag>? ProductTags { get; set; }
        public ICollection<ProductImage>? ProductImages { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}
