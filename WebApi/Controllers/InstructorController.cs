using ApplicationCore.System.Holiday;
using ApplicationCore.System.Instructor;
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
    [Authorize(Roles ="Admin")]

    public class InstructorController : ControllerBase
    {
        private readonly IInstructorService _instructorService;
        

        public InstructorController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
            
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAll([FromQuery] InstructorModeRequest request)
        {
            var response = await _instructorService.GetList(request);

            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromQuery] InstructorModel model, string role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newInstructor = await _instructorService.Create(model, role);

            if (!newInstructor)
            {
                return BadRequest("Create is unsuccessful.");
            }
            return Ok();
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllById(Guid id)
        {
            var response = await _instructorService.GetbyId(id);

            if (response == null) return NotFound();

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromForm] InstructorModel model, Guid id)
        {
            var response = await _instructorService.Update(model, id);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var response = await _instructorService.Delete(id);

            return Ok(response);
        }
    }
}
