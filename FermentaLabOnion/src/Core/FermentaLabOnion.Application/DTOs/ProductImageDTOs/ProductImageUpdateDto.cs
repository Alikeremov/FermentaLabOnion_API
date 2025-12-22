using FermentaLabOnion.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.DTOs.ProductImageDTOs
{
    public record ProductImageUpdateDto
    {
        public ImageType ImageType { get; set; }
        public IFormFile? NewImage { get; set; }
        public string? ExistUrl { get; set; } 
        public int? ProductId { get; set; }
    }
}
