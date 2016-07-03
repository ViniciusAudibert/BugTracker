using Interface.Presentation.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Interface.Presentation.Models.User
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [DisplayName("E-mail")]
        [EmailAddress]
        public string Email { get; set; }

        [ImageExtensionValidation(ErrorMessage = "Only images .jpg, .jpeg and .png")]
        [FileSizeValidation( 100 * 1024, ErrorMessage = "File size must be lesser than 100 kb")]
        [DisplayName("Perfil Image")]
        public HttpPostedFileBase FileImage { get; set; }

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