using ApplicationCore.System.Holiday;
using ApplicationCore.System.Tuition;
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
    public class TuitionController : ControllerBase
    {
        private readonly ITuitionService _tuitionService;

        public TuitionController(ITuitionService tuitionService)
        {
            _tuitionService = tuitionService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAll([FromQuery] TuitionModelRequest request)
        {
            var response = await _tuitionService.GetList(request);

            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] TuitionModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newTuition = await _tuitionService.Create(model);
            if (!newTuition)
            {
                return BadRequest("Create is unsuccessful.");
            }
            return Ok();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllById(Guid id)
        {
            var response = await _tuitionService.GetbyId(id);

            if (response == null) return NotFound();

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] TuitionModel model, Guid id)
        {
            var response = await _tuitionService.Update(model, id);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var response = await _tuitionService.Delete(id);

            return Ok(response);
        }
    }
}
