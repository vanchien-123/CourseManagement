using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Models.Email;
using ApplicationCore.System.User;
using Infrastructure.Enum;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(IUserService userService, IConfiguration configuration, IEmailService emailService, UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _configuration = configuration;
            _emailService = emailService;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("list")]
        public async Task<IActionResult> GetAll([FromQuery] UserModelRequest request)
        {
            var response = await _userService.GetList(request);

            return Ok(response);
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultToken = await _userService.Authencate(loginRequest);

            return Ok(resultToken);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] ApiRequestRegisterModel requestRequest, string role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.Register(requestRequest, role);

            if (!result)
            {
                return BadRequest("Register is unsuccessful.");
            }
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllById(string id)
        {
            var response = await _userService.GetbyId(id);

            if (response == null) return NotFound();

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] ApiRequestRegisterModel model, string id, string role)
        {
            var response = await _userService.Update(model, id, role);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var response = await _userService.Delete(id);

            return Ok(response);
        }

        [HttpGet]
        public IActionResult TestMail()
        {
            var message = new Message(
                new string[] { "vanchien0830@gmail.com" }, "Send Mail", "<h1>Thanks very much!!</h1>"
                );

            _emailService.SendEmail(message);

            return StatusCode(StatusCodes.Status200OK);
            //new Response { Status = "Success", Message = "Email Sent Successfully" }
        }

        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<EmailResponseModel<IActionResult>> ForgotPassword([Required] string email)
        {
            var user = await _userManager.FindByNameAsync(email);

            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

                //var forgotPasswordLink =  Url.Action(nameof(ResetPassword), "Authencation", new { token, email = user.Email }, Request.Scheme);

                var forgotPasswordLink = "Link = " + "https://localhost:7255/api/Users/reset-password" + " Email: " + user.Email + " Token: " + token;

                var message = new Message(new string[] { user.Email! }, "Forgot Password Link", forgotPasswordLink!);
                await _emailService.SendEmail(message);

                return new EmailResponseModel<IActionResult>
                {
                    Status = StatusResponse.SUCCESS,
                    Message = $"Password Changed request is sent on Email {user.Email}. Please open your email &  click the link"
                };

            }
            return new EmailResponseModel<IActionResult>
            {
                Status = StatusResponse.FAIL,
                Message = $"Couldnot send link to email, pleale try again"
            };
        }

        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword resetPassword)
        {
            var user = await _userManager.FindByNameAsync(resetPassword.Email);

            if (user != null)
            {
                Byte[] tokenDecodeByte = WebEncoders.Base64UrlDecode(resetPassword.TokenEncoding);
                string tokenString = Encoding.UTF8.GetString(tokenDecodeByte);

                var resetPassResult = await _userManager.ResetPasswordAsync(user, tokenString, resetPassword.Password);

                if (!resetPassResult.Succeeded)
                {
                    foreach (var error in resetPassResult.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return Ok(ModelState);
                }

                return StatusCode(StatusCodes.Status200OK);
                //new Response { Status = "Success", Message = $"Password Changed request is sent on Email {user.Email}. Please open your email &  click the link" });

            }
            return StatusCode(StatusCodes.Status400BadRequest);
            //new Response { Status = "Error", Message = $"Couldnot send link to email, pleale try again});
        }
    }
}