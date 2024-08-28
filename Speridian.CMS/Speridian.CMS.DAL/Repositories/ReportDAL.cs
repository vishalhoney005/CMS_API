using Microsoft.EntityFrameworkCore;
using Speridian.CMS.PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speridian.CMS.DAL.Repositories
{
    public class ReportDAL
    {
        private readonly CmsDbContext _context;

        public ReportDAL(CmsDbContext context)
        {
            _context = context;
        }
        public async Task<List<GetBestSellingItem>> BestSellingItem()
        {
            return await _context.GetBestSellingItems.FromSqlRaw($" Select * from GetBestSellingItem").ToListAsync();
             
        }
        public async Task<List<GetOverallRating>> GetOverallRatings()
        {
            return await _context.GetOverallRatings.FromSqlRaw($" Select * from GetOverallRating").ToListAsync();

        }
        public async Task<List<GetHappyCustomer>> GetHappyCustomers()
        {
            return await _context.GetHappyCustomers.FromSqlRaw($" Select * from GetHappyCustomers").ToListAsync();

        }


    }
}
