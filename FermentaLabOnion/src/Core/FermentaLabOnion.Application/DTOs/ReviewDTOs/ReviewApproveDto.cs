using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.DTOs.ReviewDTOs
{
    public record ReviewApproveDto
    {
        public bool IsApproved { get; set; }
    }
}
