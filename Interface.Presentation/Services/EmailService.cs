using Interface.Presentation.Helpers;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Interface.Presentation.Services
{
    public static class EmailService
    {
        public static string EmailRazorViewToString(object model)
        {
            var stringWriter = new StringWriter();
            var context = new HttpContextWrapper(HttpContext.Current);
            var routeData = new RouteData();
            var controllerContext = new ControllerContext(new RequestContext(context, routeData), new BaseControllerEmpty());
            var razor = new RazorView(controllerContext, "~/Views/Shared/_LayoutEmail.cshtml", null, false, null);
            razor.Render(new ViewContext(controllerContext, razor, new ViewDataDictionary(model), new TempDataDictionary(), stringWriter), stringWriter);
            return stringWriter.ToString();
        }
    }
}