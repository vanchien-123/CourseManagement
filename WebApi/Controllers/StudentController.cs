using ApplicationCore.System.Holiday;
using ApplicationCore.System.Student;
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
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAll([FromQuery] StudentModelRequest request)
        {
            var response = await _studentService.GetList(request);

            return Ok(response);
        }



        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromQuery] StudentModel model, string role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newStudent = await _studentService.Create(model, role);
            if (!newStudent)
            {
                return BadRequest("Create is unsuccessful.");
            }
            return Ok();
        }

        

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllById(Guid id)
        {
            var response = await _studentService.GetbyId(id);

            if (response == null) return NotFound();

            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync([FromForm] StudentModel model, Guid id)
        {
            var response = await _studentService.Update(model, id);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var response = await _studentService.Delete(id);
            
            return Ok(response);
        }
    }
}
