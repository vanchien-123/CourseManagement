using ApplicationCore.Entities;
using ApplicationCore.System.Course;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface ICourseService
    {
        Task<ApiResponseModel<IEnumerable<CourseModel>>> GetList(CourseModelRequest request);
        Task<bool> Create(CourseModel model);
        Task<bool> Update(CourseModel model, Guid id);
        Task<bool> Delete(Guid Id);
        Task<ApiResponseModel<CourseModel>> GetbyId(Guid Id);
    }
}
