using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Speridian.CMS.Entities.DTO;
using Speridian.CMS.PL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speridian.CMS.DAL.Repositories
{
    public class MenuDAL
    {
        private readonly CmsDbContext _context;

        public MenuDAL(CmsDbContext context)
        {
            _context = context; 
        }
        public async Task<List<Menu>> GetItems(int? Item_No, string? Item_Name, int? Rate)
        {
            string sqlQuery = "GetAllItems @Item_No, @Item_Name, @Rate";
            var idParam = Item_No.HasValue ? new SqlParameter("@Item_No", Item_No) : new SqlParameter("@Item_No", DBNull.Value);
            var NameParam = string.IsNullOrEmpty(Item_Name) ? new SqlParameter("@Item_Name", DBNull.Value) : new SqlParameter("@Item_Name", Item_Name);
            var RateParam = Rate.HasValue ? new SqlParameter("@Rate", Rate) : new SqlParameter("@Rate", DBNull.Value);
            return await _context.Menus.FromSqlRaw(sqlQuery, idParam, NameParam, RateParam).ToListAsync();

        }
        public async Task<bool> InsertItem(MenuDto menuDto)
        {
            await _context.Database.ExecuteSqlRawAsync(
                   "EXEC USP_Menu_InsertUpdate " +
                   "@Item_No=@itno, @Item_Name=@itname, @Rate=@rate, @ImageURL=@imageUrl",
                   new SqlParameter("@itno", menuDto.ItemNo),
                   new SqlParameter("@itname", menuDto.ItemName),
                   new SqlParameter("@rate", menuDto.Rate),
                   new SqlParameter("@imageUrl", menuDto.ImageURL));
            return true;


           //await _context.Database.ExecuteSqlRawAsync($"EXEC USP_Menu_InsertUpdate {menuDto.ItemNo},'{menuDto.ItemName}',{menuDto.Rate},{menuDto.ImageURL}");
           // return true;
        }
        public async Task<bool> UpdateItem(MenuDto menu)
        {
            var item = await _context.Menus.FromSqlRaw($"EXEC GetAllItems {menu.ItemNo}").ToListAsync();
            if (item == null)
            {
                return false;
            }
            await _context.Database.ExecuteSqlRawAsync(
                   "EXEC USP_Menu_InsertUpdate " +
                   "@Item_No=@itno, @Item_Name=@itname, @Rate=@rate, @ImageURL=@imageUrl",
                   new SqlParameter("@itno", menu.ItemNo),
                   new SqlParameter("@itname", menu.ItemName),
                   new SqlParameter("@rate", menu.Rate),
                   new SqlParameter("@imageUrl", menu.ImageURL));
            return true;
        }
        public async Task<bool> DeleteItem(int id)
        {
            var item = await _context.Menus.FromSqlRaw($"EXEC GetAllItems {id}").ToListAsync();


            if (item == null)
            {
                return false;
            }
            var res = await _context.Database.ExecuteSqlRawAsync($"EXEC USP_Menu_Delete {id}");
            if(res != null)
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
