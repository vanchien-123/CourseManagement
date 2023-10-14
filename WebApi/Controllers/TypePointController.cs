using ApplicationCore.Entities;
using ApplicationCore.System.TypePoint;
using ApplicationCore.System.User;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, Instructor")]
    public class TypePointController : ControllerBase
    {
        private readonly ITypePointService _typePointService;

        public TypePointController(ITypePointService typePointService)
        {
            _typePointService = typePointService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAll([FromQuery] TypePointModelRequest request)
        {
            var response = await _typePointService.GetList(request);

            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] TypePointModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newTypePoint = await _typePointService.Create(model);
            if (!newTypePoint)
            {
                return BadRequest("Create is unsuccessful.");
            }
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllById(Guid id)
        {
            var response = await _typePointService.GetbyId(id);

            if (response == null) return NotFound();

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] TypePointModel model, Guid id)
        {
            var response = await _typePointService.Update(model, id);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var response = await _typePointService.Delete(id);

            return Ok(response);
        }
    }
}
