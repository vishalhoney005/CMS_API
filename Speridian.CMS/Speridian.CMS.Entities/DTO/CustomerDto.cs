using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speridian.CMS.Entities.DTO
{
    public class CustomerDto
    {
        public byte CustomerId { get; set; }

        public string CustomerName { get; set; } = null!;

        public string PhoneNo { get; set; }
    }
}
