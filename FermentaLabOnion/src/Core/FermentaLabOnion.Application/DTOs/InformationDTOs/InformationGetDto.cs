using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.DTOs.InformationDTOs
{
    public record InformationGetDto
    {
        public int Id { get; set; }
        public string Tittle { get; set; } = null!;
        public string Image { get; set; } = null!;
    }
}
