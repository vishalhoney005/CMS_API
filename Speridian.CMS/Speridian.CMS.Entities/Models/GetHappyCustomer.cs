using System;
using System.Collections.Generic;

namespace Speridian.CMS.PL.Models;

public partial class GetHappyCustomer
{
    public string CustomerName { get; set; } = null!;

    public double? Rating { get; set; }
}
