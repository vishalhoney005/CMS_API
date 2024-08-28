using System;
using System.Collections.Generic;

namespace Speridian.CMS.PL.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public string Type { get; set; } = null!;

    public int? IsDeleted { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<OrderInvoice> OrderInvoices { get; set; } = new List<OrderInvoice>();
}
