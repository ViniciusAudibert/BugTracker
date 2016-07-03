using BugTracker.Domain.Interface.Service;
using BugTracker.InfraInfra.Service;

namespace Interface.Presentation.Services
{
    public static class MailServiceInjection
    {
        public static IMailService Create()
        {
            return new MailService();
        }
    }
}