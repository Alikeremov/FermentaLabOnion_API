using FermentaLabOnion.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.DTOs.ChineseWisdomTranslateDTOs
{
    public record ChineseWisdomTranslateUpdateDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int ChineseWisdomId { get; set; }
        public Language Language { get; set; }
    }
}
