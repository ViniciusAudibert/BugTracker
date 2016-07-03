using BugTracker.Domain.Entity;
using BugTracker.Domain.Interface.Repository;
using System.Linq;
using System;

namespace BugTracker.Infra.Repository.DataEntity
{
    public class UserRepository : IUserRepository
    {
        public User FindById(int id)
        {
            using (var db = new DataContext())
            {
                return db.User.AsNoTracking().FirstOrDefault(_ => _.IDUser == id);
            }
        }

        public User FindByEmail(string email)
        {
            using (var db = new DataContext())
            {
                return db.User.AsNoTracking().FirstOrDefault(_ => _.Email == email);
            }
        }

        public User Add(User user)
        {
            using (var db = new DataContext())
            {
                db.Entry<User>(user).State = System.Data.Entity.EntityState.Added;

                db.SaveChanges();

                return user;
            }
        }

        public User Update(User user)
        {
            using (var db = new DataContext())
            {
                db.Entry<User>(user).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();

                return user;
            }
        }

        public User FindByAuthentication(string email, string password)
        {
            using (var db = new DataContext())
            {
                return db.User.AsNoTracking().FirstOrDefault(_ => _.Email == email && _.Password == password);
            }
        }

        public void ActiveAccount(User user)
        {
            using (var db = new DataContext())
            {
                user.AccountConfirmed = true;

                db.Entry<User>(user).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();
            }
        }
    }
}
