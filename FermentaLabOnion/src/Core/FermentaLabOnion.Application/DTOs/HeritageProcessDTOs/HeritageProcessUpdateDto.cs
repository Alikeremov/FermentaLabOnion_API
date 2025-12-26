using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.DTOs.HeritageProcessDTOs
{
    public record HeritageProcessUpdateDto
    {
        public string Title { get; set; } = null!;

        public string BeforeLabel { get; set; } = null!;
        public string AfterLabel { get; set; } = null!;

        public int Order { get; set; }
        public IFormFile? NewBeforeImage { get; set; }
        public IFormFile? NewAfterImage { get; set; }
        public string? ExistBeforeImageUrl { get; set; }
        public string? ExistAfterImageUrl { get; set; } 
    }
}
