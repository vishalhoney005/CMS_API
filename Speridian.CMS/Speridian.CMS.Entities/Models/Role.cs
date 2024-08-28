using Speridian.CMS.PL.Models;
using System;
using System.Collections.Generic;

namespace Speridian.CMS.PL.Models;

public partial class Role
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? IsDeleted { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
