using StayStop.BLL.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayStop.BLL.IService
{
    public interface IAccountService
    {
        string GetJwtToken(UserLoginDto dto);
        void RegisterUser(UserRegisterDto dto);
    }
}
