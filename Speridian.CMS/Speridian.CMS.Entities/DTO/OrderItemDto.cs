using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speridian.CMS.Entities.DTO
{
    public class OrderItemDto
    {

        public string ItemName { get; set; }

        public int? Qty { get; set; }

        public int? Rate { get; set; }

        public int? Value { get; set; }


    }
}
