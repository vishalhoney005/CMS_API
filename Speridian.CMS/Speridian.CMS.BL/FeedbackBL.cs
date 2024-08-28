using AutoMapper;
using Speridian.CMS.DAL.Repositories;
using Speridian.CMS.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speridian.CMS.BL
{
    public class FeedbackBL
    {
        private readonly FeedbackDAL _feedbackDAL;
        private readonly IMapper _mapper;

        public FeedbackBL(FeedbackDAL feedbackDAL,IMapper mapper)
        {
            _feedbackDAL = feedbackDAL;
            _mapper = mapper;
        }
        public async Task<List<FeedbackDto>> GetFeedbacksAsync(int? id, int? customerId, float? rating)
        {
            var feedback = await _feedbackDAL.GetFeedbacks(id, customerId, rating);
            var feedbackDto = _mapper.Map<List<FeedbackDto>>(feedback);
            return feedbackDto;
        }
        public async Task<bool> AddFeedback(FeedbackDto2 feedbackDto)
        {
            return await _feedbackDAL.AddFeedback(feedbackDto);
        }
    }
}
