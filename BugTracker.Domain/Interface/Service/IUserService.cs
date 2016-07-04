using BugTracker.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Interface.Service
{
    public interface IUserService
    {
        User FindById(int id);
        User FindByEmail(string email);
        User FindByAuthentication(string email, string password);
        User Add(User user);
        User Update(User user);
        void UpdatePassword(User user, string password);
        void ActiveAccount(User user);
        bool ComparePassword(String oldPassword, String newPassword);
        void UpdateImage(int idUser, string imagePath);
    }
}
