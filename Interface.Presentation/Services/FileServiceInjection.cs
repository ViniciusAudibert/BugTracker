using BugTracker.Domain.Service;
using BugTracker.Infra.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Interface.Presentation.Services
{
    public static class DownloadServiceInjection
    {
        public static DownloadService Create()
        {
            return new DownloadService(new FileService());
        }
    }
}