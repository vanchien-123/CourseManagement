using ApplicationCore.System.Course;
using ApplicationCore.System.Holiday;
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
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }


        [HttpGet("list")]
        public async Task<IActionResult> GetAll([FromQuery] CourseModelRequest request)
        {
            var response = await _courseService.GetList(request);

            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CourseModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newCourse = await _courseService.Create(model); 
            if (!newCourse)
            {
                return BadRequest("Create is unsuccessful.");
            }
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllById(Guid id)
        {
            var response = await _courseService.GetbyId(id);

            if (response == null) return NotFound();

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] CourseModel model, Guid id)
        {
            var response = await _courseService.Update(model, id);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var response = await _courseService.Delete(id);

            return Ok(response);
        }
    }
}
