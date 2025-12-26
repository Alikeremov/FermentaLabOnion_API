using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.DTOs.ShareSpecialDTOs
{
    public record ShareSpecialCreateDto
    {
        public string Title { get; set; } = null!;
        public string Subtitle { get; set; } = null!;
        public IFormFile Image { get; set; } = null!;
    }
}
