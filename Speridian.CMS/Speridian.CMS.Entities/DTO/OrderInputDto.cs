using Speridian.CMS.PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speridian.CMS.Entities.DTO
{
    public class OrderInputDto
    {
        public OrderInvoiceDto Order { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
