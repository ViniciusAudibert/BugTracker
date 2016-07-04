using BugTracker.Domain.Service;
using BugTracker.Interface.Presentation.Test.Mocks;
using Interface.Presentation.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BugTracker.Interface.Presentation.Test.UnitTests
{
    [TestClass]
    public class ActivationControllerTest
    {
        private ActivationController ActivationController;
        private ActivationRepositoryMock ActivationRepositoryMock;
        private UserRepositoryMock UserRepositoryMock;

        [TestInitialize]
        public void setController()
        {
            ActivationRepositoryMock = new ActivationRepositoryMock();
            UserRepositoryMock = new UserRepositoryMock();
            ActivationController = new ActivationController(new ActivationService(ActivationRepositoryMock), new UserService(UserRepositoryMock));
        }

        [TestMethod]
        public void ActiveAcountWithValidUser()
        {
            var userToActive = UserRepositoryMock.FindById(1);
            var validCode = "hash test 1";

            Assert.IsFalse(userToActive.AccountConfirmed);
            ActivationController.Code(validCode);
            Assert.IsTrue(userToActive.AccountConfirmed);
        }

        [TestMethod]
        public void ActiveAcountInvalidHash()
        {
            var invalidHash = "invalid hash";
            ActivationController.Code(invalidHash);

            Assert.IsFalse(UserRepositoryMock.FindById(1).AccountConfirmed);
            Assert.IsFalse(UserRepositoryMock.FindById(2).AccountConfirmed);
            
        }

    }
}
