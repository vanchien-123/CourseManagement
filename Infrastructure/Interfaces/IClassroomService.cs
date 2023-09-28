using ApplicationCore.Entities;
using ApplicationCore.System.Classroom;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IClassroomService
    {
        Task<ApiResponseModel<IEnumerable<ClassroomModel>>> GetList(ClassroomModelRequest request);
        Task<bool> Create(ClassroomModel model);
        Task<bool> Update(ClassroomModel model, Guid id);
        Task<bool> Delete(Guid Id);
        Task<ApiResponseModel<ClassroomModel>> GetbyId(Guid Id);
    }
}
