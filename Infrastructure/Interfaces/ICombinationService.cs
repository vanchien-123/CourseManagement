using ApplicationCore.Entities;
using ApplicationCore.System.Combination;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface ICombinationService
    {
        Task<ApiResponseModel<IEnumerable<CombinationModel>>> GetList(CombinationModelRequest request);
        Task<bool> Create(CombinationModel model);
        Task<bool> Update(CombinationModel model, Guid id);
        Task<bool> Delete(Guid Id);
        Task<ApiResponseModel<CombinationModel>> GetbyId(Guid Id);
    }
}
