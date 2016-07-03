using BugTracker.Domain.Entity;
using BugTracker.Domain.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BugTracker.Interface.Presentation.Test.Mocks
{
    public class BugTrackerRepositoryMock : IBugTrackerRepository
    {

        public List<Domain.Entity.BugTracker> BugsList;

        public BugTrackerRepositoryMock()
        {
            var tagTest1 = new BugTrackerTag("error tag");
            var tagTest2 = new BugTrackerTag("warnning tag");
            var tagsList = new List<BugTrackerTag>();
            tagsList.Add(tagTest1);
            tagsList.Add(tagTest2);
            
            var bugTest1 = new Domain.Entity.BugTracker(null, BugTrackerStatus.ERROR, "error test", DateTime.Today, tagsList, null, null);
            var bugTest2 = new Domain.Entity.BugTracker(null, BugTrackerStatus.WARNING, "warnning test", DateTime.Today, tagsList, null, null);

            BugsList = new List<Domain.Entity.BugTracker>();
            BugsList.Add(bugTest1);
            BugsList.Add(bugTest2);
            
        }

        public void Add(Domain.Entity.BugTracker bugTracker)
        {
            BugsList.Add(bugTracker);
        }

        public ICollection<Domain.Entity.BugTracker> FindByApplicationPagined(BugTrackerFilter filter)
        {
            throw new NotImplementedException();
        }

        public ICollection<Domain.Entity.BugTracker> FindByIDApplication(int id)
        {
            return BugsList.Where(x => x.IDApplication == id).ToList();
        }

        public IList<dynamic> GetCountBugsByApp(BugTrackerFilter filter)
        {
            throw new NotImplementedException();
        }

        public IList<dynamic> GetGraphicModelByIdApplication(int id)
        {
            throw new NotImplementedException();
        }
    }
}
