using FermentaLabOnion.Application.Abstraction.Services;
using FermentaLabOnion.Application.DTOs.QuestionTranslateDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FermentaLabOnion.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuestionTranslatesController : ControllerBase
    {
        private readonly IQuestionTranslateService _service;

        public QuestionTranslatesController(IQuestionTranslateService service)
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
        public async Task<IActionResult> CreateAsync([FromForm] QuestionTranslateCreateDto questionDto)
        {
            await _service.CreateAsync(questionDto);
            return StatusCode(StatusCodes.Status200OK);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromForm] QuestionTranslateUpdateDto updateDto, int id)
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
