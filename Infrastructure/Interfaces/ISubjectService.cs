using ApplicationCore.Entities;
using ApplicationCore.System.Subject;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface ISubjectService
    {
        Task<ApiResponseModel<IEnumerable<SubjectModel>>> GetList(SubjectModelRequest request);
        Task<bool> Create(SubjectModel model);
        Task<bool> Update(SubjectModel model, Guid id);
        Task<bool> Delete(Guid Id);
        Task<ApiResponseModel<SubjectModel>> GetbyId(Guid Id);
    }
}
