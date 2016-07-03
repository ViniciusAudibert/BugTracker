using Interface.Presentation.Mail_Body;
using Interface.Presentation.Models.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Interface.Presentation.Controllers
{
    public class SendEmailController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Send(SendEmailModel emailModel)
        {
            if(ModelState.IsValid)
                SendEmailToBugtracker.SendEmail(emailModel);
            return RedirectToAction("Index","Home");
        }
    }
}