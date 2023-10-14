using ApplicationCore.System.Holiday;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HolidayController : ControllerBase
    {
        private readonly IHolidayService _holidayService;

        public HolidayController(IHolidayService holidayService)
        {
            _holidayService = holidayService;
        }

        [Authorize(Roles = "Admin, Instructor")]
        [HttpGet("list")]
        public async Task<IActionResult> GetAll([FromQuery] HolidayModelRequest request)
        {
            var response = await _holidayService.GetList(request);

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] HolidayModel holidayModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newHoliday = await _holidayService.Create(holidayModel);
            if (!newHoliday)
            {
                return BadRequest("Create is unsuccessful.");
            }
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllById(Guid id)
        {
            var response = await _holidayService.GetbyId(id);

            if (response == null) return NotFound();

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] HolidayModel model, Guid id)
        {
            var response = await _holidayService.Update(model, id);

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var response = await _holidayService.Delete(id);

            return Ok(response);
        }

    }
}
