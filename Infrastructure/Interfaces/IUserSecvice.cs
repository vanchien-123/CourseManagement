using ApplicationCore.Entities;
using ApplicationCore.System.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IUserSecvice
    {
        Task<string> Authencate(LoginRequest loginRequest);

        Task<bool> Register(ApplicationUser registerRequest);
    }
}
