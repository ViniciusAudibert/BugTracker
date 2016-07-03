using BugTracker.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Interface.Service
{
    public interface IDownloadService
    {
        string GetFileName();
        byte[] GetFileByte(User user);
        void CreateFileForUser(User user);
        void SetPath(string path);
    }
}
