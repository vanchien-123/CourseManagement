using ApplicationCore.System.Combination;
using ApplicationCore.System.TypePoint;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CombinationController : ControllerBase
    {
        private readonly ICombinationService _combinationService;

        public CombinationController(ICombinationService combinationService)
        {
            _combinationService = combinationService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAll([FromQuery] CombinationModelRequest request)
        {
            var response = await _combinationService.GetList(request);

            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CombinationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newCombination = await _combinationService.Create(model);
            if (!newCombination)
            {
                return BadRequest("Create is unsuccessful.");
            }
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllById(Guid id)
        {
            var response = await _combinationService.GetbyId(id);

            if (response == null) return NotFound();

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] CombinationModel model, Guid id)
        {
            var response = await _combinationService.Update(model, id);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var response = await _combinationService.Delete(id);

            return Ok(response);
        }
    }
}
