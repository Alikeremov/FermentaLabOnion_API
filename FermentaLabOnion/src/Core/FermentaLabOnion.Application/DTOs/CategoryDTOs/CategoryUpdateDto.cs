using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.DTOs.CategoryDTOs
{
    public record CategoryUpdateDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public IFormFile? NewImage { get; set; }
        public string? ExistImage { get; set; }
    }
}
