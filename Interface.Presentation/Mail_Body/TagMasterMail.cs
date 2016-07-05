using BugTracker.Domain.Entity;
using BugTracker.Domain.Interface.Service;
using Interface.Presentation.Models.Email;
using Interface.Presentation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Interface.Presentation.Mail_Body
{
    public static class TagMasterMail
    {
        private static IMailService mail = MailServiceInjection.Create();

        public static void SendTo(string mailTo, string title)
        {
            var model = new EmailModel()
            {
                Link = "http://localhost:58173/User",
                Body = "A critical bug war reported in your application \"" + title + "\" click the button bellow and check it"
            };

            string body = RazorViewToString.EmailToString(model);

            mail.Send(mailTo, "BugTracker reported a critical bug in one of your applications", body, true);
        }
    }
}