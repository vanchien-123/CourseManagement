using ApplicationCore.Entities;
using ApplicationCore.System.User;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IUserService   
    {
        Task<string> Authencate(LoginRequest loginRequest);
        Task<bool> Register(ApiRequestRegisterModel registerRequest, string role);
        Task<ApiResponseModel<IEnumerable<ApiRequestRegisterModel>>> GetList(UserModelRequest request);
        Task<bool> Update(ApiRequestRegisterModel model, string id, string role);
        Task<bool> Delete(string Id);
        Task<ApiResponseModel<ApiRequestRegisterModel>> GetbyId(string Id);
        
    }
}
