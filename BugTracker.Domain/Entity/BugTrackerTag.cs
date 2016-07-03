using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Entity
{
    public class BugTrackerTag
    {
        public int IDBugTrackerTag { get; private set; }
        public String Name { get; private set; }
        public virtual BugTracker BugTracker { get; private set; }
        public int IDBugTracker { get; private set; }

        private BugTrackerTag() { }

        public BugTrackerTag(String name)
        {
            this.Name = name;
        }

        public BugTrackerTag(int id, String name) : this(name)
        {
            this.IDBugTrackerTag = id;
        }
    }
}
