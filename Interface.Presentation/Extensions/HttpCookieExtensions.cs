using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Interface.Presentation.Extensions
{
    public static class HttpCookieExtensions
    {
        public static void TimeOut(this HttpCookie cookie)
        {
            cookie.Expires = DateTime.Now.AddDays(-1d);
        }
    }
}