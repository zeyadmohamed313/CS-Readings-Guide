﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class BookDtoWithOutId
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public DateTime? PublishTime { get; set; }
        public string? ImageUrl { get; set; }
        public byte[]? Content { get; set; }
        public int CategoryId { get; set; }
    }
}
