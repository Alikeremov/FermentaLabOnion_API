using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.DTOs.ReviewDTOs
{
    public record ReviewCreateDto
    {
        public int ProductId { get; set; }
        public string Description { get; set; } = null!;
        public int Rate { get; set; }
    }
}
