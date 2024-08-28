using System;
using System.Collections.Generic;

namespace Speridian.CMS.PL.Models;

public partial class Menu
{
    public int ItemNo { get; set; }

    public string ItemName { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public int Rate { get; set; }

    public int? IsDeleted { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
