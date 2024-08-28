using Microsoft.EntityFrameworkCore;
using Speridian.CMS.Entities.DTO;
using Speridian.CMS.PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speridian.CMS.BL
{
    public class LoginDAL
    {
        private readonly CmsDbContext _context;

        public LoginDAL(CmsDbContext context)
        {
            _context = context;
        }
        public async Task<List<User>> LoginUserAsync(LoginDTO loginDTO)
        {
            return await _context.Users.FromSqlRaw($"Exec USP_User_PostLogin '{loginDTO.UserName}','{loginDTO.Password}'").ToListAsync();
        }

        public async Task<bool> RefreshTokenAsync(UserRefreshTokenDto refreshTokenDto)
        {
            int rows = await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC USP_User_RefreshToken_InsertUpdate {refreshTokenDto.UserName},{refreshTokenDto.RefreshToken},{refreshTokenDto.OldRefershToken}");
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}

