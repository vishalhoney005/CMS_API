using System;
using System.Collections.Generic;

namespace Speridian.CMS.PL.Models;

public partial class UserRefreshToken
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public string? RefreshToken { get; set; }

    public int? IsDeleted { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
