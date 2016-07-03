using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Interface.Presentation.Models.UserRecovery
{
    public class UserRecoveryViewModel
    {
        public string IDCode { get; set; }

        [PasswordPropertyText(true)]
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*\d)(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,}$", ErrorMessage = "Password must have: minimun length of 8 characters, a number, a low case word and a hight case word.")]
        [DataType(DataType.Password)]
        [StringLength(100)]
        public String Password { get; set; }

        [Required(ErrorMessage = "Confirm your password please")]
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }
    }
}