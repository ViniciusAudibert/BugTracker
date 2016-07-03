using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Exceptions
{
    public class TagVeryLargeException : Exception
    {
        public override string ToString()
        {
            return "Tags exceeded limit 20 characters";
        }
    }
}
