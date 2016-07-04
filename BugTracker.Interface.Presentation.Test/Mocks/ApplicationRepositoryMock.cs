using BugTracker.Domain.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTracker.Domain.Entity;

namespace BugTracker.Interface.Presentation.Test.Mocks
{
    public class ApplicationRepositoryMock : IApplicationRepository
    {

        public List<Application> AppsList;

        public ApplicationRepositoryMock()
        {


            AppsList = new List<Application>();
            var BugList = new List<Domain.Entity.BugTracker>();

            var userTest = new User("user test", "test@amil", "password", "default", "test hash", null, true, true);
            var app1 = new Application(1, "Test1", "app to test", "www.test1", true, "default", "tag", 1, userTest, BugList);
            var app2 = new Application(2, "Test2", "app to test", "www.test2", true, "default", "tag", 1, userTest, BugList);
            
            

            AppsList.Add(app1);
            AppsList.Add(app2);

        }

        public void Add(Application application)
        {
            this.AppsList.Add(application);
        }

        public void Edit(Application application)
        {
            var indexInList = AppsList.FindIndex(x => x.IDApplication == application.IDApplication);
            AppsList[indexInList] = application;

        }

        public IEnumerable<dynamic> FindAppAndBugsByAppId(int id)
        {
            return AppsList.Where(x => x.IDApplication == id).
                Select( x => new {
                    AppName = x.Title,
                    AppId = x.IDApplication,
                    AppImage = x.Image,
                    LastTrack = x.BugTrackers.OrderByDescending(_ => _.IDBugTracker).FirstOrDefault(),
                    TracksCountError = x.BugTrackers.Where(b => b.Status == BugTrackerStatus.ERROR && b.OccurredDate == DateTime.Today).Count(),
                    TracksCountWarning = x.BugTrackers.Where(b => b.Status == BugTrackerStatus.WARNING && b.OccurredDate == DateTime.Today).Count()
                }
            ).ToList();
        }

        public IEnumerable<dynamic> FindAppAndBugsByName(string name, int id)
        {
            throw new NotImplementedException();
        }

        public Application FindById(int id)
        {
            return AppsList.FirstOrDefault(x => x.IDApplication == id);
        }

        public ICollection<Application> FindByIDUser(int id)
        {
            return AppsList.Where(x => x.IDUser == id).ToList();
        }

        public ICollection<Application> FindByName(string name)
        {
            return AppsList.Where(x => x.Title.Contains(name)).ToList();
        }

        public Application FindByUrl(string url)
        {
            return AppsList.FirstOrDefault(x => x.Url == (url));
        }

        public Application FindByUrlAndUserHashCode(string url, string hashCode)
        {
            return AppsList.FirstOrDefault(x => x.Url == url && x.User.HashCode == hashCode);
        }
    }
}
