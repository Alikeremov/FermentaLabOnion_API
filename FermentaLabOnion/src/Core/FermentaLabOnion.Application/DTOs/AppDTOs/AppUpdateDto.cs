using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.DTOs.AppDTOs
{
    public record AppUpdateDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsMain { get; set; }
        public string? ExistImage { get; set; }
        public IFormFile? NewImage { get; set; }
    }
}
