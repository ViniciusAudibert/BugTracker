using BugTracker.Domain.Entity;
using BugTracker.Domain.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Infra.Repository.DataEntity
{
    public class BugTrackerRepository : IBugTrackerRepository
    {
        public void Add(Domain.Entity.BugTracker bugTracker)
        {
            using (var db = new DataContext())
            {
                db.Entry<Domain.Entity.BugTracker>(bugTracker).State = System.Data.Entity.EntityState.Added;
                db.Entry<Domain.Entity.User>(bugTracker.Application.User).State = System.Data.Entity.EntityState.Unchanged;
                db.Entry<Domain.Entity.Application>(bugTracker.Application).State = System.Data.Entity.EntityState.Unchanged;
                db.SaveChanges();
            }
        }

        public ICollection<Domain.Entity.BugTracker> FindByIDApplication (int idApplication)
        {
            using (var db = new DataContext())
            {
                return db.BugTrucker
                    .AsNoTracking()
                    .Include("Tags")
                    .Where(_ => _.IDApplication == idApplication)
                    .ToList();
            }
        }

        public ICollection<Domain.Entity.BugTracker> FindByApplicationPagined(BugTrackerFilter filter)
        {
            using (var db = new DataContext())
            {
                var query = db.BugTrucker
                    .AsNoTracking()
                    .Include("Tags")
                    .OrderBy(_ => _.IDBugTracker)
                    .Skip(filter.Limit * (filter.Page - 1))
                    .Take(filter.Limit)
                    .Where(_ => _.IDApplication == filter.idApplication);

                return addFilter(query, filter.Trace, filter.Status).ToList();
            }
        }

        public IList<dynamic> GetCountBugsByApp(BugTrackerFilter filter)
        {
            using (var db = new DataContext())
            {
                var query = db.BugTrucker
                    .AsNoTracking()
                    .Where(_ => _.IDApplication == filter.idApplication);

                return addFilter(query, filter.Trace, filter.Status)
                        .GroupBy(x => x.Status)
                        .Select(s => new
                        {
                            Status = s.Key,
                            Count = s.Count()
                        }).ToArray();
            }
        }

        //TODO: Criar objeto para representar os filros
        private IEnumerable<Domain.Entity.BugTracker> addFilter(IEnumerable<Domain.Entity.BugTracker> query, string trace, List<Domain.Entity.BugTrackerStatus> status)
        {
            if (!string.IsNullOrEmpty(trace))
            {
                query = query.Where(_ => _.Description.Contains(trace));
            }

            if (status != null )
            {
                query = query.Where(_ => status.Contains(_.Status));
            }

            return query;
        }

        //TODO: dois métodos praticamente iguais.
        public IList<dynamic> GetGraphicModelByIdApplication(int id)
        {
            using (var db = new DataContext())
            {
                DateTime dateLimit = DateTime.Now.AddDays(-7);

                return db.BugTrucker
                    .AsNoTracking()
                    .Where(_ => _.IDApplication == id && _.OccurredDate >= dateLimit)
                    .GroupBy(x => x.Status)
                    .Select(s => new
                     {
                            Status = s.Key,
                            Count = s.Count()
                     })
                    .ToArray();
            }
        }
    }
}
