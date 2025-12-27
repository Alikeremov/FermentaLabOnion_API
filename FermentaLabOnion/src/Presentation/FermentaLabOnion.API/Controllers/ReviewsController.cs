using FermentaLabOnion.Application.Abstraction.Services;
using FermentaLabOnion.Application.DTOs.ReviewDTOs;
using FermentaLabOnion.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FermentaLabOnion.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _service;

        public ReviewsController(IReviewService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] ReviewCreateDto dto)
        {
            await _service.CreateAsync(dto);
            return Ok();
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
        [HttpGet]
        public async Task<IActionResult> GetByProductAndApproved(int productId,bool IsApproved, int page = 1, int take = 10)
        {
            var data = await _service.GetAllbyApprovedandProduuct(productId, IsApproved, page, take);
            return Ok(data);
        }

        [HttpPut]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Approve(int reviewId, [FromForm] ReviewApproveDto dto)
        {
            await _service.ApproveAsync(reviewId, dto.IsApproved);
            return Ok();
        }

        [HttpDelete]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int reviewId)
        {
            await _service.DeleteAsync(reviewId);
            return Ok();
        }
    }
}
