using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class UserDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string? Address { get; set; }
        public string UserName { get; set; }
        [EmailAddress]
        public string Email {  get; set; }
        
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword {  get; set; }
    }
}
