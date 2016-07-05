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
    public static class UserRecoverPasswordMail
    {
        private static IMailService mail = MailServiceInjection.Create();

        private static IUserRecoveryService userRecoveryService = UserRecoveryServiceInjection.Create();

        public static void SendTo(User user)
        {
            userRecoveryService.Add(new ForgotPassword(user, DateTime.Now, UserRecoverPasswordMail.SendTo(user.Email)));
        }

        private static string SendTo(string mailTo)
        {
            string code = Guid.NewGuid().ToString() + new Random().Next(1000).ToString();

            var model = new EmailModel()
            {
                Link = "http://localhost:58173/userrecovery/code?code=" + code,
                Body = "A request of a new password was made in BugTracker website, click on the button bellow to change your password"
            };

            string body = RazorViewToString.EmailToString(model);

            mail.Send(mailTo, "Forgot password of BugTracker account", body, true);

            return code;
        }
    }
}