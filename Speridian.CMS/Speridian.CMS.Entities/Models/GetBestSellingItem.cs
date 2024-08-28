using System;
using System.Collections.Generic;

namespace Speridian.CMS.PL.Models;

public partial class GetBestSellingItem
{
    public int ItemNo { get; set; }

    public string ItemName { get; set; } = null!;

    public int? TotalQty { get; set; }
}
