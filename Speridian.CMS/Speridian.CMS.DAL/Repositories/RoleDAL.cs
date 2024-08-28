using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Speridian.CMS.PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speridian.CMS.BL
{
    public class RoleDAL
    {
        private readonly CmsDbContext _context;

        public RoleDAL(CmsDbContext context)
        {
            _context = context;
        }
        public async Task<List<Role>> GetRoleById(int? id)
        {
            string sqlQuery = "USP_Roles_GetRolesById  @RoleId";
            var idParam = id.HasValue ? new SqlParameter("@RoleId", id) : new SqlParameter("@RoleId", DBNull.Value);
            return await _context.Roles.FromSqlRaw(sqlQuery,idParam).ToListAsync();
        }
    }
}
