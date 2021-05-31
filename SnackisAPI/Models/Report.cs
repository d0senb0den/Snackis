using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisAPI.Models
{
    public class Report
    {
        public Guid ID { get; set; }
        public string Content { get; set; }
        public Guid Post { get; set; }
        public Guid Comment { get; set; }
        public Guid ForUser { get; set; }
        public Guid ByUser { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
