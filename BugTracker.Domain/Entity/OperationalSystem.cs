using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Entity
{
    public class OperationalSystem : Software
    {
        public OperationalSystem(string name) : base(name) { }

        private OperationalSystem() { }
    }
}
