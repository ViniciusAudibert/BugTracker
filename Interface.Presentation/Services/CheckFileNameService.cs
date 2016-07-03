using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Interface.Presentation.Services
{
    public static class CheckFileNameService
    {
        public static string CheckUserImageName(string fileName)
        {
            string path = PathService.userImagePath + fileName;

            while (File.Exists(path))
            {
                fileName = fileName.Insert(0, "C");
                path = PathService.userImagePath + fileName;
            }

            return fileName;
        }

        public static string CheckApplicationImageName(string fileName)
        {
            string path = PathService.applicationImagePath + fileName;

            while (File.Exists(path))
            {
                fileName = fileName.Insert(0, "C");
                path = PathService.applicationImagePath + fileName;
            }

            return fileName;
        }
    }
}