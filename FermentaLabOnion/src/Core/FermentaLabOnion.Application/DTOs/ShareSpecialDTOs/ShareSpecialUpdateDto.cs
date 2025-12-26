using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.DTOs.ShareSpecialDTOs
{
    public record ShareSpecialUpdateDto
    {
        public string Title { get; set; } = null!;
        public string Subtitle { get; set; } = null!;
        public string? ExistImage { get; set; }
        public IFormFile? NewImage { get; set; }
    }
}
