using BugTracker.Domain.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTracker.Domain.Entity;

namespace BugTracker.Interface.Presentation.Test.Mocks
{
    class UserRepositoryMock : IUserRepository
    {
        public List<User> Users;

        public UserRepositoryMock()
        {
            Users = new List<User>();
            var userTest = new User();
            var user1 = new User(1, "User Test 1", "teste@1", "test", "default", "hash", null, true, true);
            var user2 = new User(2, "User Test 2", "teste@2", "test", "default", "hash", null, true, true);

            Users.Add(user1);
            Users.Add(user2);
        }

        public void ActiveAccount(User user)
        {
            throw new NotImplementedException();
        }

        public User Add(User user)
        {
            throw new NotImplementedException();
        }

        public User FindByAuthentication(string email, string password)
        {
            throw new NotImplementedException();
        }

        public User FindByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public User FindById(int id)
        {
            return Users.FirstOrDefault(x => x.IDUser == id);
        }

        public User Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
