using ApplicationCore.Entities;
using ApplicationCore.System.Holiday;
using ApplicationCore.System.Subject;
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
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAll([FromQuery] SubjectModelRequest request)
        {
            var response = await _subjectService.GetList(request);

            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] SubjectModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newSubject = await _subjectService.Create(model);
            if (!newSubject)
            {
                return BadRequest("Create is unsuccessful.");
            }
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllById(Guid id)
        {
            var response = await _subjectService.GetbyId(id);

            if (response == null) return NotFound();

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] SubjectModel model, Guid id)
        {
            var response = await _subjectService.Update(model, id);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var response = await _subjectService.Delete(id);

            return Ok(response);
        }
    }
}
