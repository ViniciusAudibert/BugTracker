using BugTracker.Domain.Interface.Service;
using Interface.Presentation.Models.Email;
using Interface.Presentation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Interface.Presentation.Mail_Body
{
    public static class SendEmailToBugtracker
    {
        private static IMailService mail = MailServiceInjection.Create();

        private const string BUGTRCKER_EMAIL = "crescer.bugtraker@gmail.com";

        public static void SendEmail(SendEmailModel emailModel)
        {
            string body = String.Format("The user {0} {1}, with email \"{2}\" sent a message\n\t{3}",
                emailModel.FirstName,emailModel.LastName,emailModel.Email,emailModel.Message);

            mail.Send(BUGTRCKER_EMAIL, "User message", body, false);
        }
    }
}