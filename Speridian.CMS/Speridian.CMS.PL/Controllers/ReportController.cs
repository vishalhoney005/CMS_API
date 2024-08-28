using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Speridian.CMS.BL;

namespace Speridian.CMS.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ReportBL _reportBL;

        public ReportController(ReportBL reportBL)
        {
            _reportBL = reportBL;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("View-Best-Selling-Item")]
        public async Task<IActionResult> GetBestSellingItemsAsync()
        {

            return Ok(await _reportBL.GetBestSellingItemsAsync());
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("View-Rating")]
        public async Task<IActionResult> GetOverallRatingsAsync()
        {

            return Ok(await _reportBL.GetOverallRatingsAsync());
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("View-Happy-Customers")]
        public async Task<IActionResult> GetHappyCustomersAsync()
        {

            return Ok(await _reportBL.GetHappyCustomersAsync());
        }
    }
}
