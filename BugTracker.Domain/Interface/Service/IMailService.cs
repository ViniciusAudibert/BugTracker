using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Interface.Service
{
    public interface IMailService
    {
        void Send(string mailTo, string subject, string body, bool isHtml, string filePath = null);
    }
}
