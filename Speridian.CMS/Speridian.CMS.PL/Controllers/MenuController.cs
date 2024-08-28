using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Speridian.CMS.BL;
using Speridian.CMS.Entities.DTO;

namespace Speridian.CMS.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly MenuBL _menuBL;

        public MenuController(MenuBL menuBL)
        {
            _menuBL = menuBL;
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpGet("View-Menu")]
        public async Task<IActionResult> GetItems(int? Item_no, string? Item_Name, int? Rate)
        {

            return Ok(await _menuBL.GetItemsAsync(Item_no, Item_Name, Rate));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("Add-Item")]
        public async Task<IActionResult> InsertItem(MenuDto menuDto)
        {
            if (menuDto == null)
            {
                throw new BadHttpRequestException("Invalid Model");
            }
            if (await _menuBL.InsertItem(menuDto))
            {
                return Ok(new { message = "Added Successfully" });
            }
            else
            {
                throw new BadHttpRequestException("Operation Error");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("Update-Item")]
        public async Task<IActionResult> UpdateItem(MenuDto menuDto)
        {
            if (menuDto == null)
            {
                throw new BadHttpRequestException("Invalid Model");
            }
            if (await _menuBL.InsertItem(menuDto))
            {
                return Ok(new { message = "Updated Successfully" });
            }
            else
            {
                throw new BadHttpRequestException("Not Found");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete-Item")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            if (id == 0)
            {
                throw new BadHttpRequestException("Invalid Error");
            }
            if (await _menuBL.DeleteItem(id))
            {
                return Ok(new { message = "Deleted Successfully" });
            }
            else
            {
                throw new BadHttpRequestException("Not Found");
            }
        }
    }
}
