using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Core.Entites.Identity
{
    public class AppUser:IdentityUser
    {
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public FavouriteList FavouriteList { get; set; }
        public ToReadList ToReadList { get; set; }
        public ReadList ReadList { get; set; }
        public CurrentlyReadingList CurrentlyReadingList { get; set; }
        public List<Note> Notes { get; set; }
    }
}
