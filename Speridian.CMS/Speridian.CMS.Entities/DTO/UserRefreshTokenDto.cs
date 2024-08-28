using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speridian.CMS.Entities.DTO
{
    public class UserRefreshTokenDto
    {
        public string UserName { get; set; }
        public string RefreshToken { get; set; }
        public string OldRefershToken { get; set; }
    }
}
