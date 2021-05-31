using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisAPI.Models
{
    public class Comment
    {
        public Guid ID { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid PostID { get; set; }
        public Guid UserID { get; set; }
    }
}
