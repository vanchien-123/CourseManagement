 using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.System.Holiday;
using ApplicationCore.System.TypePoint;
using Infeastructure.Data;
using Infrastructure.Enum;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class TypePointService : ITypePointService
    {
        private readonly ApplicationDbContext _context;

        public TypePointService(ApplicationDbContext context)
        {
            _context = context; ;
        }

        public async Task<ApiResponseModel<IEnumerable<TypePointModel>>> GetList(TypePointModelRequest request)
        {
            var query = _context.TypePoint.AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchText))
            {
                query = query.Where(x => x.Name.Contains(request.SearchText));
            }

            var data = await query.Where(x=>!x.IsDelete).Select(x => new TypePointModel()
            {
                Name = x.Name,
                Coefficient= (int)x.Coefficient,
                CreatedBy = x.CreatedBy,
                UpdatedBy = x.UpdatedBy
            }).ToListAsync();

            return new ApiResponseModel<IEnumerable<TypePointModel>>
            {
                Data = data,
                Status = StatusResponse.SUCCESS,
                Errors = null
            };
        }

        public async Task<ApiResponseModel<TypePointModel>> GetbyId(Guid id)
        {
            try
            {
                var entity = await _context.TypePoint.Where(x => x.Id == id &&  !x.IsDelete).FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new CourseException("Not Found");
                }

                var model = new TypePointModel
                {
                    Name = entity.Name,
                    Coefficient = (int)entity.Coefficient,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy
                };


                return new ApiResponseModel<TypePointModel>
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

        public async Task<bool> Create(TypePointModel typePoint)
        {
            var newTypePoint = new TypePoint()
            {
                Name = typePoint.Name,
                Coefficient = typePoint.Coefficient,
                CreatedBy = typePoint.CreatedBy,
                UpdatedBy = typePoint.UpdatedBy
            };

            if (newTypePoint != null)
            {
                _context.TypePoint.Add(newTypePoint);
                await _context.SaveChangesAsync();
            }

            return true;
        }


        public async Task<bool> Update(TypePointModel model, Guid id)
        {
            try
            {
                if (model == null)
                {
                    throw new CourseException("Update is unsuccess!");
                }

                var entity = await _context.TypePoint.Where(x => x.Id == id && !x.IsDelete).FirstOrDefaultAsync();
                if (entity != null)
                {
                    entity.UpdatedBy = model.UpdatedBy;
                    entity.Name = model.Name;
                    entity.UpdatedDate = DateTime.Now;
                    entity.Coefficient = model.Coefficient;
                    entity.CreatedBy = model.CreatedBy;
                    entity.UpdatedBy = model.UpdatedBy;



                    _context.TypePoint.Update(entity);
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
                var item = await _context.TypePoint
                .Where(x => x.Id == Id)
                .FirstOrDefaultAsync();

                if (item != null)
                {
                    item.IsDelete = true;
                    _context.TypePoint.Update(item);
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
