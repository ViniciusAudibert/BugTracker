using BugTracker.Domain.Exceptions;
using BugTracker.Domain.Interface.Repository;
using BugTracker.Domain.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Service
{
    public class BugTrackerService : IBugTrackerService
    {
        private IBugTrackerRepository bugTrackerRepository;
        private IMailService emailService;

        public BugTrackerService(IBugTrackerRepository bugTrackerRepository, IMailService emailService)
        {
            this.bugTrackerRepository = bugTrackerRepository;
            this.emailService = emailService;
        }

        public void Add(Entity.BugTracker bugTracker)
        {
            if (bugTracker.ValidateTags())
            {
                bugTrackerRepository.Add(bugTracker);

                var existeTagMaster = bugTracker.ContainsSpecialTag();

                if (existeTagMaster != null)
                {
                    this.emailService.Send
                    (
                        bugTracker.Application.User.Email,
                        "Erro master",
                        "Erro no e-mail",
                        false
                    );
                }
            }
            else
            {
                throw new TagVeryLargeException();
            }
        }

        public ICollection<Entity.BugTracker> FindByIDApplication(int id)
        {
            return bugTrackerRepository.FindByIDApplication(id);
        }

        public ICollection<Entity.BugTracker> FindByApplicationPagined(Entity.BugTrackerFilter filter)
        {
            return bugTrackerRepository.FindByApplicationPagined(filter);
        }

        public IList<dynamic> GetGraphicModelByIdApplication(int id) 
        {
            return bugTrackerRepository.GetGraphicModelByIdApplication(id);
        }

        public IList<dynamic> GetCountBugsByApp(Entity.BugTrackerFilter filter)
        {
            return bugTrackerRepository.GetCountBugsByApp(filter);
        }
    }
}
