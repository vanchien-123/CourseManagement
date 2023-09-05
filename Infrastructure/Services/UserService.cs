using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.System.User;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService : IUserSecvice
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationUser> _roleManager;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationUser> roleManager, IConfiguration configuration) {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager; 
            _configuration = configuration;
        }

        public async Task<string> Authencate(LoginRequest loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.UserName);
            if (user == null)
            {
                throw new  CourseException("Cannot find username");
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginRequest.Password, loginRequest.Remember, true);
            if (result.Succeeded)
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, string.Join(",", roles))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Tokens:Issuer"],
                _configuration["Token:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> Register(ApplicationUser registerRequest)        {
            var user = new ApplicationUser()
            {
                Email = registerRequest.Email,
                Name = registerRequest.Name,
                UserName = registerRequest.UserName,
                PhoneNumber = registerRequest.PhoneNumber,
                PasswordHash = registerRequest.PasswordHash
            };

            var result = await _userManager.CreateAsync(user, registerRequest.PasswordHash);

            if(result.Succeeded)
            {
                return true;
            }
            return false; 

        }
    }
}
