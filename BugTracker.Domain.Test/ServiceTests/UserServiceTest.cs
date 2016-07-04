using BugTracker.Domain.Entity;
using BugTracker.Domain.Service;
using BugTracker.Interface.Presentation.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Test.ServiceTests
{
    [TestClass]
    public class UserServiceTest
    {

        private UserRepositoryMock UserRepositoryMock;
        private UserService UserService;


        [TestInitialize()]
        public void setRepository()
        {
            UserRepositoryMock = new UserRepositoryMock();
            UserService = new UserService(UserRepositoryMock);
        }

        [TestMethod]
        public void AddAUser()
        {
            var userToAdd = new User();
            var usersListCount = UserRepositoryMock.Users.Count;
            Assert.AreEqual(usersListCount, 2);
            UserService.Add(userToAdd);
            usersListCount = UserRepositoryMock.Users.Count;
            Assert.AreEqual(usersListCount, 3);
        }

        [TestMethod]
        public void EditAValidUser()
        {
            var oldUser = UserRepositoryMock.FindById(1);
            var userToEdit = new User(1, "name changed", "test@changed", "test password", "default", "hash test", null, true, true);
            Assert.AreEqual(oldUser.Name, "User Test 1");
            Assert.AreEqual(oldUser.Email, "teste@1");

            UserService.Update(userToEdit);
            var userEdited = UserRepositoryMock.FindById(1);
            Assert.AreEqual(userEdited.Name, "name changed");
            Assert.AreEqual(userEdited.Email, "test@changed");
        }

        [TestMethod]
        public void EditAInvalidUser()
        {
            var oldUser = UserService.FindById(1);
            var invalidUserToEdit = new User(999, "name changed", "test@changed", "test password", "default", "hash test", null, true, true);
            UserService.Update(invalidUserToEdit);

            var userUnchanged = UserService.FindById(1);
            Assert.AreEqual(oldUser, userUnchanged);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void EditANullUser()
        {
            var oldUser = UserService.FindById(1);
            UserService.Update(null);
            var userUnchanged = UserService.FindById(1);

            Assert.AreEqual(oldUser, userUnchanged);
        }

        [TestMethod]
        public void FindUserValidEmail()
        {
            var existentEmailInMock = "teste@1";
            var userFinded = UserService.FindByEmail(existentEmailInMock);
            var existentUserinMock = UserRepositoryMock.FindByEmail(existentEmailInMock);

            Assert.AreEqual(userFinded, existentUserinMock);
        }

        [TestMethod]
        public void FindUserInvalidEmail()
        {
            var invalidEmail = "invalid";
            var userFinded = UserService.FindByEmail(invalidEmail);
            
            Assert.IsNull(userFinded);
        }

        [TestMethod]
        public void ActiveAcountValidUSer()
        {
            var validUser = UserService.FindById(1);
            validUser.AccountConfirmed = false;
            UserService.Update(validUser);

            UserService.ActiveAccount(validUser);
            Assert.IsTrue(UserService.FindById(1).AccountConfirmed);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ActiveAcountinValidUSer()
        {
            var invalidUser = UserService.FindById(999);
            
            UserService.ActiveAccount(invalidUser);
            
        }
    }
}
