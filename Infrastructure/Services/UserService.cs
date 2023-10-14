using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.System.TypePoint;
using ApplicationCore.System.User;
using Infeastructure.Data;
using Infrastructure.Enum;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly JwtConfiguration _jwtConfiguration;
        private readonly ApplicationDbContext _context;
        private readonly IFileService _fileService;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, JwtConfiguration jwtConfiguration, ApplicationDbContext context,RoleManager<IdentityRole> roleManager, IFileService fileService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _jwtConfiguration = jwtConfiguration;
            _context = context;
            _roleManager = roleManager;
            _fileService = fileService;
        }

        public async Task<bool> Register(ApiRequestRegisterModel registerRequest, string role)
        {
            //check user exits

            var userExits = await _userManager.FindByEmailAsync(registerRequest.Email);
            if(userExits != null)
            {
                throw new CourseException("User already exits!");
            }


            var user = new ApplicationUser()
            {
                Email = registerRequest.Email,
                Name = registerRequest.Name,
                UserName = registerRequest.Email,
                PhoneNumber = registerRequest.PhoneNumber,
                PasswordHash = registerRequest.Password
            };

            if(await _roleManager.RoleExistsAsync(role))
            {
                var result = await _userManager.CreateAsync(user, registerRequest.Password);

                if (!result.Succeeded)
                {
                    throw new CourseException("User created failed");
                }

                await _userManager.AddToRoleAsync(user, role);
                return true;
            }


            
            return false;
        }

        public async Task<ApiResponseModel<IEnumerable<ApiRequestRegisterModel>>> GetList(UserModelRequest request)
        {
            var query = _context.ApplicationUser.AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchText))
            {
                query = query.Where(x =>
                x.Name.Contains(request.SearchText) ||
                x.UserName.Contains(request.SearchText) ||
                x.Email.Contains(request.SearchText) ||
                x.PhoneNumber.Contains(request.SearchText)
                );
            }

            var data = await query.Where(x=>!x.IsDelete && x.ParentID == null).Select(x => new ApiRequestRegisterModel()
            {
                Email = x.Email,
                Name = x.Name,
                Password = x.PasswordHash,
                PhoneNumber = x.PhoneNumber,
            }).ToListAsync();

            return new ApiResponseModel<IEnumerable<ApiRequestRegisterModel>>
            {
                Data = data,
                Status = StatusResponse.SUCCESS,
                Errors = null
            };
        }
        public async Task<string> Authencate(LoginRequest loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.UserName);
            if (user == null)
            {
                throw new CourseException("User or password incorrect, please try again !");
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginRequest.Password, loginRequest.Remember, true);
            if (result.Succeeded)
            {
                throw new CourseException("User or password incorrect, please try again !");
            }

            var userInfo = new UserInfo()
            {
                Email = user.Email,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber
            };

            var roles = await _userManager.GetRolesAsync(user);

            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim("id",""),
                new Claim("name",""),
                new Claim("email",""),
                new Claim("phoneNumber",""),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, string .Join(";", roles)),
            });

            var expiredToken = DateTime.UtcNow.AddMinutes(_jwtConfiguration.TokenValidityInMinutes);
            var secretKey = Encoding.UTF8.GetBytes(_jwtConfiguration.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = claimsIdentity,
                Expires = expiredToken,
                Issuer = _jwtConfiguration.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey),
                SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);

            return stringToken;
        }

        public async Task<ApiResponseModel<ApiRequestRegisterModel>> GetbyId(String id)
        {
            try
            {
                var entity = await _context.ApplicationUser.Where(x => x.Id == id && !x.IsDelete).FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new CourseException("Not Found");
                }

                var model = new ApiRequestRegisterModel
                {
                    Email = entity.Email,
                    Name = entity.Name,
                    Password = entity.PasswordHash,
                    PhoneNumber = entity.PhoneNumber,
                };


                return new ApiResponseModel<ApiRequestRegisterModel>
                {
                    Data = model,
                    Status = StatusResponse.SUCCESS,
                    Errors = null
                };
            }catch(Exception ex)
            {
                throw new CourseException(ex.ToString());
            }
        }

        public async Task<bool> Update(ApiRequestRegisterModel model, String id)
        {
            try
            {
                if (model == null)
                {
                    throw new CourseException("Update is unsuccess!");
                }

                var entity = await _context.ApplicationUser.Where(x => x.Id == id && !x.IsDelete).FirstOrDefaultAsync();
                if (entity != null)
                {
                    entity.Email = model.Email;
                    entity.NormalizedEmail = model.Email;
                    entity.UserName = model.Email;
                    entity.NormalizedUserName = model.Email;
                    entity.Name = model.Name;
                    entity.PasswordHash = model.Password;
                    entity.PhoneNumber = model.PhoneNumber;

                    _context.ApplicationUser.Update(entity);
                }

                await _context.SaveChangesAsync();

                return true;
            }catch(Exception ex)
            {
                throw new CourseException(ex.ToString());
            }
        }
        public async Task<bool> Delete(String Id)
        {
            try
            {
                var item = await _context.ApplicationUser
                .Where(x => x.Id == Id)
                .FirstOrDefaultAsync();

                if (item != null)
                {
                    item.IsDelete = true;
                    _context.ApplicationUser.Update(item);
                }

                await _context.SaveChangesAsync();
                return true;
            }catch(Exception ex)
            {
                throw new CourseException(ex.ToString());
            }

        }
    }
}
