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
        string LoginUser(UserLoginDto dto); // w tej metodzie jwt gen
        void RegisterUser(UserRegisterDto dto); // mozna mzienic od razu na zalogowanie
    }
}
