using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisWebApp.Models
{
    public class SubCategory
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryID { get; set; }

    }
}
