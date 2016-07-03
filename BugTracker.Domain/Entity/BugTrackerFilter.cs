using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Entity
{
    public class BugTrackerFilter
    {
        public string Trace { get; set; }
        public int idApplication { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
        public List<BugTrackerStatus> Status { get; set; }
    }
}
