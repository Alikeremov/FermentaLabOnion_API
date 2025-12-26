using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.DTOs.HeritageProcessDTOs
{
    public record HeritageProcessCreateDto
    {
        public string Title { get; set; } = null!;
        public string BeforeLabel { get; set; } = null!;
        public string AfterLabel { get; set; } = null!;
        public int Order { get; set; }
        public IFormFile BeforeImageUrl { get; set; } = null!;
        public IFormFile AfterImageUrl { get; set; } = null!;
    }
}
