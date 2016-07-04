using Interface.Presentation.Filters;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Interface.Presentation.Models.User
{
    public class UserEditAccountViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(80, ErrorMessage = "Maximun of 80 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [StringLength(90, ErrorMessage = "Maximun of 90 characters")]
        [DisplayName("E-mail")]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "Maximun of 100 characters")]
        public String Image { get; set; }

        [DisplayName("New Photo")]
        [ImageExtensionValidation(ErrorMessage = "Only images .jpg, .jpeg and .png")]
        [FileSizeValidation(100 * 1024, ErrorMessage = "File size must be lesser than 100 kb")]
        public HttpPostedFileBase File { get; set; }

        [DisplayName("Old Password")]
        [RegularExpression(@"^(?=.*\d)(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,}$", ErrorMessage = "Password must have: minimun length of 8 characters, a number, a low case word and a hight case word.")]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "Maximun of 50 characters")]
        public String OldPassword { get; set; }


        [DisplayName("New Password")]
        [RegularExpression(@"^(?=.*\d)(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,}$", ErrorMessage = "Password must have: minimun length of 8 characters, a number, a low case word and a hight case word.")]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "Maximun of 50 characters")]
        public String NewPassword { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Confirm  New Password")]
        [Compare("NewPassword", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }

        [StringLength(200, ErrorMessage = "Maximun of 200 characters")]
        public string HashCode { get; set; }
    }
}