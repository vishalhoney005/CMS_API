using System;
using System.Collections.Generic;

namespace Speridian.CMS.PL.Models;

public partial class OrderInvoice
{
    public short InvoiceNo { get; set; }

    public DateTime? Date { get; set; } 

    public byte? CustomerId { get; set; }

    public int? PayId { get; set; }

    public string CustomerName { get; set; }    

    public string PayType { get; set; }

    public string PhoneNo { get; set; }

    public int? Amount { get; set; }

    public int? IsDeleted { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual CustomerMaster? Customer { get; set; }

    public virtual Payment? Pay { get; set; }
}
