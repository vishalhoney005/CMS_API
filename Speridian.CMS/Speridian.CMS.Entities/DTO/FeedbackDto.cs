using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speridian.CMS.Entities.DTO
{
    public class FeedbackDto
    {
        public int Id { get; set; }

        public string? CustomerName { get; set; }

        public string Text { get; set; } = null!;

        public int Rating { get; set; }

        public DateTime? CreatedDate { get; set; }
    }

    public class FeedbackDto2
    {
        public int Id { get; set; }

        public string PhoneNo { get; set; }
        public string Text { get; set; } = null!;

        public int Rating { get; set; }
    }
}
