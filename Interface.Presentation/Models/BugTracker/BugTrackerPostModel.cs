using BugTracker.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Interface.Presentation.Models.BugTracker
{
    public class BugTrackerPostModel
    {
        [Required(ErrorMessage="Field 'STATUS' is required")]
        [EnumDataType(typeof(BugTrackerStatus), ErrorMessage = "Field 'STATUS' invalid. Accepted values are: ERROR, INFO, WARNING")]
        public BugTrackerStatus Status { get; set; }

        [Required(ErrorMessage = "Field 'TAGS' is required")]
        public List<string> Tags { get; set; }

        [Required(ErrorMessage = "Field 'TRACE' is required")]
        public String Trace { get; set; }

        [Required(ErrorMessage = "Broken library. Please, download again")]
        public String HashCode { get; set; }

        public List<BugTrackerTag> ToTrackerTag()
        {
            List<BugTrackerTag> formatedTags = new List<BugTrackerTag>();

            foreach(string tagName in this.Tags)
            {
                formatedTags.Add(new BugTrackerTag(tagName));
            }

            return formatedTags;
        }
    }
}