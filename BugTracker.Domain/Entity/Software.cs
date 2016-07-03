using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Entity
{
    public class Software
    {
        public String Name { get; private set; }
        
        public Software() { }

        public Software(String name)
        {
            this.Name = name;
        }
    }
}
