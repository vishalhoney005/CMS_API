using System;
using System.Collections.Generic;

namespace Speridian.CMS.PL.Models;

public partial class CustomerMaster
{
    public byte CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string PhoneNo { get; set; }

    public int? IsDeleted { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<OrderInvoice> OrderInvoices { get; set; } = new List<OrderInvoice>();
}
