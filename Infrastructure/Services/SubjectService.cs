using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.System.Holiday;
using ApplicationCore.System.Subject;
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
    public class SubjectService : ISubjectService
    {
        private readonly ApplicationDbContext _context;

        public SubjectService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponseModel<IEnumerable<SubjectModel>>> GetList(SubjectModelRequest request)
        {
            var query = _context.Subject.Where(x => !x.IsDelete)
                .Include(x => x.Combination)
                .Include(x => x.Course)
                .Include(x => x.Instructor)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchText))
            {
                query = query.Where(x => x.Name.Contains(request.SearchText)
                );
            }

            if (request.CourseId.HasValue)
            {
                query = query.Where(x => (x.CourseId.HasValue && x.Combination.Name.Equals(request.CourseId)));
            }

            if (request.CambinationId.HasValue)
            {
                query = query.Where(x => (x.CombinationId.HasValue && x.Combination.Name.Equals(request.CambinationId)));
            }

            var data = await query.Select(x => new SubjectModel()
            {
                Name = x.Name,
                CombinationId = x.CombinationId,
                //Combination = x.Combination.Name,
                CourseId = x.CourseId,
                InstructorId = x.InstructorId,
                CreatedBy = x.CreatedBy,
                UpdatedBy = x.UpdatedBy
            }).ToListAsync();

            return new ApiResponseModel<IEnumerable<SubjectModel>>
            {
                Data = data,
                Status = StatusResponse.SUCCESS,
                Errors = null
            };
        }

        public async Task<bool> Create(SubjectModel model)
        {
            var newSubject = new Subject()
            {
                Name = model.Name,
                CombinationId = model.CombinationId,
                CourseId = model.CourseId,
                InstructorId = model.InstructorId,
                CreatedBy = model.CreatedBy,
                UpdatedBy = model.UpdatedBy
            };

            if (newSubject != null)
            {
                _context.Subject.Add(newSubject);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<ApiResponseModel<SubjectModel>> GetbyId(Guid id)
        {
            try
            {
                var entity = await _context.Subject.Where(x => x.Id == id && !x.IsDelete).FirstOrDefaultAsync();
                if (entity == null)
                {
                    throw new CourseException("Not Found");
                }
                var model = new SubjectModel
                {
                    Name = entity.Name,
                    CombinationId = entity.CombinationId,
                    CourseId = entity.CourseId,
                    InstructorId = entity.InstructorId,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy
                };


                return new ApiResponseModel<SubjectModel>
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

        public async Task<bool> Update(SubjectModel model, Guid id)
        {
            try
            {
                if (model == null)
                {
                    throw new CourseException("Update is unsuccess!");
                }

                var entity = await _context.Subject.Where(x => x.Id == id && !x.IsDelete).FirstOrDefaultAsync();
                if (entity != null)
                {
                    entity.UpdatedDate = DateTime.Now;
                    entity.Name = model.Name;
                    entity.CombinationId = model.CombinationId;
                    entity.CourseId = model.CourseId;
                    entity.InstructorId = model.InstructorId;
                    entity.CreatedBy = model.CreatedBy;
                    entity.UpdatedBy = model.UpdatedBy;
                    _context.Subject.Update(entity);
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
                var item = await _context.Subject
                .Where(x => x.Id == Id)
                .FirstOrDefaultAsync();

                if (item != null)
                {
                    item.IsDelete = true;
                    _context.Subject.Update(item);
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
