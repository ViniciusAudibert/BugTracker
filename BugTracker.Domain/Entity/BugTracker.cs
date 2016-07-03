using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Entity
{
    public class BugTracker
    {
        public int IDBugTracker { get; set; }
        public int IDApplication { get; set; }
        public virtual Application Application { get; private set; }
        public BugTrackerStatus Status { get; private set; }
        public string Description { get; private set; }
        public DateTime OccurredDate { get; private set; }
        public virtual Browser Browser { get; private set; }
        public virtual OperationalSystem OperationalSystem { get; private set; }
        public virtual ICollection<BugTrackerTag> Tags { get; private set; }
            
        public BugTracker() { }

        public BugTracker(Application application, BugTrackerStatus status, String description, DateTime data, List<BugTrackerTag> tags, Browser browser, OperationalSystem operationalSystem)
        {
            this.Application = application;
            this.Status = status;
            this.Description = description;
            this.OccurredDate = data;
            this.Tags = tags;
            this.OperationalSystem = operationalSystem;
            this.Browser = browser;
        }

        public BugTracker(int id, Application application, BugTrackerStatus status, String description, DateTime data, List<BugTrackerTag> tags, Browser browser, OperationalSystem operationalSystem)
            : this(application, status, description, data, tags, browser, operationalSystem)
        {
            this.IDBugTracker = id;
        }

        public bool ValidateTags()
        {
            return Tags.Where(_ => _.Name.Length > 20).Count() == 0;
        }

        public BugTrackerTag ContainsSpecialTag()
        {
            return Tags.FirstOrDefault(_ => _.Name.Equals(this.Application.SpecialTag));
        }
    }
}
