using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Interface.Presentation.Services
{
    public static class PathService
    {
        private static string serverPath = HttpContext.Current.Server.MapPath("~/");

        public static string applicationImagePath = serverPath + "/Content/Images/Application/";
        public static string userImagePath = serverPath + ("/Content/Images/User/");
    }
}