using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.System.Classroom;
using ApplicationCore.System.Holiday;
using Infeastructure.Data;
using Infrastructure.Enum;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Infrastructure.Services
{
    public class ClassroomService : IClassroomService
    {
        private readonly ApplicationDbContext _context;

        public ClassroomService(ApplicationDbContext context)
        {
            _context = context; ;
        }
        public async Task<ApiResponseModel<IEnumerable<ClassroomModel>>> GetList(ClassroomModelRequest request)
        {
            var query = _context.Classroom
                .Include(x=>x.Course)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchText))
            {
                query = query.Where(x =>
                x.Name.Contains(request.SearchText)
                );

            }

            var data = await query.Where(x=>!x.IsDelete && !x.Course.IsDelete).Select(x => new ClassroomModel()
            {
                Name = x.Name,
                Depscription = x.Depscription,
                SchoolYear = x.SchoolYear,
                CourseId = x.CourseId,
                AmountStudent = x.AmountStudent,
                Tuition = x.Tuition,
                Avatar = x.Avatar,
                TuitionFee = x.TuitionFee,
                CreatedBy = x.CreatedBy,
                UpdatedBy = x.UpdatedBy
            }).ToListAsync();

            return new ApiResponseModel<IEnumerable<ClassroomModel>>
            {
                Data = data,
                Status = StatusResponse.SUCCESS,
                Errors = null
            };
        }

        public async Task<ApiResponseModel<ClassroomModel>> GetbyId(Guid id)
        {
            var entity = await _context.Classroom.Where(x => x.Id == id && !x.IsDelete).FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new CourseException("Not Found");
            }

            var model = new ClassroomModel
            {
                Name = entity.Name,
                Depscription = entity.Depscription,
                SchoolYear = entity.SchoolYear,
                CourseId = entity.CourseId,
                AmountStudent = entity.AmountStudent,
                Tuition = entity.Tuition,
                Avatar = entity.Avatar,
                TuitionFee = entity.TuitionFee,
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy
            };


            return new ApiResponseModel<ClassroomModel>
            {
                Data = model,
                Status = StatusResponse.SUCCESS,
                Errors = null
            };
        }

        public async Task<bool> Create(ClassroomModel model)
        {
            try
            {
                var newClassroom = new Classroom()
                {
                    Name = model.Name,
                    Depscription = model.Depscription,
                    SchoolYear = model.SchoolYear,
                    CourseId = model.CourseId,
                    AmountStudent = model.AmountStudent,
                    Tuition = model.Tuition,
                    Avatar = model.Avatar,
                    TuitionFee = model.TuitionFee,
                    CreatedBy = model.CreatedBy,
                    UpdatedBy = model.UpdatedBy
                };

                if (newClassroom != null)
                {
                    _context.Classroom.Add(newClassroom);
                    await _context.SaveChangesAsync();
                }

                return true;
            }catch(Exception ex)
            {
                throw new CourseException(ex.ToString());
            }
        }

        public async Task<bool> Update(ClassroomModel model, Guid id)
        {
            try
            {
                if (model == null)
                {
                    throw new CourseException("Update is unsuccess!");
                }

                var entity = await _context.Classroom.Where(x => x.Id == id && !x.IsDelete).FirstOrDefaultAsync();
                if (entity != null)
                {
                    entity.Name = model.Name;
                    entity.Depscription = model.Depscription;
                    entity.UpdatedDate = DateTime.Now;
                    entity.SchoolYear = model.SchoolYear;
                    entity.CourseId = model.CourseId;
                    entity.AmountStudent = model.AmountStudent;
                    entity.Tuition = model.Tuition;
                    entity.Avatar = model.Avatar;
                    entity.TuitionFee = model.TuitionFee;
                    entity.CreatedBy = model.CreatedBy;
                    entity.UpdatedBy = model.UpdatedBy;
                    _context.Classroom.Update(entity);
                }

                await _context.SaveChangesAsync();

                return true;
            }catch(Exception ex)
            {
                throw new CourseException(ex.ToString());
            }
        }

        public async Task<bool> Delete(Guid Id)
        {
            try {
                var item = await _context.Classroom
                .Where(x => x.Id == Id)
                .FirstOrDefaultAsync();

                if (item != null)
                {
                    item.IsDelete = true;
                    _context.Classroom.Update(item);
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
