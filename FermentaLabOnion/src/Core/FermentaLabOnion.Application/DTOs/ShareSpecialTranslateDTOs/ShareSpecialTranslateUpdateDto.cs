using FermentaLabOnion.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.DTOs.ShareSpecialTranslateDTOs
{
    public record ShareSpecialTranslateUpdateDto
    {
        public string Tittle { get; set; } = null!;
        public string Subtittle { get; set; } = null!;
        public int ShareSpecialId { get; set; }
        public Language Language { get; set; }
    }
}
