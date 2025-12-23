using FermentaLabOnion.Application.DTOs.TokenDTOs;
using FermentaLabOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Abstraction.Services
{
    public interface IJwtTokenService
    {
        TokenResponseDto CreateJwtToken(AppUser user, int minutes, List<string> roles);
    }
}
