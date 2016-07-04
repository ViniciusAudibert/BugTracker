using Interface.Presentation.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace BugTracker.Interface.Presentation.Test.Mocks
{
    public static class HttpContextMock
    {

        private static HttpRequest HttpRequest;
        private static StringWriter StringWriter;
        private static HttpResponse HttpResponse;
        private static HttpContext HttpContext;

        public static void CreateMockContext()
        {
            HttpRequest = new HttpRequest("", "http://stackoverflow/", "");
            StringWriter = new StringWriter();
            HttpResponse = new HttpResponse(StringWriter);
            HttpContext = new HttpContext(HttpRequest, HttpResponse);

            var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
                                            new HttpStaticObjectsCollection(), 10, true,
                                            HttpCookieMode.AutoDetect,
                                            SessionStateMode.InProc, false);

            UserSessionService.SetContext(HttpContext);

            HttpContext.Items["AspSession"] = typeof(HttpSessionState).GetConstructor(
                            BindingFlags.NonPublic | BindingFlags.Instance,
                                null, CallingConventions.Standard,
                                new[] { typeof(HttpSessionStateContainer) },
                                null)
                        .Invoke(new object[] { sessionContainer});
        } 
    }
}
