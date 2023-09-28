using ApplicationCore.Entities;
using ApplicationCore.System.Point;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IPointService
    {
        Task<ApiResponseModel<IEnumerable<PointModel>>> GetList(PointModelRequest request);
        Task<bool> Create(PointModel model);
        Task<bool> Update(PointModel model, Guid id);
        Task<bool> Delete(Guid Id);
        Task<ApiResponseModel<PointModel>> GetbyId(Guid Id);
    }
}
