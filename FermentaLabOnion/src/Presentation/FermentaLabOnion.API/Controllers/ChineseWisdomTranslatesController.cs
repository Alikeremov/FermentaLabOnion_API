using FermentaLabOnion.Application.Abstraction.Services;
using FermentaLabOnion.Application.DTOs.ChineseWisdomTranslateDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FermentaLabOnion.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChineseWisdomTranslatesController : ControllerBase
    {
        private readonly IChineseWisdomTranslateService _service;

        public ChineseWisdomTranslatesController(IChineseWisdomTranslateService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 3)
        {
            return StatusCode(StatusCodes.Status200OK, await _service.GetAllAsync(page, take));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            return StatusCode(StatusCodes.Status200OK, await _service.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] ChineseWisdomTranslateCreateDto chineseWisdomDto)
        {
            await _service.CreateAsync(chineseWisdomDto);
            return StatusCode(StatusCodes.Status200OK);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromForm] ChineseWisdomTranslateUpdateDto updateDto, int id)
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
