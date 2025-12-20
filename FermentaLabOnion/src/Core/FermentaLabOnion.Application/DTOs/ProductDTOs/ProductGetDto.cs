using FermentaLabOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.DTOs.ProductDTOs
{
    public record ProductGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string ShortDescription { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public string SKU { get; set; } = null!;
        public int Stock { get; set; }
        public bool IsInStock { get; set; }
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
        public ICollection<int> TagIds { get; set; } = new List<int>();
        public ICollection<string> Tags { get; set; } = new List<string>();
        public int? CategoryId { get; set; }
    }
}
