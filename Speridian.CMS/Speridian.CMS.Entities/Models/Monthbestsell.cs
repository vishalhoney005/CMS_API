using System;
using System.Collections.Generic;

namespace Speridian.CMS.PL.Models;

public partial class Monthbestsell
{
    public byte ItemNo { get; set; }

    public string ItemName { get; set; } = null!;

    public string? Month { get; set; }

    public int? TotalQuantitySold { get; set; }
}
