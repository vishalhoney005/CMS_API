using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Speridian.CMS.Entities.DTO;
using Speridian.CMS.PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speridian.CMS.DAL.Repositories
{
    public class UserDAL
    {
        private readonly CmsDbContext _context;

        public UserDAL(CmsDbContext context)
        {
            _context = context;
        }
        public async Task<bool> RegisterUserAsync(UserDto userDto)
        {
            await _context.Database.ExecuteSqlRawAsync($"EXEC USP_Users_InsertUser {userDto.UserId},'{userDto.UserName}','{userDto.Password}','','{userDto.RoleName}'");
            return true;

        }
        public async Task<List<User>> GetUsersAsync(int? userId, int? roleId)
        {
            string sqlQuery = "USP_Users_GetList @UserId, @RoleId";

            var userIdParam = userId.HasValue ? new SqlParameter("@UserId", userId) : new SqlParameter("@UserId", DBNull.Value);
            var roleIdParam = roleId.HasValue ? new SqlParameter("@RoleId", roleId) : new SqlParameter("@RoleId", DBNull.Value);

            return await _context.Users.FromSqlRaw(sqlQuery, userIdParam, roleIdParam).ToListAsync();

        }

        public async Task<List<Role>> GetAllRoles()
        {
            return await _context.Roles.FromSqlRaw($" Select * from Roles").ToListAsync();

        }

        public async Task<bool> UpdateUser(UserDto2 userDto)
        {
            var item = await _context.Users.FromSqlRaw($"EXEC USP_Users_GetList {userDto.UserId}").ToListAsync();
            if (item == null)
            {
                return false;
            }
            await _context.Database.ExecuteSqlRawAsync($"EXEC  USP_Users_InsertUser {userDto.UserId},'{userDto.UserName}','{userDto.Password}','{userDto.NewPassword}','{null}'");
            return true;

        }

        public async Task<bool> DeleteUser(int id)
        {
            var item = await GetUsersAsync(id, null);

            if (item == null)
            {
                return false;
            }

            var rowsAffected = await _context.Database.ExecuteSqlRawAsync($"EXEC USP_Users_Delete {id}");
            if (rowsAffected == null) 
            {
                return false;
            }
            return true;

            

        }
    }
}
