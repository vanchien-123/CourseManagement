using ApplicationCore.Models.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IPermission
    {
        Task<bool> GetList(string roleId);
        Task<bool> Update(PermissionViewModel model, string roleId);
    }
}
