using FermentaLabOnion.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.DTOs.ProductImageDTOs
{
    public record ProductImageCreateDto
    {
        public IFormFile Image { get; set; } = null!;
        public ImageType ImageType { get; set; }
        public int? ProductId { get; set; }
    }
}
