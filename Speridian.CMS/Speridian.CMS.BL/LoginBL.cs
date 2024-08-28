using Speridian.CMS.BL;
using Speridian.CMS.Entities.DTO;
using Speridian.CMS.PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speridian.CMS.DAL.Repositories
{
    public class LoginBL
    {
        private readonly LoginDAL _loginDAL;

        public LoginBL(LoginDAL loginDAL)
        {
            _loginDAL = loginDAL;
        }
        public async Task<List<User>> UserLoginasync(LoginDTO loginDTO)
        {

            return await _loginDAL.LoginUserAsync(loginDTO);
        }
        public async Task<bool> RefreshTokenAsync(UserRefreshTokenDto refreshTokenDto)
        {
            return await _loginDAL.RefreshTokenAsync(refreshTokenDto);
        }
    }
}
