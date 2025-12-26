using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.DTOs.InformationDTOs
{
    public record InformationUpdateDto
    {
        public string Title { get; set; } = null!;
        public string? ExistImage { get; set; } 
        public IFormFile? NewImage { get; set; }

    }
}
