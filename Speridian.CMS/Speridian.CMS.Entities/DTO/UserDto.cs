using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speridian.CMS.Entities.DTO
{
    public class UserDto
    {
        public int? UserId { get; set; } 
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
    }
    public class UserDto2
    {
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? NewPassword { get; set; }
        public string RoleName { get; set; }
    }
}
