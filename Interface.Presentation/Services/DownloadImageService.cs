using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace Interface.Presentation.Services
{
    public static class DownloadImageService
    {
        public static string userImagePath = PathService.userImagePath;
        public static string applicationImagePath = PathService.applicationImagePath;

        public static string DownloadUserImage(string urlDownload,string title)
        {
            using (WebClient webClient = new WebClient())
            {
                byte[] data = webClient.DownloadData(urlDownload);

                using (MemoryStream memory = new MemoryStream(data))
                {
                    using (var image = Image.FromStream(memory))
                    {
                        if (ImageFormat.Jpeg.Equals(image.RawFormat))
                        {
                            string path = title + ".jpeg";
                            image.Save(userImagePath + path, ImageFormat.Jpeg);

                            return path;
                        }
                        else if (ImageFormat.Png.Equals(image.RawFormat))
                        {
                            string path = title + ".png";
                            image.Save(userImagePath + path, ImageFormat.Png);

                            return path;
                        }

                        return null;
                    }
                }
            }
        }
    }
}