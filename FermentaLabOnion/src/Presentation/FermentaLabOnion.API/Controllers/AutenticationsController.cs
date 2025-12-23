using FermentaLabOnion.Application.Abstraction.Services;
using FermentaLabOnion.Application.DTOs.AutenticationDTOs;
using FermentaLabOnion.Application.DTOs.TokenDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FermentaLabOnion.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AutenticationsController : ControllerBase
    {
        private readonly IAutenticationService _service;

        public AutenticationsController(IAutenticationService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            return StatusCode(StatusCodes.Status200OK, await _service.GetCurrentUserAsync());
        }
        
        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterDto registerDto)
        {
            await _service.Register(registerDto);
            return StatusCode(StatusCodes.Status204NoContent);
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginDto loginDto)
        {
            return StatusCode(StatusCodes.Status200OK, await _service.Login(loginDto));
        }
        [HttpGet]
        public async Task<IActionResult> CreateRole()
        {
            await _service.CreateRoleAsync();
            return StatusCode(StatusCodes.Status204NoContent);
        }
        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromForm]FogotPasswordDto dto)
        {
            await _service.ForgotPassword(dto);
            return Ok();
        }
        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromForm]ResetPasswordDto dto)
        {
            await _service.ResetPassword(dto);
            return Ok();
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            TokenResponseDto result = await _service.Refresh();
            return Ok(result);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _service.Logout();
            return Ok();
        }
    }
}
