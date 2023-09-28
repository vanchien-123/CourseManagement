using ApplicationCore.Entities;
using ApplicationCore.System.Instructor;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IInstructorService
    {
        Task<ApiResponseModel<IEnumerable<InstructorModel>>> GetList(InstructorModeRequest request);
        Task<bool> Create(InstructorModel model);
        Task<bool> Update(InstructorModel model, Guid id);
        Task<bool> Delete(Guid Id);
        Task<ApiResponseModel<InstructorModel>> GetbyId(Guid Id);
    }
}
