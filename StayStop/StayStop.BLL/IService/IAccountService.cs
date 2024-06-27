using StayStop.BLL.Authentication;
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
        UserTokenResponse LoginUser(UserLoginDto dto, bool populateExp);
        UserTokenResponse RefreshToken(UserTokenResponse token);
        void RegisterUser(UserRegisterDto dto);
        UserTokenResponse UpdateUser(UserUpdateRequestDto dto);
    }
}
