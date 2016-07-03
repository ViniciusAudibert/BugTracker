using Interface.Presentation.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Interface.Presentation.Models
{
    public class ApplicationModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [DisplayName("Title")]
        [StringLength(50, ErrorMessage = "Maximun of 50 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [DisplayName("Description")]
        [StringLength(100, ErrorMessage = "Maximun of 100 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Url application is required")]
        [DisplayName("Url Application")]
        [StringLength(100, ErrorMessage = "Maximun of 100 characters")]
        public string Url { get; set; }
        
        [DisplayName("Icon Application")]
        [StringLength(100, ErrorMessage = "Maximun of 100 characters")]
        public string Icon { get; set; }

        [Required(ErrorMessage ="Special tag error is required")]
        [DisplayName("Special Tag Error")]
        [StringLength(20,ErrorMessage = "Maximun of 20 characters")]
        public string Tag { get; set; }

        
        [DisplayName("Icon Application")]
        [ImageExtensionValidation(ErrorMessage = "Only images .jpg, .jpeg and .png")]
        [FileSizeValidation(100 * 1024, ErrorMessage = "File size must be lesser than 100 kb")]
        public HttpPostedFileBase File { get; set; }

       
    }
}