using System;
using System.Collections.Generic;

namespace Speridian.CMS.PL.Models;

public partial class Feedback
{
    public byte Id { get; set; }

    public byte CustomerId { get; set; }

    public string CustomerName { get; set; }

    public string PhoneNo { get; set; }

    public string Text { get; set; } = null!;

    public double Rating { get; set; }

    public int? IsDeleted { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual CustomerMaster Customer { get; set; } = null!;
}
