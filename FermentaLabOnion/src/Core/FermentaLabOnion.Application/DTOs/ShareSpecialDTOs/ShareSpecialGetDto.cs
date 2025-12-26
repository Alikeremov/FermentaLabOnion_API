using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.DTOs.ShareSpecialDTOs
{
    public record ShareSpecialGetDto
    {
        public int Id { get; set; }
        public string Tittle { get; set; } = null!;
        public string Subtittle { get; set; } = null!;
        public string Image { get; set; } = null!;
    }
}
