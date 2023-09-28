using ApplicationCore.Entities;
using ApplicationCore.System.Schedule;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IScheduleService
    {
        Task<ApiResponseModel<IEnumerable<ScheduleModel>>> GetList( ScheduleModelRequest request);
        Task<bool> Create(ScheduleModel model);
        Task<bool> Update(ScheduleModel model, Guid id);
        Task<bool> Delete(Guid Id);
        Task<ApiResponseModel<ScheduleModel>> GetbyId(Guid Id);
    }
}
