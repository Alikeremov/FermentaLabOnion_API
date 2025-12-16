using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.DTOs.ProductImageDTOs
{
    public record ProductImageUpdateDto
    {
        public string Url { get; set; } = null!;
        public bool IsPrimary { get; set; }
        public int? ProductId { get; set; }
    }
}
