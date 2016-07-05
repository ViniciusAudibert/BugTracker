using BugTracker.Domain.Entity;
using System;
using System.Collections.Generic;

namespace BugTracker.Domain.Interface.Repository
{
    public interface IBugTrackerRepository
    {
        ICollection<Domain.Entity.BugTracker> FindByIDApplication(int id);
        ICollection<Domain.Entity.BugTracker> FindByApplicationPagined(BugTrackerFilter filter);
        ICollection<Domain.Entity.BugTracker> FindByApplicationFilter(BugTrackerFilter filter);
        IList<dynamic> GetGraphicModelByIdApplication(int id);
        IList<dynamic> GetCountBugsByApp(BugTrackerFilter filter);
        void Add(Domain.Entity.BugTracker bugTracker);
    }
}
