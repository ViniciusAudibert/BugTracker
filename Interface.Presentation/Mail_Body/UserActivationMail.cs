using BugTracker.Domain.Entity;
using BugTracker.Domain.Interface.Service;
using Interface.Presentation.Controllers;
using Interface.Presentation.Models.Email;
using Interface.Presentation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Interface.Presentation.Mail_Body
{
    public static class UserActivationMail
    {
        private static IMailService mail = MailServiceInjection.Create();

        private static IActivationService activationService = ActivationServiceInjection.Create();

        public static void SendTo(User user)
        {
            activationService.Add(new BugTracker.Domain.Entity.UserActivation(SendTo(user.Email), user.IDUser, user));
        }

        private static string SendTo(string mailTo)
        {
            string code = Guid.NewGuid().ToString() + new Random().Next(1000).ToString();

            var model = new EmailModel() {
                Link = "http://localhost:58173/activation/code?code=" + code,
                Body = "This email is about a new account you made in BugTracker website, click on the button bellow to activate your account"};

            string body = EmailService.EmailRazorViewToString(model);

            mail.Send(mailTo, "Activation of BugTracker account", body, true);

            return code;
        }
    }
}