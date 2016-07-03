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
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DisplayName("Password")]
        [StringLength(100)]
        [PasswordPropertyText(true)]
        public string Password { get; set; }

    }
}