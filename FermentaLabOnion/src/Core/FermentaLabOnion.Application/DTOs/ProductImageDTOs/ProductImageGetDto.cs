using FermentaLabOnion.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.DTOs.ProductImageDTOs
{
    public record ProductImageGetDto
    {
        public int Id { get; set; }
        public string Url { get; set; } = null!;
        public ImageType ImageType { get; set; }
        public int? ProductId { get; set; }

    }
}
