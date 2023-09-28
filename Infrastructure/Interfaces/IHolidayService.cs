using ApplicationCore.Entities;
using ApplicationCore.System.Holiday;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IHolidayService
    {
        Task<ApiResponseModel<IEnumerable<HolidayModel>>> GetList(HolidayModelRequest request);
        Task<bool> Create(HolidayModel model);
        Task<bool> Update(HolidayModel model, Guid id);
        Task<bool> Delete(Guid Id);
        Task<ApiResponseModel<HolidayModel>> GetbyId(Guid Id);
    }
}
