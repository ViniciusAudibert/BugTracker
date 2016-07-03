using BugTracker.Domain.Entity;
using BugTracker.Domain.Interface.Service;
using Interface.Presentation.App_Start;
using Interface.Presentation.Extensions;
using Interface.Presentation.Filters;
using Interface.Presentation.Models.BugTracker;
using Interface.Presentation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Domain = BugTracker.Domain;


namespace Interface.Presentation.Controllers
{
    public class BugTrackerController : Controller
    {
        private IBugTrackerService bugTrackerService;
        private IApplicationService applicationService;

        public BugTrackerController()
        {
            bugTrackerService = BugTrackerServiceInjection.Create();
            applicationService = ApplicationServiceInjection.Create();
        }

        [HttpPost]
        [AllowOriginAttributeConfig]
        public JsonResult Add(BugTrackerPostModel bugTrackerPostModel)
        {
            var request = HttpContext.Request;

            var application = 
                applicationService.FindByUrlAndUserHashCode(
                    HttpContext.Request.Url.Host,
                    bugTrackerPostModel.HashCode
                 );

            if (application == null)
            {
                throw new HttpException(
                    (int)HttpStatusCode.BadRequest,
                    "Domain invalid or Libray broke. Verify your domain in painel and download again library."
                );
            }

            if (!ModelState.IsValid)
            {
                throw new HttpException(
                    (int)HttpStatusCode.BadRequest,
                    string.Join("; ", ModelState.Values
                        .SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage)
                    )
                );
            }

            try
            {
                bugTrackerService.Add(
                    new Domain.Entity.BugTracker(
                        application,
                        bugTrackerPostModel.Status,
                        bugTrackerPostModel.Trace,
                        DateTime.Now,
                        bugTrackerPostModel.ToTrackerTag(),
                        new Domain.Entity.Browser(request.Browser.Browser, request.Browser.Version),
                        new Domain.Entity.OperationalSystem(request.Browser.Platform)
                    )
                );

                return Json(new { msg = "Success!" });
            }
            catch (Domain.Exceptions.TagVeryLargeException e)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, e.ToString());
            }
            catch (Exception e)
            {
                throw new HttpException((int)HttpStatusCode.InternalServerError, e.ToString());
            }
        }

        [HttpPost]
        public ActionResult GetBugTrackerPaginedByApp(BugTrackerFilter filter)
        {
            var bugTrackers = bugTrackerService.FindByApplicationPagined(filter);

            return PartialView("_bug-trackers-application", bugTrackers.FromModel());
        }

        [HttpPost]
        public JsonResult GetCountBugTrackerByApp(BugTrackerFilter filter)
        {
            return formatReturn(bugTrackerService.GetCountBugsByApp(filter));
        }

        [HttpGet]
        public JsonResult GetGraphicModelByIdApplication(int idApplication)
        {
            return formatReturn(bugTrackerService.GetGraphicModelByIdApplication(idApplication));
        }

        private JsonResult formatReturn(IList<dynamic> data)
        {
            return Json(
                new
                {
                    data = data,
                    status = Enum.GetNames(typeof(Domain.Entity.BugTrackerStatus))
                },
                JsonRequestBehavior.AllowGet
            );
        }
    }
}
