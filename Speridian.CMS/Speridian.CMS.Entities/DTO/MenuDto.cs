using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speridian.CMS.Entities.DTO
{
    public class MenuDto
    {
        public int ItemNo { get; set; }

        public string ItemName { get; set; } = null!;

        public int Rate { get; set; }
        public string ImageURL { get; set; }
    }
}
