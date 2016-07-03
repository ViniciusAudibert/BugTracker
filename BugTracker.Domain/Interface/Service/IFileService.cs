using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Interface.Service
{
    public interface IFileService
    {
        bool Exists(string path);
        string ReadAllLines(string path);
        void Write(string path, string body);
        byte[] ReadAllBytes(string path);
    }
}
