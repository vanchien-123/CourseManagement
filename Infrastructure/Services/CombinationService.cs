using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.System.Combination;
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

namespace Infrastructure.Services
{
    public class CombinationService : ICombinationService
    {
        private readonly ApplicationDbContext _context;

        public CombinationService(ApplicationDbContext context) { 
            _context= context;  
        }

        public async Task<ApiResponseModel<IEnumerable<CombinationModel>>> GetList(CombinationModelRequest request)
        {
            var query = _context.Combination.AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchText))
            {
                query = query.Where(x =>x.Name.Contains(request.SearchText));
            }

            var data = await query.Where(x=>!x.IsDelete).Select(x => new CombinationModel()
            {
                Name = x.Name,
                CreatedBy = x.CreatedBy,
                UpdatedBy = x.UpdatedBy
            }).ToListAsync();

            return new ApiResponseModel<IEnumerable<CombinationModel>>
            {
                Data = data,
                Status = StatusResponse.SUCCESS,
                Errors = null
            };
        }

        public async Task<bool> Create(CombinationModel model)
        {
            var newCombination = new Combination()
            {
                Name = model.Name,
                CreatedBy = model.CreatedBy,
                UpdatedBy = model.UpdatedBy
            };

            if (newCombination != null)
            {
                _context.Combination.Add(newCombination);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<ApiResponseModel<CombinationModel>> GetbyId(Guid id)
        {
            try
            {
                var entity = await _context.Combination.Where(x => x.Id == id && !x.IsDelete).FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new CourseException("Not Found");
                }

                var model = new CombinationModel
                {
                    Name = entity.Name,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy
                };


                return new ApiResponseModel<CombinationModel>
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

        public async Task<bool> Update(CombinationModel model, Guid id)
        {
            try
            {
                if (model == null)
                {
                    throw new CourseException("Update is unsuccess!");
                }

                var entity = await _context.Combination.Where(x => x.Id == id && !x.IsDelete).FirstOrDefaultAsync();
                if (entity != null)
                {
                    entity.CreatedBy = model.CreatedBy;
                    entity.UpdatedBy = model.UpdatedBy;
                    entity.UpdatedDate = DateTime.Now;
                    entity.Name = model.Name;
                    _context.Combination.Update(entity);
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
                var item = await _context.Combination
                .Where(x => x.Id == Id)
                .FirstOrDefaultAsync();

                if (item != null)
                {
                    item.IsDelete = true;
                    _context.Combination.Update(item);
                }

                await _context.SaveChangesAsync();
                return true;
            } catch (Exception ex)
            {
                throw new CourseException(ex.ToString());
            }
        }
    }
}
