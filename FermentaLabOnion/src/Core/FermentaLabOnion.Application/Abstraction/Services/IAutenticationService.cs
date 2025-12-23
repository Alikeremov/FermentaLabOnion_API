using FermentaLabOnion.Application.DTOs.AutenticationDTOs;
using FermentaLabOnion.Application.DTOs.TokenDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Abstraction.Services
{
    public interface IAutenticationService
    {
        Task Register(RegisterDto registerDto);
        Task<TokenResponseDto> Login(LoginDto loginDto);
        bool IsUserCurrent();
        Task<AppUserGetDto> GetCurrentUserAsync();
        Task<string> GetUserRoleAsync(string id);
        Task<TokenResponseDto> Refresh();
        Task Logout();
        Task CreateRoleAsync();
        Task ForgotPassword(FogotPasswordDto dto);
        Task ResetPassword(ResetPasswordDto dto);
    }
}
