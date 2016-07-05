using BugTracker.Domain.Exceptions;
using BugTracker.Domain.Interface.Repository;
using BugTracker.Domain.Interface.Service;
using System.Collections.Generic;

namespace BugTracker.Domain.Service
{
    public class BugTrackerService : IBugTrackerService
    {
        private IBugTrackerRepository bugTrackerRepository;
        

        public BugTrackerService(IBugTrackerRepository bugTrackerRepository)
        {
            this.bugTrackerRepository = bugTrackerRepository;
            
        }

        public bool Add(Entity.BugTracker bugTracker)
        {
            if (bugTracker.ValidateTags())
            {
                bugTrackerRepository.Add(bugTracker);

                var existsTagMaster = bugTracker.ContainsSpecialTag();

                if (existsTagMaster != null)
                {
                    return true;
                }
            }
            else
            {
                throw new TagVeryLargeException();
            }
            return false;
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
