using Speridian.CMS.DAL.Repositories;
using Speridian.CMS.PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speridian.CMS.BL
{
    public class ReportBL
    {
        private readonly ReportDAL _reportDAL;

        public ReportBL(ReportDAL reportDAL)
        {
            _reportDAL = reportDAL;
        }
        public async Task<List<GetBestSellingItem>> GetBestSellingItemsAsync()
        {
            var report = await _reportDAL.BestSellingItem();

            return report;
        }
        public async Task<List<GetOverallRating>> GetOverallRatingsAsync()
        {
            var report = await _reportDAL.GetOverallRatings();

            return report;
        }
        public async Task<List<GetHappyCustomer>> GetHappyCustomersAsync()
        {
            var report = await _reportDAL.GetHappyCustomers();

            return report;
        }

    }
}
