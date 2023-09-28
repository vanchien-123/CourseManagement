using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.System.Holiday;
using ApplicationCore.System.Tuition;
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

namespace Infrastructure.Services
{
    public class TuitionService : ITuitionService
    {
        private readonly ApplicationDbContext _context;

        public TuitionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponseModel<IEnumerable<TuitionModel>>> GetList(TuitionModelRequest request)
        {
            var query = _context.Tuition
                .Include(x=>x.Student)
                .Include(x=>x.Classroom)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchText))
            {
                query = query.Where(x =>
                x.Student.FirstName.Contains(request.SearchText) ||
                x.Student.LastName.Contains(request.SearchText) ||
                x.Student.Email.Contains(request.SearchText) ||
                x.Student.PhoneNumber.Contains(request.SearchText)
                );
            }

            var data = await query.Where(x=>!x.IsDelete && !x.Student.IsDelete && !x.Classroom.IsDelete).Select(x => new TuitionModel()
            {
                StudentId = x.StudentId,
                ClassroomId = x.ClassroomId,
                TypeTuition = x.TypeTuition,
                FeeLevel = x.FeeLevel,
                Discount = x.Discount,
                Note = x.Note,
                CreatedBy = x.CreatedBy,
                UpdatedBy = x.UpdatedBy
            }).ToListAsync();

            return new ApiResponseModel<IEnumerable<TuitionModel>>
            {
                Data = data,
                Status = StatusResponse.SUCCESS,
                Errors = null
            };
        }

        public async Task<bool> Create(TuitionModel model)
        {
            var newTuition = new Tuition()
            {
                StudentId = model.StudentId,
                ClassroomId = model.ClassroomId,
                TypeTuition = model.TypeTuition,
                FeeLevel = model.FeeLevel,
                Discount = model.Discount,
                Note = model.Note,
                CreatedBy = model.CreatedBy,
                UpdatedBy = model.UpdatedBy
            };

            if (newTuition != null)
            {
                _context.Tuition.Add(newTuition);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<ApiResponseModel<TuitionModel>> GetbyId(Guid id)
        {
            try
            {
                var entity = await _context.Tuition.Where(x => x.Id == id && !x.IsDelete).FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new CourseException("Not Found");
                }

                var model = new TuitionModel
                {
                    StudentId = entity.StudentId,
                    ClassroomId = entity.ClassroomId,
                    TypeTuition = entity.TypeTuition,
                    FeeLevel = entity.FeeLevel,
                    Discount = entity.Discount,
                    Note = entity.Note,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy
                };


                return new ApiResponseModel<TuitionModel>
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

        public async Task<bool> Update(TuitionModel model, Guid id)
        {
            try
            {
                if (model == null)
                {
                    throw new CourseException("Update is unsuccess!");
                }

                var entity = await _context.Tuition.Where(x => x.Id == id && !x.IsDelete).FirstOrDefaultAsync();
                if (entity != null)
                {
                    entity.UpdatedDate = DateTime.Now;
                    entity.StudentId = model.StudentId;
                    entity.ClassroomId = model.ClassroomId;
                    entity.TypeTuition = model.TypeTuition;
                    entity.FeeLevel = model.FeeLevel;
                    entity.Discount = model.Discount;
                    entity.Note = model.Note;
                    entity.CreatedBy = model.CreatedBy;
                    entity.UpdatedBy = model.UpdatedBy;
                    _context.Tuition.Update(entity);
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
            try
            {
                var item = await _context.Tuition
                .Where(x => x.Id == Id)
                .FirstOrDefaultAsync();

                if (item != null)
                {
                    item.IsDelete = true;
                    _context.Tuition.Update(item);
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
