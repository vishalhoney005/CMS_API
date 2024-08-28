using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Speridian.CMS.Entities.DTO;
using Speridian.CMS.Exceptions;
using Speridian.CMS.Exceptions;

namespace Speridian.CMS.Helpers
{
    public class AuthorizeService
    {
        private readonly IConfiguration _config;
        public AuthorizeService(IConfiguration cofig)
        {
            _config = cofig;
        }
        public string CreateAccessToken(string userName, string roleName, int id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["JWT:Key"]);
            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, roleName),
                    new Claim(ClaimTypes.NameIdentifier,id.ToString())
                }),
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"],
                Expires = DateTime.Now.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
            };
            var token = tokenHandler.CreateJwtSecurityToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }

        public string CreateRefreshToken()
        {
            var randomNumber = new Byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public TokenDto CreateToken(string userName, string roleName, int id)
        {
            return new TokenDto() { AccessToken = CreateAccessToken(userName, roleName, id), RefreshToken = CreateRefreshToken() };
        }

        public ClaimsPrincipal ValidateAccessToken(string accessToken)
        {
            var TokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["JWT:Key"]);
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidIssuer = _config["JWT:Issuer"],
                ValidAudience = _config["JWT:Audience"],
                ValidateIssuer = true,
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
            var principal = TokenHandler.ValidateToken(accessToken, tokenValidationParameters, out SecurityToken validatedToken);
            JwtSecurityToken jwtSecurityToken = validatedToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new AllException("Invalid Token");
            }
            return principal;
        }
    }
}