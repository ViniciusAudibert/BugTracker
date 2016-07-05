using BugTracker.Domain.Entity;
using BugTracker.Domain.Interface.Service;
using Interface.Presentation.App_Start;
using Interface.Presentation.Extensions;
using Interface.Presentation.Filters;
using Interface.Presentation.Mail_Body;
using Interface.Presentation.Models.BugTracker;
using Interface.Presentation.Services;
using iTextSharp.text.pdf;
using iTextSharp.xmp;
using System.IO;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Domain = BugTracker.Domain;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf.parser;

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

        public BugTrackerController(IBugTrackerService bugTrackerService, IApplicationService applicationService)
        {
            this.bugTrackerService = bugTrackerService;
            this.applicationService = applicationService;
        }

        [HttpPost]
        [AllowOriginAttributeConfig]
        public JsonResult Add(BugTrackerPostModel bugTrackerPostModel)
        {
            var request = HttpContext.Request;
            JsonResult returnJson;

            var application =
                applicationService.FindByUrlAndUserHashCode(
                HttpContext.Request.UrlReferrer.Host,
                    bugTrackerPostModel.HashCode
                 );

            if (application == null)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                returnJson = Json(new { error = "Domain invalid or Libray broke. Verify your domain in painel and download again library." });
            }

            if (!ModelState.IsValid)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                returnJson = Json(new { error = string.Join("; ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)) });
            }

            try
            {
                var sendEmail = bugTrackerService.Add(
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

                if (sendEmail)
                {
                    TagMasterMail.SendTo(application.User.Email, application.Title);
                }

                returnJson = Json(new { msg = "Success!" });
            }
            catch (Domain.Exceptions.TagVeryLargeException e)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                returnJson = returnJson = Json(new { error = e.ToString() });
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                returnJson = returnJson = Json(new { error = e.ToString() });
            }

            return returnJson;
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

        [HttpPost]
        public FileResult ExportBugsForPdf(BugTrackerFilter filter)
        {
            var bugTrackers = bugTrackerService.FindByApplicationFilter(filter);

            Byte[] bytes = ExportFileService.TableHtmlToPdf(bugTrackers).ToArray();

            return File(
                bytes,
                System.Net.Mime.MediaTypeNames.Application.Octet,
                DateTime.Now.ToString("dd_MM/yyyy") + "_bugs.pdf"
            );
        }

        [HttpPost]
        public FileResult ExportBugsForTxt(BugTrackerFilter filter)
        {
            var bugTrackers = bugTrackerService.FindByApplicationFilter(filter);

            Stream streamFile = ExportFileService.TableHtmlToTxt(bugTrackers);



            return File(
                streamFile,
                System.Net.Mime.MediaTypeNames.Application.Octet,
                DateTime.Now.ToString("dd_MM/yyyy") + "_bugs.txt"
            );
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

        public static string ExtractTextFromPdf(string path)
        {
            ITextExtractionStrategy its = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();

            using (PdfReader reader = new PdfReader(path))
            {
                StringBuilder text = new StringBuilder();

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    string thePage = PdfTextExtractor.GetTextFromPage(reader, i, its);
                    string[] theLines = thePage.Split('\n');
                    foreach (var theLine in theLines)
                    {
                        text.AppendLine(theLine);
                    }
                }
                return text.ToString();
            }
        }
    }
}
