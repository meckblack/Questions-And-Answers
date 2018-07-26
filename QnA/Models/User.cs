using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QnA.Models
{
    public class User
    {
        public int UserId { get; set; }
        
        [Required(ErrorMessage="First Name is required!!!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required!!!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email field is required!!!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username field is reqired!!!")]
        public string Username { get; set; }

        [Required(ErrorMessage ="Password field is required!!!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage ="ConfirmPassword field is required!!!")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        


    }
}
