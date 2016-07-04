using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Interface.Presentation.Models.User
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "Email address is required")]
        [DisplayName("E-mail")]
        [StringLength(90, ErrorMessage = "Maximun of 90 characters")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DisplayName("Password")]
        [StringLength(50, ErrorMessage = "Maximun of 50 characters")]
        [PasswordPropertyText(true)]
        public string Password { get; set; }

    }
}