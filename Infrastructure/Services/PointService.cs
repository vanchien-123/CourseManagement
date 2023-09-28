using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.System.Holiday;
using ApplicationCore.System.Point;
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
    public class PointService : IPointService
    {
        private readonly ApplicationDbContext _context;
        public PointService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponseModel<IEnumerable<PointModel>>> GetList(PointModelRequest request)
        {
            var query = _context.Point
                .Include(x => x.Course)
                .Include(x => x.Subject)
                .Include(x => x.TypePoint)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchText))
            {
                query = query.Where(x =>
                x.Course.Name.Contains(request.SearchText) ||
                x.Subject.Name.Contains(request.SearchText)
                );
            }
            var data = await query.Where(x => !x.IsDelete && !x.Course.IsDelete && !x.Subject.IsDelete && !x.TypePoint.IsDelete).Select(x => new PointModel()
            {
                CourseId = x.CourseId,
                SubjectId = x.SubjectId,
                TypePointId = x.TypePointId,
                PointCol = x.PointCol,
                PointColRequired = x.PointColRequired,
                CreatedBy = x.CreatedBy,
                UpdatedBy = x.UpdatedBy
            }).ToListAsync();

            return new ApiResponseModel<IEnumerable<PointModel>>
            {
                Data = data,
                Status = StatusResponse.SUCCESS,
                Errors = null
            };
        }

        public async Task<bool> Create(PointModel model)
        {
            var newPoint = new Point()
            {
                CourseId = model.CourseId,
                SubjectId = model.SubjectId,
                TypePointId = model.TypePointId,
                PointCol = model.PointCol,
                PointColRequired = model.PointColRequired,
                CreatedBy = model.CreatedBy,
                UpdatedBy = model.UpdatedBy
            };

            if (newPoint != null)
            {
                _context.Point.Add(newPoint);
                await _context.SaveChangesAsync();
            }

            return true;
        }



        public async Task<ApiResponseModel<PointModel>> GetbyId(Guid id)
        {
            try
            {
                var entity = await _context.Point.Where(x => x.Id == id && !x.IsDelete).FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new CourseException("Not Found");
                }

                var model = new PointModel
                {
                    CourseId = entity.CourseId,
                    SubjectId = entity.SubjectId,
                    TypePointId = entity.TypePointId,
                    PointCol = entity.PointCol,
                    PointColRequired = entity.PointColRequired,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy
                };


                return new ApiResponseModel<PointModel>
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



        public async Task<bool> Update(PointModel model, Guid id)
        {
            try
            {
                if (model == null)
                {
                    throw new CourseException("Update is unsuccess!");
                }

                var entity = await _context.Point.Where(x => x.Id == id && !x.IsDelete).FirstOrDefaultAsync();
                if (entity != null)
                {
                    entity.UpdatedDate = DateTime.Now;
                    entity.CourseId = model.CourseId;
                    entity.SubjectId = model.SubjectId;
                    entity.TypePointId = model.TypePointId;
                    entity.PointCol = model.PointCol;
                    entity.PointColRequired = model.PointColRequired;
                    entity.CreatedBy = model.CreatedBy;
                    entity.UpdatedBy = model.UpdatedBy;
                    _context.Point.Update(entity);
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
                var item = await _context.Point
                .Where(x => x.Id == Id)
                .FirstOrDefaultAsync();

                if (item != null)
                {
                    item.IsDelete = true;
                    _context.Point.Update(item);
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
