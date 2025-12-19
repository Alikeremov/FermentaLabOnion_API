using FermentaLabOnion.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.DTOs.ProductTranslateDTOs
{
    public record ProductTranslateGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
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
        public int? ProductId { get; set; }
        public Language Language { get; set; }

    }
}
