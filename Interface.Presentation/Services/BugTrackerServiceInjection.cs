using BugTracker.Domain.Service;
using BugTracker.Infra.Repository.DataEntity;
using BugTracker.InfraInfra.Service;

namespace Interface.Presentation.Services
{
    public static class BugTrackerServiceInjection
    {
        public static BugTrackerService Create()
        {
            return new BugTrackerService(new BugTrackerRepository(), new MailService());
        }
    }
}