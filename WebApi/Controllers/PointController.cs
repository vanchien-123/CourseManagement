using ApplicationCore.System.Point;
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
    public class PointController : ControllerBase
    {
        private readonly IPointService _pointService;

        public PointController(IPointService pointService)
        {
            _pointService = pointService;
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] PointModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newPoint = await _pointService.Create(model);
            if (!newPoint)
            {
                return BadRequest("Create is unsuccessful.");
            }
            return Ok();
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAll([FromQuery] PointModelRequest request)
        {
            var response = await _pointService.GetList(request);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllById(Guid id)
        {
            var response = await _pointService.GetbyId(id);

            if (response == null) return NotFound();

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] PointModel model, Guid id)
        {
            var response = await _pointService.Update(model, id);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var response = await _pointService.Delete(id);

            return Ok(response);
        }
    }
}
