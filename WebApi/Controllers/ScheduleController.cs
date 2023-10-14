using ApplicationCore.System.Holiday;
using ApplicationCore.System.Schedule;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [Authorize(Roles = "Admin, Instructor")]
        [HttpGet("list")]
        public async Task<IActionResult> GetAll([FromQuery] ScheduleModelRequest request)
        {
            var response = await _scheduleService.GetList(request);

            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ScheduleModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newSchedule = await _scheduleService.Create(model);
            if (!newSchedule)
            {
                return BadRequest("Create is unsuccessful.");
            }
            return Ok();
        }
        
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllById(Guid id)
        {
            var response = await _scheduleService.GetbyId(id);

            if (response == null) return NotFound();

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] ScheduleModel model, Guid id)
        {
            var response = await _scheduleService.Update(model, id);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var response = await _scheduleService.Delete(id);

            return Ok(response);
        }
    }
}
