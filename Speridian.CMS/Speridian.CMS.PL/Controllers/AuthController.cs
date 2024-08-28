using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using NuGet.Configuration;
using Speridian.CMS.BL;
using Speridian.CMS.DAL.Repositories;
using Speridian.CMS.Entities.DTO;
using Speridian.CMS.Helpers;
using Speridian.CMS.PL.Models;
using System.Security.Claims;

namespace Speridian.CMS.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthorizeService _authorize;
        private readonly RoleBL _roleBL;
        private readonly LoginBL _loginBL;

        public AuthController(AuthorizeService iauthorize, RoleBL roleBL, LoginBL loginBL)
        {

            _authorize = iauthorize;
            _roleBL = roleBL;
            _loginBL = loginBL;
        }
        [HttpPost("UserLogin")]
        public async Task<IActionResult> UserLogin([FromBody] LoginDTO loginDTO)
        {

            List<User> user = await _loginBL.UserLoginasync(loginDTO);
            if (user.Any())
            {
                UserRefreshTokenDto refreshTokenDto = new UserRefreshTokenDto();
                TokenDto tokenDto = new TokenDto();
                foreach (var item in user)
                {
                    List<RoleDto> role = await _roleBL.GetRolesById(item.RoleId);
                    item.Role = new Role()
                    {
                        Id = item.RoleId,
                        Name = role[0].Name
                    };

                    tokenDto = _authorize.CreateToken(user[0].UserName, user[0].Role.Name, item.UserId);
                    tokenDto.UserId = item.UserId;
                    tokenDto.UserName = item.UserName;
                    tokenDto.RoleId = item.RoleId;
                    refreshTokenDto.UserName = item.UserName;
                    refreshTokenDto.RefreshToken = tokenDto.RefreshToken;
                }
                bool refreshTokenStatus = await _loginBL.RefreshTokenAsync(refreshTokenDto);
                if (refreshTokenStatus)

                {
                    return Ok(tokenDto);
                }
                else
                {
                    throw new BadHttpRequestException("Opertion Error");
                }
            }
            else { throw new Exception("Authentication Error"); }


        }
        [HttpPut("UserRefreshToken")]
        public async Task<IActionResult> UserRefreshToken(TokenDto tokensDTO)
        {
            if (!ModelState.IsValid)
            {
                throw new BadHttpRequestException("Invalid Model");
            }
            else
            {
                var principal = _authorize.ValidateAccessToken(tokensDTO.AccessToken);
                //string username = principal.Identity.Name;
                string userName = principal.FindFirst(ClaimTypes.Name)?.Value;
                string role = principal.FindFirst(ClaimTypes.Role)?.Value;
                string subject = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!int.TryParse(subject, out int id))
                {
                    throw new Exception("Unable to parse");
                };
                TokenDto newTokensDTO = _authorize.CreateToken(userName, role,id);
                bool status = await _loginBL.RefreshTokenAsync(new UserRefreshTokenDto() { UserName = userName, OldRefershToken = tokensDTO.RefreshToken, RefreshToken = newTokensDTO.RefreshToken });
                if (status)
                {
                    return (Ok(newTokensDTO));
                }
                else
                {
                    throw new BadHttpRequestException("Token Refresh Operation Error");
                }
            }

        }

    }
}
