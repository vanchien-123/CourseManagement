using ApplicationCore.Entities;
using ApplicationCore.System.TypePoint;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface ITypePointService
    {
        Task<ApiResponseModel<IEnumerable<TypePointModel>>> GetList(TypePointModelRequest request);
        Task<bool> Create(TypePointModel model);
        Task<bool> Update(TypePointModel model, Guid id);
        Task<bool> Delete(Guid Id);
        Task<ApiResponseModel<TypePointModel>> GetbyId(Guid Id);
    }
}
