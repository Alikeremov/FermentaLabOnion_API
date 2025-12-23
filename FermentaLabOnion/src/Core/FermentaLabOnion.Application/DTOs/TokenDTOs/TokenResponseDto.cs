using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.DTOs.TokenDTOs
{
    public record TokenResponseDto(
    string Username,
    string AccessToken,
    DateTime ExpiredAt,
    string RefreshToken
);
}
