using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisAPI.Models
{
    public class Post
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Content { get; set; }
        public Guid SubCategoryID { get; set; }
        public Guid User { get; set; }

    }
}
