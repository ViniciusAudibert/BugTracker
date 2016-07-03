using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BugTracker.Domain.Entity;

namespace Interface.Presentation.Models.BugTracker
{
    public class BugTrackerViewModel
    {
        public String Trace { get; set; }
        public DateTime OccuredDate { get; set; }
        public BugTrackerStatus Status { get; set; }
        public string Tags { get; set; }
        public Browser Browser {get;set;}
        public OperationalSystem OperationalSystem { get; set; }

        public BugTrackerViewModel(String trace, DateTime occurredDate, BugTrackerStatus status, string tags, Browser browser, OperationalSystem operationalSystem)
        {
            this.Trace = trace;
            this.OccuredDate = occurredDate;
            this.Status = status;
            this.Tags = tags;
            this.OperationalSystem = operationalSystem;
            this.Browser = browser;
        }
    }
}