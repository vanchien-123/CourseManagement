using ApplicationCore.Helpers;
using ApplicationCore.Models.Claims;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class PermissionController : ControllerBase
    {
        private readonly IPermission _permission;
        public PermissionController(IPermission permission)
        {
            _permission = permission;
        }

        [HttpGet("{list}")]
        public async Task<ActionResult> GetList([FromBody] string roleId)
        {
            var response = await _permission.GetList(roleId);
            return Ok(response);
        }

        [HttpPut("{roleId}")]
        public async Task<IActionResult> Update([FromBody] PermissionViewModel model, string roleId)
        {
            var response = await _permission.Update(model, roleId);

            return Ok(response);
        }
    }
}
