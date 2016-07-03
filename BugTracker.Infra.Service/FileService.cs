using BugTracker.Domain.Interface.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Infra.Service
{
    public class FileService : IFileService
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public string ReadAllLines(string path)
        {
            return File.ReadAllText(path);
        }

        public void Write(string path, string body)
        {
            using (StreamWriter w = File.AppendText(path))
            {
                w.WriteLine(body);
                w.Close();
            }
        }

        public byte[] ReadAllBytes(string path)
        {
            return File.ReadAllBytes(path);
        }
    }
}
