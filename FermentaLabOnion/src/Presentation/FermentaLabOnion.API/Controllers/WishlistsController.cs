using FermentaLabOnion.Application.Abstraction.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FermentaLabOnion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistsController : ControllerBase
    {
        private readonly IWishlistService _service;

        public WishlistsController(IWishlistService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetAsync();
            return Ok(result);
        }

        [HttpPost("{productId:int}")]
        public async Task<IActionResult> Add([FromRoute] int productId)
        {
            await _service.AddAsync(productId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpDelete("{productId:int}")]
        public async Task<IActionResult> Remove([FromRoute] int productId)
        {
            await _service.RemoveAsync(productId);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }

}
