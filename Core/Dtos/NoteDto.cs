using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class NoteDto
    {
        public int Id { get; set; }
        public string? NoteContent { get; set; }
        public int? PageNumber { get; set; }
        public int BookId { get; set; }
    }
}
