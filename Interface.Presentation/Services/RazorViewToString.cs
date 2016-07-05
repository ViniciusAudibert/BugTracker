using Interface.Presentation.Helpers;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Interface.Presentation.Services
{
    public static class RazorViewToString
    {
        public static string EmailToString(object model)
        {
            return ViewToString(model, "~/Views/Shared/_LayoutEmail.cshtml");
        }

        public static string TableTrackingToString(object model)
        {
            return ViewToString(model, "~/Views/BugTracker/_export.cshtml");
        }

        private static string ViewToString(object model, string viewPath)
        {
            var stringWriter = new StringWriter();

            var context = new HttpContextWrapper(HttpContext.Current);

            var routeData = new RouteData();

            var controllerContext = new ControllerContext(new RequestContext(context, routeData), new BaseControllerEmpty());

            var razor = new RazorView(controllerContext, viewPath, null, false, null);

            razor.Render(new ViewContext(controllerContext, razor, new ViewDataDictionary(model), new TempDataDictionary(), stringWriter), stringWriter);

            return stringWriter.ToString();
        }
    }
}