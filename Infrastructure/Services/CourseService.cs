using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.System.Course;
using ApplicationCore.System.Holiday;
using Infeastructure.Data;
using Infrastructure.Enum;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Infrastructure.Services
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _context;

        public CourseService(ApplicationDbContext context)
        {
            _context = context; ;
        }

        public async Task<ApiResponseModel<IEnumerable<CourseModel>>> GetList(CourseModelRequest request)
        {
            var query = _context.Course.AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchText))
            {
                query = query.Where(x =>x.Name.Contains(request.SearchText));
            }


            var data = await query.Where(x=>!x.IsDelete).Select(x => new CourseModel()
            {
                Name = x.Name,
                Price = x.Price,
                StatusTuition = x.StatusTuition,
                CreatedBy = x.CreatedBy,
                UpdatedBy = x.UpdatedBy
            }).ToListAsync();

            return new ApiResponseModel<IEnumerable<CourseModel>>
            {
                Data = data,
                Status = StatusResponse.SUCCESS,
                Errors = null
            };

        }
        public async Task<bool> Create(CourseModel model)
        {
            var newCourse = new Course()
            {
                Name = model.Name,
                Price = model.Price,
                StatusTuition = model.StatusTuition,
                CreatedBy = model.CreatedBy,
                UpdatedBy = model.UpdatedBy
            };

            if (newCourse != null)
            {
                _context.Course.Add(newCourse);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<ApiResponseModel<CourseModel>> GetbyId(Guid id)
        {
            try
            {
                var entity = await _context.Course.Where(x => x.Id == id && !x.IsDelete).FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new CourseException("Not Found");
                }

                var model = new CourseModel
                {
                    Name = entity.Name,
                    Price = entity.Price,
                    StatusTuition = entity.StatusTuition,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy
                };


                return new ApiResponseModel<CourseModel>
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

        public async Task<bool> Update(CourseModel model, Guid id)
        {
            try
            {
                if (model == null)
                {
                    throw new CourseException("Update is unsuccess!");
                }

                var entity = await _context.Course.Where(x => x.Id == id && !x.IsDelete).FirstOrDefaultAsync();
                if (entity != null)
                {
                    entity.UpdatedBy = model.UpdatedBy;
                    entity.Name = model.Name;
                    entity.Price = model.Price;
                    entity.StatusTuition = model.StatusTuition;
                    entity.CreatedBy = model.CreatedBy;
                    entity.UpdatedBy = model.UpdatedBy;
                    _context.Course.Update(entity);
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
                var item = await _context.Course
                .Where(x => x.Id == Id)
                .FirstOrDefaultAsync();

                if (item != null)
                {
                    item.IsDelete = true;
                    _context.Course.Update(item);
                }

                await _context.SaveChangesAsync();
                return true;
            }catch (Exception ex)
            {
                throw new CourseException(ex.ToString());
            }
        }
    }
}
