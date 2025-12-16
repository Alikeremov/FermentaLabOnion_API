using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.DTOs.TagDTOs
{
    public record TagGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
