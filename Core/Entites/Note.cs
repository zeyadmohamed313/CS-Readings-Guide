using Core.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entites
{
    public class Note
    {
        public int Id {  get; set; }
        public string? NoteContent {  get; set; }
        //public int PageNumber {  get; set; }
        public int BookId {  get; set; }
        public Book Book { get; set; }
        public string UserId {  get; set; }
        public AppUser User { get; set; }

    }
}
