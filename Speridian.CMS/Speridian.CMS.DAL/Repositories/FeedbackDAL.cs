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
    public class FeedbackDAL
    {
        private readonly CmsDbContext _context;

        public FeedbackDAL(CmsDbContext context)
        {
            _context = context;
        }
        public async Task<List<Feedback>> GetFeedbacks(int? id, int? customerId, float? rating)
        {
            string sqlQuery = "GetFeedback  @Id, @Customer_id, @Rating";
            var idParam = id.HasValue ? new SqlParameter("@Id", id) : new SqlParameter("@Id", DBNull.Value);
            var custParam = customerId.HasValue ? new SqlParameter("@Customer_id", customerId) : new SqlParameter("@Customer_id", DBNull.Value);
            var ratingParam = rating.HasValue ? new SqlParameter("@Rating", rating) : new SqlParameter("@Rating", DBNull.Value);
            return await _context.Feedbacks.FromSqlRaw(sqlQuery, idParam, custParam, ratingParam).ToListAsync();

        }
        public async Task<bool> AddFeedback(FeedbackDto2 feedbackDto)
        {
            var status =await _context.Database.ExecuteSqlRawAsync(
                    "EXEC USP_Feedback_InsertUpdate " +
                    "@Id=@id, @PhoneNo=@phoneno, @Text=@text, @Rating=@rating",
                    new SqlParameter("@id", feedbackDto.Id),
                    new SqlParameter("@phoneno", feedbackDto.PhoneNo),
                    new SqlParameter("@text", feedbackDto.Text),
                    new SqlParameter("@rating", feedbackDto.Rating));
            if (status != 0)
            {
                return true;
            }
            return false;



        }
    }
}
