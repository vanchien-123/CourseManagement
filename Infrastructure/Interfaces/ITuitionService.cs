using ApplicationCore.Entities;
using ApplicationCore.System.Tuition;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface ITuitionService
    {
        Task<ApiResponseModel<IEnumerable<TuitionModel>>> GetList(TuitionModelRequest request);
        Task<bool> Create(TuitionModel model);
        Task<bool> Update(TuitionModel model, Guid id);
        Task<bool> Delete(Guid Id);
        Task<ApiResponseModel<TuitionModel>> GetbyId(Guid Id);
    }
}
