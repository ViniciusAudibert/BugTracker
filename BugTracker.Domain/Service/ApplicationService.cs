using BugTracker.Domain.Entity;
using BugTracker.Domain.Interface.Repository;
using BugTracker.Domain.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Service
{
    public class ApplicationService : IApplicationService
    {
        IApplicationRepository applicationRepository;

        public ApplicationService(IApplicationRepository applicationRepository)
        {
            this.applicationRepository = applicationRepository;
        }

        public ICollection<Application> FindByIDUser(int id)
        {
            return applicationRepository.FindByIDUser(id);
        }

        public Application FindByUrl(string url)
        {
            return applicationRepository.FindByUrl(url);
        }

        public Application FindByUrlAndUserHashCode(string url, string hashCode)
        {
            return applicationRepository.FindByUrlAndUserHashCode(url, hashCode);
        }
        
        public void Add(Application application)
        {
            applicationRepository.Add(application);
        }

        public Application FindById(int id)
        {
            return applicationRepository.FindById(id);
        }

        public void Edit(Application application)
        {
            applicationRepository.Edit(application);
        }

        public ICollection<Application> FindByName(String name)
        {
            return applicationRepository.FindByName(name);
        }

        public IEnumerable<dynamic> FindAppAndBugsByAppId(int id)
        {
            return applicationRepository.FindAppAndBugsByAppId(id);
        }

        public IEnumerable<dynamic> FindAppAndBugsByName(string name, int id)
        {
            return applicationRepository.FindAppAndBugsByName(name, id);
        }
    }
}
