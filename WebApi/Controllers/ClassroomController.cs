using ApplicationCore.System.Classroom;
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
    [Authorize("Admin, Instructor")]
    public class ClassroomController : ControllerBase
    {
        private readonly IClassroomService _classroomService;

        public ClassroomController(IClassroomService classroomService)
        {
            _classroomService = classroomService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAll([FromQuery] ClassroomModelRequest request)
        {
            var response = await _classroomService.GetList(request);

            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ClassroomModel mocel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newClassroom = await _classroomService.Create(mocel);
            if (!newClassroom)
            {
                return BadRequest("Create is unsuccessful.");
            }
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllById(Guid id)
        {
            var response = await _classroomService.GetbyId(id);

            if (response == null) return NotFound();

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] ClassroomModel model, Guid id)
        {
            var response = await _classroomService.Update(model, id);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var response = await _classroomService.Delete(id);

            return Ok(response);
        }
    }
}
