using ApplicationCore.Entities;
using ApplicationCore.System.Holiday;
using ApplicationCore.Exceptions;
using Infeastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Models;
using System.Xml.Linq;
using Infrastructure.Enum;

namespace Infrastructure.Services
{
    public class HolidayService : IHolidayService
    {
        private readonly ApplicationDbContext _context;

        public HolidayService(ApplicationDbContext context)
        {
            _context = context; ;
        }
        public async Task<ApiResponseModel<IEnumerable<HolidayModel>>> GetList(HolidayModelRequest request)
        {
            var query = _context.Holiday.AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchText))
            {
                query = query.Where(x =>
                x.Name.Contains(request.SearchText) ||
                x.Reason.Contains(request.SearchText));
            }

            var data = await query.Where(x=>!x.IsDelete).Select(x => new HolidayModel()
            {
                Name = x.Name,
                Reason = x.Reason,
                CreatedBy = x.CreatedBy,
                LastDate = x.LastDate,
                StartDate = x.StartDate,
                UpdatedBy = x.UpdatedBy
            }).ToListAsync();

            return new ApiResponseModel<IEnumerable<HolidayModel>>
            {
                Data = data,
                Status = StatusResponse.SUCCESS,
                Errors = null
            };
        }

        public async Task<bool> Create(HolidayModel model)
        {
            var newHoliday = new Holiday()
            {
                Name = model.Name,
                Reason = model.Reason,
                StartDate = model.StartDate,
                LastDate = model.LastDate,
                CreatedBy = model.CreatedBy,
                UpdatedBy = model.UpdatedBy
            };

            if (newHoliday != null)
            {
                _context.Holiday.Add(newHoliday);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<ApiResponseModel<HolidayModel>> GetbyId(Guid id)
        {
            try
            {
                var entity = await _context.Holiday.Where(x => x.Id == id && !x.IsDelete).FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new CourseException("Not Found");
                }

                var model = new HolidayModel
                {
                    Name = entity.Name,
                    Reason = entity.Reason,
                    CreatedBy = entity.CreatedBy,
                    LastDate = entity.LastDate,
                    StartDate = entity.StartDate,
                    UpdatedBy = entity.UpdatedBy
                };


                return new ApiResponseModel<HolidayModel>
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

        public async Task<bool> Update(HolidayModel model, Guid id)
        {
            try
            {
                if (model == null)
                {
                    throw new CourseException("Update is unsuccess!");
                }

                var entity = await _context.Holiday.Where(x => x.Id == id && !x.IsDelete).FirstOrDefaultAsync();
                if (entity != null)
                {
                    entity.UpdatedDate = DateTime.Now;
                    entity.UpdatedBy = model.UpdatedBy;
                    entity.StartDate = model.StartDate;
                    entity.LastDate = model.LastDate;
                    entity.Name = model.Name;
                    entity.Reason = model.Reason;
                    _context.Holiday.Update(entity);
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
                var item = await _context.Holiday
                .Where(x => x.Id == Id)
                .FirstOrDefaultAsync();

                if (item != null)
                {
                    item.IsDelete = true;
                    _context.Holiday.Update(item);
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
