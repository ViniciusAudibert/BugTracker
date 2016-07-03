using BugTracker.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Interface.Repository
{
    public interface IUserRepository
    {
        User FindById(int id);
        User FindByEmail(string email);
        User FindByAuthentication(string email, string password);
        User Add(User user);
        User Update(User user);
        void ActiveAccount(User user);
    }
}
