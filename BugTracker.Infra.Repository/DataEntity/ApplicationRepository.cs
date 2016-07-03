using BugTracker.Domain.Interface.Repository;
using System.Linq;
using BugTracker.Domain.Entity;
using System.Collections.Generic;
using System;

namespace BugTracker.Infra.Repository.DataEntity
{
    public class ApplicationRepository : IApplicationRepository
    {
        public ICollection<Application> FindByIDUser(int IDUser)
        {
            using (var db = new DataContext())
            {
                return db.Application.Include("BugTrackers").Where(_ => _.IDUser == IDUser && _.Active == true).ToList();
            }
        }

        public void Add(Application application)
        {
            using (var db = new DataContext())
            {
                db.Entry<Application>(application).State = System.Data.Entity.EntityState.Added;
                db.Entry<User>(application.User).State = System.Data.Entity.EntityState.Unchanged;
                db.SaveChanges();
            }
        }

        public Application FindByUrl(string url)
        {
            using (var db = new DataContext())
            {
                return db.Application
                    .Include("User")
                    .AsNoTracking()
                    .FirstOrDefault(_ => _.Url == url);
            }

        }

        public Application FindByUrlAndUserHashCode(string url, string hashCode)
        {
            using (var db = new DataContext())
            {
                return db.Application
                    .Include("User")
                    .AsNoTracking()
                    .FirstOrDefault(_ => _.Url == url && _.User.HashCode == hashCode);
            }

        }

        public void Edit(Application application)
        {
            using (var db = new DataContext())
            {
                db.Entry<Application>(application).State = System.Data.Entity.EntityState.Modified;
                db.Entry<User>(application.User).State = System.Data.Entity.EntityState.Unchanged;
                db.SaveChanges();
            }
        }

        public Application FindById(int id)
        {
            using (var db = new DataContext())
            {
                return db.Application.AsNoTracking().FirstOrDefault(_ => _.IDApplication == id);
            }
        }

        public ICollection<Application> FindByName(string name)
        {
            using (var db = new DataContext())
            {
                return db.Application.Include("BugTrackers").Where(_ => _.Title.Contains(name) && _.Active == true).ToList();
            }
        }

        public IEnumerable<dynamic> FindAppAndBugsByAppId(int id)
        {
            using (var db = new DataContext())
            {
                return db.Application
                    .AsNoTracking()
                    .Where(_ => _.IDUser == id && _.Active == true)
                    .Select(
                        _ => new
                        {
                            AppName = _.Title,
                            AppId = _.IDApplication,
                            AppImage = _.Image,
                            LastTrack = _.BugTrackers.OrderByDescending(x => x.IDBugTracker).FirstOrDefault(),
                            TracksCountError = _.BugTrackers.Where(b => b.Status == BugTrackerStatus.ERROR && b.OccurredDate >= DateTime.Today).Count(),
                            TracksCountWarning = _.BugTrackers.Where(b => b.Status == BugTrackerStatus.WARNING && b.OccurredDate >= DateTime.Today).Count()                            
                        }
                    ).ToList();
                
            }
        }

        public IEnumerable<dynamic> FindAppAndBugsByName(String name, int id)
        {
            return FindAppAndBugsByAppId(id).Where(b => b.AppName.Contains(name)).ToList();
        }
    }
}
