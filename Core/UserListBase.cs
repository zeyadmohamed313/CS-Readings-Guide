using Core.Entites.Identity;
using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class UserListBase
    {
        public int Id { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public List<Book> Books { get; set; }
    }
}
