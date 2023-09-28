using ApplicationCore.Entities;
using ApplicationCore.System.Student;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IStudentService
    {
        Task<ApiResponseModel<IEnumerable<StudentModel>>> GetList(StudentModelRequest request);
        Task<bool> Create(StudentModel model);
        Task<bool> Update(StudentModel model, Guid id);
        Task<bool> Delete(Guid Id);
        Task<ApiResponseModel<StudentModel>> GetbyId(Guid Id);
    }
}
