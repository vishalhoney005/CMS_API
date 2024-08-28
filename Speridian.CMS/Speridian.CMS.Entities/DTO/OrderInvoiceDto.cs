using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speridian.CMS.Entities.DTO
{
    public class OrderInvoiceDto
    {
        public short InvoiceNo { get; set; }

        public DateTime? Date { get; set; }

        public string? CustomerName { get; set; }

        public string? PayType { get; set; }

        public string? PhoneNo { get; set;}

        public int? Amount { get; set; }


    }
}
