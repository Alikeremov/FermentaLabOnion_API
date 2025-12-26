using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.DTOs.InformationDTOs
{
    public record InformationCreateDto
    {
        public string Title { get; set; } = null!;
        public IFormFile Image { get; set; } = null!;
    }
}
