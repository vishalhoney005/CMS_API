using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Speridian.CMS.BL;
using Speridian.CMS.Entities.DTO;

namespace Speridian.CMS.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly FeedbackBL _feedbackBL;

        public FeedbackController(FeedbackBL feedbackBL)
        {
            _feedbackBL = feedbackBL;
        }
        //[Authorize(Roles = "Admin,Employee")]
        [HttpGet("View-Feedback")]
        public async Task<IActionResult> GetFeedbacks(int? id, int? customerId, float? rating)
        {

            return Ok(await _feedbackBL.GetFeedbacksAsync(id, customerId, rating));
        }
        //[Authorize(Roles = "Admin,Employee")]
        [HttpPost("Add-Feedback")]
        public async Task<IActionResult> AddFeedback(FeedbackDto2 feedbackDto)
        {
            if (feedbackDto == null)
            {
                throw new BadHttpRequestException("Invalid Model");

            }
            if (await _feedbackBL.AddFeedback(feedbackDto))
            {
                return Ok(new { message = "Added Successfully" });
            }
            else
            {
                throw new BadHttpRequestException("Opertion Error");

            }
        }
    }
}
