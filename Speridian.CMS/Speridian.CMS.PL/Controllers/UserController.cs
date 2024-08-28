using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Speridian.CMS.BL;
using Speridian.CMS.Entities.DTO;
using Speridian.CMS.PL.Models;


namespace Speridian.CMS.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserBL _userBL;

        public UserController(UserBL userBL)
        {
            _userBL = userBL;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("Register-User")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] UserDto userDTO)
        {
            var res = await _userBL.RegisterUserAsync(userDTO);
            if (res)
            {
                return Ok(new { message = "Registered Successfully" });
            }
            else
            {
                throw new BadHttpRequestException("Operation Error");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetUser")]
        public async Task<IActionResult> GetActiveUsersAsync(int? id, int? roleId)
        {
            var users = await _userBL.GetUsersAsync(id, roleId);
            return Ok(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await _userBL.GetAllRoles());
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateCustomer([FromBody] UserDto2 userDto)
        {
            if (userDto == null)
            {
                throw new BadHttpRequestException("Opertion Error");
            }
            if (await _userBL.UpdateUserAsync(userDto))
            {
                return Ok(new { message = "Updated Successfully" });
            }
            else
            {
                throw new BadHttpRequestException("Not Found");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete-User")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id == 0)
            {
                throw new BadHttpRequestException("Opertion Error");
            }
            if (await _userBL.DeleteUser(id))
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
