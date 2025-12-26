using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.DTOs.HeritageProcessDTOs
{
    public record HeritageProcessGetDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string BeforeLabel { get; set; } = null!;
        public string AfterLabel { get; set; } = null!;
        public int Order { get; set; }
        public string BeforeImageUrl { get; set; } = null!;
        public string AfterImageUrl { get; set; } = null!;
    }
}
