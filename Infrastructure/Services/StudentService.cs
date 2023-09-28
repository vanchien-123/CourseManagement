using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.System.Holiday;
using ApplicationCore.System.Student;
using Infeastructure.Data;
using Infrastructure.Enum;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileService _fileService;

        public StudentService(ApplicationDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<ApiResponseModel<IEnumerable<StudentModel>>> GetList(StudentModelRequest request)
        {
            var query = _context.Student
                .Include(x => x.Course)
                .Include(x => x.Classroom)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchText))
            {
                query = query.Where(x =>
                x.FirstName.Contains(request.SearchText) ||
                x.LastName.Contains(request.SearchText) ||
                x.Email.Contains(request.SearchText) ||
                x.PhoneNumber.Contains(request.SearchText)
                );
            }

            var data = await query.Where(x => !x.IsDelete && !x.Course.IsDelete && !x.Classroom.IsDelete).Select(x => new StudentModel()
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Gender = x.Gender,
                DateOfBirth = x.DateOfBirth,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Address = x.Address,
                Password = x.Password,
                Avatar = x.Avatar,
                ClassroomId = x.ClassroomId,
                CourseId = x.CourseId,
                CreatedBy = x.CreatedBy,
                UpdatedBy = x.UpdatedBy
            }).ToListAsync();

            return new ApiResponseModel<IEnumerable<StudentModel>>
            {
                Data = data,
                Status = StatusResponse.SUCCESS,
                Errors = null
            };
        }

        public async Task<bool> Create(StudentModel model)
        {
            try
            {
                var newStudent = new Student()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = model.Gender,
                    DateOfBirth = model.DateOfBirth,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address,
                    Password = model.Password,
                    Avatar = model.fileAvatar.FileName,
                    ClassroomId = model.ClassroomId,
                    CourseId = model.CourseId,
                    CreatedBy = model.CreatedBy,
                    UpdatedBy = model.UpdatedBy
                };

                if (model.fileAvatar != null)
                {
                    await _fileService.SaveImage(model.fileAvatar);

                }

                _context.Student.Add(newStudent);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new CourseException(ex.ToString());
            }
        }

        public async Task<ApiResponseModel<StudentModel>> GetbyId(Guid id)
        {
            try
            {
                var entity = await _context.Student.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (entity == null)
                {
                    throw new CourseException("Not Found");
                }

                var model = new StudentModel
                {
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    Gender = entity.Gender,
                    DateOfBirth = entity.DateOfBirth,
                    Email = entity.Email,
                    PhoneNumber = entity.PhoneNumber,
                    Address = entity.Address,
                    Password = entity.Password,
                    Avatar = entity.Avatar,
                    ClassroomId = entity.ClassroomId,
                    CourseId = entity.CourseId,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy
                };


                return new ApiResponseModel<StudentModel>
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

        public async Task<bool> Update(StudentModel model, Guid id)
        {
            try
            {
                if (model == null)
                {
                    throw new CourseException("Update is unsuccess!");
                }

                var entity = await _context.Student.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (entity != null)
                {
                    entity.UpdatedDate = DateTime.Now;
                    entity.FirstName = model.FirstName;
                    entity.LastName = model.LastName;
                    entity.Gender = model.Gender;
                    entity.DateOfBirth = model.DateOfBirth;
                    entity.Email = model.Email;
                    entity.PhoneNumber = model.PhoneNumber;
                    entity.Address = model.Address;
                    entity.Password = model.Password;
                    entity.Avatar = model.fileAvatar.FileName;
                    entity.ClassroomId = model.ClassroomId;
                    entity.CourseId = model.CourseId;
                    entity.CreatedBy = model.CreatedBy;
                    entity.UpdatedBy = model.UpdatedBy;

                    if (model.fileAvatar != null)
                    {
                        await _fileService.SaveImage(model.fileAvatar);

                    }

                    _context.Student.Update(entity);
                }

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
                var item = await _context.Student
                .Where(x => x.Id == Id)
                .FirstOrDefaultAsync();

                if (item != null)
                {
                    item.IsDelete = true;
                    _context.Student.Update(item);
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
