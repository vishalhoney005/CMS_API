using System;
using System.Collections.Generic;

namespace Speridian.CMS.PL.Models;

public partial class OrderItem
{
    public short? InvoiceNo { get; set; }

    public int? ItemNo { get; set; }

    public int? Qty { get; set; }

    public string ItemName { get; set; }

    public int? Rate { get; set; }

    public int? Value { get; set; }

    public int? IsDeleted { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual OrderInvoice? InvoiceNoNavigation { get; set; }

    public virtual Menu? ItemNoNavigation { get; set; }
}
