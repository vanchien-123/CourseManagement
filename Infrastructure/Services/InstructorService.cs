using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.System.Instructor;
using ApplicationCore.System.User;
using Infeastructure.Data;
using Infrastructure.Enum;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileService _fileService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public InstructorService(ApplicationDbContext context, IFileService fileService, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _fileService = fileService;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<ApiResponseModel<IEnumerable<InstructorModel>>> GetList(InstructorModeRequest request)
        {
            var query = _context.Instructor.AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchText))
            {
                query = query.Where(x =>
                x.FirstName.Contains(request.SearchText) ||
                x.LastName.Contains(request.SearchText) ||
                x.Email.Contains(request.SearchText) ||
                x.PhoneNumber.Contains(request.SearchText)
                );
            }

            var data = await query.Where(x => !x.IsDelete).Select(x => new InstructorModel()
            {
                TaxCode = x.TaxCode,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Address = x.Address,
                DateOfBirth = x.DateOfBirth,
                Gender = x.Gender,
                Email = x.Email,
                Avatar = x.Avatar,
                PhoneNumber = x.PhoneNumber,
                ParttimeSubject = x.ParttimeSubject,
                Password = x.Password,
                CreatedBy = x.CreatedBy,
                UpdatedBy = x.UpdatedBy
            }).ToListAsync();

            return new ApiResponseModel<IEnumerable<InstructorModel>>
            {
                Data = data,
                Status = StatusResponse.SUCCESS,
                Errors = null
            };
        }
        public async Task<bool> Create(InstructorModel model, string role)
        {
            try
            {
                var newInstructor = new Instructor()
                {
                    TaxCode = model.TaxCode,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    DateOfBirth = model.DateOfBirth,
                    Gender = model.Gender,
                    Email = model.Email,
                    //Avatar = model.Avatar,
                    Avatar = model.fileAvatar.FileName,
                    PhoneNumber = model.PhoneNumber,
                    ParttimeSubject = model.ParttimeSubject,
                    Password = model.Password,
                    CreatedBy = model.CreatedBy,
                    UpdatedBy = model.UpdatedBy,
                };

                if (model.fileAvatar != null)
                {
                    await _fileService.SaveImage(model.fileAvatar);

                }


                var user = new ApplicationUser()
                {
                    ParentID = newInstructor.Id,
                    Email = model.Email,
                    Name = model.FirstName + " " + model.LastName,
                    UserName = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    PasswordHash = model.Password,
                    Avatar = model.fileAvatar.FileName
                };

                if (await _roleManager.RoleExistsAsync(role))
                {
                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (!result.Succeeded)
                    {
                        throw new CourseException("User created failed");
                    }

                    await _userManager.AddToRoleAsync(user, role);
                    _context.Instructor.Add(newInstructor);
                    await _context.SaveChangesAsync();
                    return true;
                }

                //_context.Instructor.Add(newInstructor);
                //await _context.SaveChangesAsync();

                return false;
            }
            catch (Exception ex)
            {
                throw new CourseException(ex.ToString());
            }
        }

        public async Task<ApiResponseModel<InstructorModel>> GetbyId(Guid id)
        {
            try
            {
                var entity = await _context.Instructor.Where(x => x.Id == id && !x.IsDelete).FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new CourseException("Not Found");
                }

                var model = new InstructorModel
                {
                    TaxCode = entity.TaxCode,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    Address = entity.Address,
                    DateOfBirth = entity.DateOfBirth,
                    Gender = entity.Gender,
                    Email = entity.Email,
                    Avatar = entity.Avatar,
                    PhoneNumber = entity.PhoneNumber,
                    ParttimeSubject = entity.ParttimeSubject,
                    Password = entity.Password,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy
                };


                return new ApiResponseModel<InstructorModel>
                {
                    Data = model,
                    Status = StatusResponse.SUCCESS,
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                throw new CourseException(ex.ToString());
            }
        }

        public async Task<bool> Update(InstructorModel model, Guid id)
        {
            try
            {
                if (model == null)
                {
                    throw new CourseException("Update is unsuccess!");
                }

                var entity = await _context.Instructor.Where(x => x.Id == id && !x.IsDelete).FirstOrDefaultAsync();
                if (entity != null)
                {
                    entity.UpdatedDate = DateTime.Now;
                    entity.TaxCode = model.TaxCode;
                    entity.FirstName = model.FirstName;
                    entity.LastName = model.LastName;
                    entity.Address = model.Address;
                    entity.DateOfBirth = model.DateOfBirth;
                    entity.Gender = model.Gender;
                    entity.Email = model.Email;
                    entity.Avatar = model.fileAvatar.FileName;
                    entity.PhoneNumber = model.PhoneNumber;
                    entity.ParttimeSubject = model.ParttimeSubject;
                    entity.Password = model.Password;
                    entity.CreatedBy = model.CreatedBy;
                    entity.UpdatedBy = model.UpdatedBy;

                    if (model.fileAvatar != null)
                    {
                        await _fileService.SaveImage(model.fileAvatar);

                    }

                    _context.Instructor.Update(entity);
                }

                var entityUser = await _context.ApplicationUser.Where(x => x.ParentID == id).FirstOrDefaultAsync();

                if (entityUser != null)
                {
                    entityUser.Email = model.Email;
                    entityUser.NormalizedEmail = model.Email;
                    entityUser.UserName = model.Email;
                    entityUser.NormalizedUserName = model.Email;
                    entityUser.Name = model.FirstName + " " + model.LastName;
                    entityUser.PasswordHash = model.Password;
                    entityUser.PhoneNumber = model.PhoneNumber;

                    _context.ApplicationUser.Update(entityUser);
                }

                await _context.SaveChangesAsync();


                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new CourseException(ex.ToString());
            }

        }

        public async Task<bool> Delete(Guid Id)
        {
            try
            {
                var item = await _context.Instructor
                .Where(x => x.Id == Id)
                .FirstOrDefaultAsync();

                if (item != null)
                {
                    item.IsDelete = true;
                    _context.Instructor.Update(item);
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new CourseException(ex.ToString());
            }
        }

    }
}
