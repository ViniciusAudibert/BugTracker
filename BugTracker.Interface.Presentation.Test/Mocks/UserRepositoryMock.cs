using BugTracker.Domain.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTracker.Domain.Entity;

namespace BugTracker.Interface.Presentation.Test.Mocks
{
    public class UserRepositoryMock : IUserRepository
    {
        public List<User> Users;

        public UserRepositoryMock()
        {
            Users = new List<User>();
            var userTest = new User();
            var user1 = new User(1, "User Test 1", "teste@1", "test", "default", "hash", null, true, false);
            var user2 = new User(2, "User Test 2", "teste@2", "test", "default", "hash", null, true, false);

            Users.Add(user1);
            Users.Add(user2);
        }

        public void ActiveAccount(User user)
        {
            var userToActive = Users.FirstOrDefault( x => x.IDUser == user.IDUser);
        
            Users.Remove(user);
            userToActive.AccountConfirmed = true;
            Users.Add(userToActive);
        
            
        }

        public User Add(User user)
        {
            Users.Add(user);

            return user;
        }

        public User FindByAuthentication(string email, string password)
        {
            return Users.FirstOrDefault(x => x.Email == email && x.Password == password);
        }

        public User FindByEmail(string email)
        {
            return Users.FirstOrDefault(x => x.Email == email);
        }

        public User FindById(int id)
        {
            return Users.FirstOrDefault(x => x.IDUser == id);
        }

        public User Update(User user)
        {
            var userToEdit = this.FindById(user.IDUser);
            Users.Remove(userToEdit);
            userToEdit = user;
            Users.Add(userToEdit);
            return userToEdit;
        }
    }
}
