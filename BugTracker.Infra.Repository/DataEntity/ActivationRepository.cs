using BugTracker.Domain.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTracker.Domain.Entity;

namespace BugTracker.Infra.Repository.DataEntity
{
    public class ActivationRepository : IActivationRepository
    {
        public UserActivation FindByCode(string code)
        {
            using (var db = new DataContext())
            {
                return db.Activation
                    .Include("User")
                    .FirstOrDefault(_ => _.HashCode == code);
            }
        }

        public void Add(UserActivation activation)
        {
            using (var db = new DataContext())
            {
                db.Entry<UserActivation>(activation).State = System.Data.Entity.EntityState.Added;
                db.Entry<User>(activation.User).State = System.Data.Entity.EntityState.Unchanged;
                db.SaveChanges();
            }
        }

        public void Remove(int id)
        {
            using (var db = new DataContext())
            {
                UserActivation activation = db.Activation.Find(id);

                db.Entry<UserActivation>(activation).State = System.Data.Entity.EntityState.Deleted;

                db.SaveChanges();
            }
        }
    }
}
