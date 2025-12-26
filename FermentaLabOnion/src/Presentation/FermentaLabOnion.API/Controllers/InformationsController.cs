using FermentaLabOnion.Application.Abstraction.Services;
using FermentaLabOnion.Application.DTOs.InformationDTOs;
using FermentaLabOnion.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FermentaLabOnion.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InformationsController : ControllerBase
    {
        private readonly IInformationService _service;

        public InformationsController(IInformationService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 3)
        {
            return StatusCode(StatusCodes.Status200OK, await _service.GetAllAsync(page, take));
        }
        [HttpGet]
        public async Task<IActionResult> GetTranslates(Language language, int page = 1, int take = 3)
        {
            return StatusCode(StatusCodes.Status200OK,
                await _service.GetAllTranslatedAsync(page, language: language, take));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            return StatusCode(StatusCodes.Status200OK, await _service.GetAsync(id));
        }
        [HttpGet("{id}/translated")]
        public async Task<IActionResult> Get(int id, Language language)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            return StatusCode(StatusCodes.Status200OK, await _service.GetTranslatedAsync(id, language));
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] InformationCreateDto informationDto)
        {
            await _service.CreateAsync(informationDto);
            return StatusCode(StatusCodes.Status200OK);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromForm] InformationUpdateDto updateDto, int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            await _service.UpdateAsync(updateDto, id);
            return StatusCode(StatusCodes.Status200OK);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            await _service.DeleteAsync(id);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
