using AutoMapper;
using FermentaLabOnion.Application.Abstraction.Repositories;
using FermentaLabOnion.Application.Abstraction.Services;
using FermentaLabOnion.Application.DTOs.AutenticationDTOs;
using FermentaLabOnion.Application.DTOs.TokenDTOs;
using FermentaLabOnion.Domain.Entities;
using FermentaLabOnion.Domain.Enums;
using FermentaLabOnion.Persistence.Utilites.Exceptions.Autentication;
using FermentaLabOnion.Persistence.Utilites.Exceptions.Common;
using FermentaLabOnion.Persistence.Utilites.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace FermentaLabOnion.Persistence.Implementations.Services
{
    public class AutenticationService : IAutenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IHttpContextAccessor _accessor;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IUrlHelper _urlHelper;
        private readonly IRefreshTokenRepository _rtRepo;

        public AutenticationService(UserManager<AppUser> userManager, IMapper mapper,
           ICloudinaryService cloudinaryService, IJwtTokenService jwtTokenService, IHttpContextAccessor accessor,
           RoleManager<IdentityRole> roleManager, IEmailService emailService, IUrlHelper urlHelper, IRefreshTokenRepository rtRepo)
        {
            _userManager = userManager;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
            _jwtTokenService = jwtTokenService;
            _accessor = accessor;
            _roleManager = roleManager;
            _emailService = emailService;
            _urlHelper = urlHelper;
            _rtRepo=rtRepo;
        }
        public async Task Register(RegisterDto registerDto)
        {
            if (await _userManager.Users
                .AnyAsync(x => x.UserName == registerDto.UserName
                || x.Email == registerDto.Email)) throw new AlreadyExistException("Email or Username have");
            AppUser user = _mapper.Map<AppUser>(registerDto);
            if (registerDto.Image != null)
            {
                registerDto.Image.ValidateImage();
                user.ProfileImage = await _cloudinaryService.FileCreateAsync(registerDto.Image);
            }
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                StringBuilder builder = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    builder.AppendLine(error.Description);
                }
                throw new Exception(builder.ToString());
            }
            await _userManager.AddToRoleAsync(user, Role.User.ToString());
        }
        public async Task<TokenResponseDto> Login(LoginDto loginDto)
        {
            AppUser? user = await _userManager.FindByNameAsync(loginDto.UsernameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(loginDto.UsernameOrEmail);
                if (user == null) throw new LoginException();
            }

            bool ok = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!ok) throw new LoginException();

            int expireMinutes = loginDto.isRemembered ? 4300 : 60;
            List<string> roles = (await _userManager.GetRolesAsync(user)).ToList();

            TokenResponseDto access = _jwtTokenService.CreateJwtToken(user, expireMinutes, roles);

            string refresh = TokenHelper.GenerateRefreshToken();
            int refreshDays = loginDto.isRemembered ? 30 : 14;

            await _rtRepo.AddAsync(new RefreshToken
            {
                TokenHash = TokenHelper.Sha256(refresh),
                AppUserId = user.Id,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(refreshDays),
                RevokedAt = null,
                IsRemembered = loginDto.isRemembered
            });

            await _rtRepo.SaveChangesAsync();

            SetRefreshCookie(refresh, refreshDays);
            return access with { RefreshToken = refresh };
        }
        public bool IsUserCurrent()
        {
            return _accessor.HttpContext?.User.Identity?.IsAuthenticated == true;
        }
        public async Task<AppUserGetDto> GetCurrentUserAsync()
        {
            var id = _accessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id is null)
                throw new UserNotFoundException();

            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                throw new UserNotFoundException();

            var dto = _mapper.Map<AppUserGetDto>(user);

            dto.Role = await GetUserRoleAsync(dto.Id);

            return dto;
        }
        public async Task<string> GetUserRoleAsync(string id)
        {
            var user = await _getUserById(id);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() is null) return "";
            return roles.FirstOrDefault();
        }

        public async Task CreateRoleAsync()
        {
            foreach (var item in Enum.GetValues(typeof(Role)))
            {
                if (!await _roleManager.RoleExistsAsync(item.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = item.ToString() });
                };
            }
        }
        public async Task<TokenResponseDto> Refresh()
        {
            string? refreshToken = GetRefreshFromCookie();
            if (string.IsNullOrWhiteSpace(refreshToken))
                throw new UnauthorizedAccessException();

            string hash = TokenHelper.Sha256(refreshToken);

            RefreshToken? rt = await _rtRepo.GetByHashAsync(hash);
            if (rt == null) throw new UnauthorizedAccessException();
            if (rt.RevokedAt != null) throw new UnauthorizedAccessException();
            if (rt.ExpiresAt <= DateTime.UtcNow) throw new UnauthorizedAccessException();

            _rtRepo.Revoke(rt, DateTime.UtcNow);

            AppUser? user = await _userManager.FindByIdAsync(rt.AppUserId);
            if (user == null) throw new UnauthorizedAccessException();

            List<string> roles = (await _userManager.GetRolesAsync(user)).ToList();

            int expireMinutes = rt.IsRemembered ? 4300 : 60;
            TokenResponseDto access = _jwtTokenService.CreateJwtToken(user, expireMinutes, roles);

            string newRefresh = TokenHelper.GenerateRefreshToken();
            int refreshDays = rt.IsRemembered ? 30 : 14;

            await _rtRepo.AddAsync(new RefreshToken
            {
                TokenHash = TokenHelper.Sha256(newRefresh),
                AppUserId = user.Id,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(refreshDays),
                RevokedAt = null,
                IsRemembered = rt.IsRemembered
            });

            await _rtRepo.SaveChangesAsync();

            SetRefreshCookie(newRefresh, refreshDays);

            return access with { RefreshToken = newRefresh };
        }

        public async Task Logout()
        {
            string? refreshToken = GetRefreshFromCookie();

            if (!string.IsNullOrWhiteSpace(refreshToken))
            {
                string hash = TokenHelper.Sha256(refreshToken);
                RefreshToken? rt = await _rtRepo.GetByHashAsync(hash);

                if (rt != null && rt.RevokedAt == null)
                {
                    _rtRepo.Revoke(rt, DateTime.UtcNow);
                    await _rtRepo.SaveChangesAsync();
                }
            }

            DeleteRefreshCookie();
        }

        private string? GetRefreshFromCookie()
        {
            HttpContext? ctx = _accessor.HttpContext;
            if (ctx == null) return null;

            ctx.Request.Cookies.TryGetValue("refreshToken", out string? token);
            return token;
        }

        private void SetRefreshCookie(string refreshToken, int days)
        {
            HttpContext? ctx = _accessor.HttpContext;
            if (ctx == null) return;

            bool isHttps = ctx.Request.IsHttps;

            ctx.Response.Cookies.Append(
                "refreshToken",
                refreshToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = isHttps, 
                    SameSite = isHttps
                        ? SameSiteMode.None                   
                        : SameSiteMode.Lax,                  
                    Path = "/",
                    Expires = DateTimeOffset.UtcNow.AddDays(days)
                }
            );
        }

        private void DeleteRefreshCookie()
        {
            HttpContext? ctx = _accessor.HttpContext;
            if (ctx == null) return;

            ctx.Response.Cookies.Delete("refreshToken");
        }
        //Burani Yenilemeyi unutma
        public async Task ForgotPassword(FogotPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) return; 

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var encodedToken = WebUtility.UrlEncode(token);

            var resetLink =
                $"https://frontend.com/reset-password?email={dto.Email}&token={encodedToken}";

            await _emailService.SendEmailAsync(
                dto.Email,
                "Şifrəni yenilə",
                resetLink
            );
        }
        public async Task ResetPassword(ResetPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                throw new UserNotFoundException();

            var token = WebUtility.UrlDecode(dto.Token);

            var result = await _userManager.ResetPasswordAsync(
                user,
                token,
                dto.Password
            );

            if (!result.Succeeded)
            {
                var errors = string.Join("\n", result.Errors.Select(e => e.Description));
                throw new Exception(errors);
            }
        }
        private async Task<AppUser> _getUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) throw new UserNotFoundException();
            return user;
        }


    }
}
