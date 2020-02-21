using System;
using System.ComponentModel.DataAnnotations;
using Martial.Data.Models;

namespace Martial.Web.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }
 
        [Required]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string PasswordConfirm  { get; set; }

        [Required]
        public Role Role { get; set; }

    }
}