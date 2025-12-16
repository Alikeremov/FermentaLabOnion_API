using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.DTOs.CategoryDTOs
{
    public record CategoryGetDto
    {
        public int Id { get; set; } 
        public string Name { get; set; } = null!;

    }
}
