using ApplicationCore.Entities;
using ApplicationCore.System.User;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserSecvice _userSecvice;
        public UsersController(IUserSecvice userSecvice)
        {
            _userSecvice = userSecvice;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest loginRequest)
        {
            if(ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultToken = await _userSecvice.Authencate(loginRequest);
            if (string.IsNullOrEmpty(resultToken))
            {
                return BadRequest("Username or password is incorrect.");
            }
            return Ok(new { token = resultToken });
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] ApplicationUser requestRequest)
        {
            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userSecvice.Register(requestRequest);

            if (!result)
            {
                return BadRequest("Register is unsuccessful.");
            }
            return Ok();
        }
    }
}
