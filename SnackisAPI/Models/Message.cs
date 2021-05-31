﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisAPI.Models
{
    public class Message
    {
        public Guid ID { get; set; }
        public string Content { get; set; }
        public Guid FromUser { get; set; }
        public Guid ToUser { get; set; }
        public DateTime SentAt { get; set; }
    }
}
