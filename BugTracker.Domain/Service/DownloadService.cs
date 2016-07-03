using BugTracker.Domain.Entity;
using BugTracker.Domain.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Service
{
    public class DownloadService : IDownloadService
    {
        private string path;
        private string libraryFileName = "bugTracker.js";
        private string fileUser; 
        private IFileService fileService;

        public DownloadService (IFileService fileService)
	    {
            this.fileService = fileService;
	    }

        public string GetFileName()
        {
            return this.libraryFileName;
        }

        public byte[] GetFileByte(User user)
        {
            return fileService.ReadAllBytes(fileUser + user.IDUser + ".js");
        }

        public void SetPath(string path)
        {
            this.path = path;
            this.fileUser = path + libraryFileName.Replace(".js", "") + "_";
        }
        
        public void CreateFileForUser(User user)
        {
            var libraryCode =
                fileService.ReadAllLines(path + libraryFileName)
                .Replace("hash_code_user", "'" + user.HashCode + "'");

            fileService.Write(fileUser + user.IDUser + ".js", libraryCode);
        }
    }
}
