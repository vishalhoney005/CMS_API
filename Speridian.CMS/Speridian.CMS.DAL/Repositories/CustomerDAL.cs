using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Speridian.CMS.Entities.DTO;
using Speridian.CMS.Exceptions;
using Speridian.CMS.PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speridian.CMS.DAL.Repositories
{
    public class CustomerDAL
    {
        private readonly CmsDbContext _context;

        public CustomerDAL(CmsDbContext context)
        {
            _context = context;
        }
        public async Task<List<CustomerMaster>> GetCustomers(int? Id, string? Name, string? PhNo)
        {
            string sqlQuery = "GetAllCustomers  @Customer_Id, @Customer_Name, @Phone_No";
            var idParam = Id.HasValue ? new SqlParameter("@Customer_Id", Id) : new SqlParameter("@Customer_Id", DBNull.Value);
            var NameParam = string.IsNullOrEmpty(Name) ? new SqlParameter("@Customer_Name", DBNull.Value) : new SqlParameter("@Customer_Name", Name);
            var PhNoParam = string.IsNullOrEmpty(PhNo) ? new SqlParameter("@Phone_No", DBNull.Value) : new SqlParameter("@Phone_No", PhNo);
            return await _context.CustomerMasters.FromSqlRaw(sqlQuery, idParam, NameParam, PhNoParam).ToListAsync();

        }   
        public async Task<bool> AddCustomer(CustomerDto customerDto)
        {

            await _context.Database.ExecuteSqlRawAsync($"EXEC USP_Customer_InsertUpdate {customerDto.CustomerId},'{customerDto.CustomerName}',{customerDto.PhoneNo}");
            return true;



        }
        public async Task<bool> UpdateCustomer(CustomerDto customerDto)
        {
            var item = await _context.CustomerMasters.FromSqlRaw($"EXEC GetAllCustomers {customerDto.CustomerId}").ToListAsync();
            if (item == null)
            {
                return false;
            }
            await _context.Database.ExecuteSqlRawAsync($"EXEC  USP_Customer_InsertUpdate {customerDto.CustomerId},'{customerDto.CustomerName}',{customerDto.PhoneNo}");
            return true;




        }
        public async Task<bool> DeleteCustomer(int id)
        {
            var item = await GetCustomers(id, null, null);

            if (item == null)
            {

                return false;
            }

            var res= await _context.Database.ExecuteSqlRawAsync($"EXEC USP_Customer_Delete {id}");

            if (res != null)
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
