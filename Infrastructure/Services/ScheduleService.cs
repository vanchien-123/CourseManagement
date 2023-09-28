using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.System.Holiday;
using ApplicationCore.System.Schedule;
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
    public class ScheduleService : IScheduleService
    {
        private readonly ApplicationDbContext _context;

        public ScheduleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponseModel<IEnumerable<ScheduleModel>>> GetList(ScheduleModelRequest request)
        {
            var query = _context.Schedule
                .Include(x=>x.Instructor)
                .Include(x=> x.Subject)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchText))
            {
                query = query.Where(x =>
                x.Instructor.FirstName.Contains(request.SearchText) ||
                x.Instructor.LastName.Contains(request.SearchText) ||
                x.Subject.Name.Contains(request.SearchText)
                );
            }

            var data = await query.Where(x=>!x.IsDelete && !x.Instructor.IsDelete && !x.Subject.IsDelete).Select(x => new ScheduleModel()
            {
                SubjectId = x.SubjectId,
                Room = x.Room,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                StartDay = x.StartDay,
                EndDay = x.EndDay,
                Day = x.Day,
                InstructorId = x.InstructorId,
                CreatedBy = x.CreatedBy,
                UpdatedBy = x.UpdatedBy
            }).ToListAsync();

            return new ApiResponseModel<IEnumerable<ScheduleModel>>
            {
                Data = data,
                Status = StatusResponse.SUCCESS,
                Errors = null
            };
        }

        public async Task<bool> Create(ScheduleModel model)
        {
            try
            {
                var newSchedule = new Schedule()
                {
                    SubjectId = model.SubjectId,
                    Room = model.Room,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime,
                    StartDay = model.StartDay,
                    EndDay = model.EndDay,
                    Day = model.Day,
                    InstructorId = model.InstructorId,
                    CreatedBy = model.CreatedBy,
                    UpdatedBy = model.UpdatedBy
                };

                if (newSchedule != null)
                {
                    _context.Schedule.Add(newSchedule);
                    await _context.SaveChangesAsync();
                }

                return true;
            } catch (Exception ex)
            {
                throw new CourseException(ex.ToString());   
            }
        }

        public async Task<ApiResponseModel<ScheduleModel>> GetbyId(Guid id)
        {
            try
            {
                var entity = await _context.Schedule.Where(x => x.Id == id && !x.IsDelete).FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new CourseException("Not Found");
                }

                var model = new ScheduleModel
                {
                    SubjectId = entity.SubjectId,
                    Room = entity.Room,
                    StartTime = entity.StartTime,
                    EndTime = entity.EndTime,
                    StartDay = entity.StartDay,
                    EndDay = entity.EndDay,
                    Day = entity.Day,
                    InstructorId = entity.InstructorId,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy
                };


                return new ApiResponseModel<ScheduleModel>
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

        public async Task<bool> Update(ScheduleModel model, Guid id)
        {
            if (model == null)
            {
                throw new CourseException("Update is unsuccess!");
            }

            var entity = await _context.Schedule.Where(x => x.Id == id && !x.IsDelete).FirstOrDefaultAsync();
            if (entity != null)
            {
                entity.UpdatedDate = DateTime.Now;
                entity.SubjectId = model.SubjectId;
                entity.Room = model.Room;
                entity.StartTime = model.StartTime;
                entity.EndTime = model.EndTime;
                entity.StartDay = model.StartDay;
                entity.EndDay = model.EndDay;
                entity.Day = model.Day;
                entity.InstructorId = model.InstructorId;
                entity.CreatedBy = model.CreatedBy;
                entity.UpdatedBy = model.UpdatedBy;
                _context.Schedule.Update(entity);
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Guid Id)
        {
            try
            {
                var item = await _context.Schedule
                .Where(x => x.Id == Id)
                .FirstOrDefaultAsync();

                if (item != null)
                {
                    item.IsDelete = true;
                    _context.Schedule.Update(item);
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
