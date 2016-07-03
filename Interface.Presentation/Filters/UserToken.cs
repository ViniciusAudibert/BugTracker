using Interface.Presentation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Interface.Presentation.Filters
{
    public class UserToken : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return UserSessionService.IsLogged;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            bool isLogged = this.AuthorizeCore(filterContext.HttpContext);

            if (!isLogged)
            {
                filterContext.Result = new RedirectToRouteResult(
                                   new RouteValueDictionary
                                   {
                                       { "action", "Index" },
                                       { "controller", "Login" }
                                   });
            }
        }
    }
}