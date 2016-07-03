using BugTracker.Domain.Entity;
using BugTracker.Domain.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Infra.Repository.DataEntity
{
    public class UserRecoveryRepository : IUserRecoveryRepository
    {
        public void Add(ForgotPassword userRecovery)
        {
            using (var db = new DataContext())
            {
                db.Entry<ForgotPassword>(userRecovery).State = System.Data.Entity.EntityState.Added;

                db.Entry<User>(userRecovery.RequestUser).State = System.Data.Entity.EntityState.Unchanged;

                db.SaveChanges();
            }
        }

        public ForgotPassword FindByCode(string code)
        {
            using (var db = new DataContext())
            {
                return db.UserRecovery.Include("RequestUser").FirstOrDefault(_ => _.HashCode == code);
            }
        }
    }
}
