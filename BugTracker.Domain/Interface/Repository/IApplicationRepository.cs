using BugTracker.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Interface.Repository
{
    public interface IApplicationRepository
    {
        ICollection<Application> FindByIDUser(int id);
        Application FindByUrl(string url);
        void Add(Application application);
        void Edit(Application application);
        Application FindById(int id);
        Application FindByUrlAndUserHashCode(string url, string hashCode);
        ICollection<Application> FindByName(string name);
        IEnumerable<dynamic> FindAppAndBugsByAppId(int id);
        IEnumerable<dynamic> FindAppAndBugsByName(string name, int id);

    }
}
