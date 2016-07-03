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
    public class UserRecoveryServiceTest
    {

        private UserRecoveryRepositoryMock UserRecoveryRepositoryMock;
        private UserRecoveryService UserRecoveryService;

        [TestInitialize()]
        public void SetRepository()
        {
            this.UserRecoveryRepositoryMock = new UserRecoveryRepositoryMock();
            this.UserRecoveryService = new UserRecoveryService(UserRecoveryRepositoryMock);
        }

        [TestMethod]
        public void AddARecoveryUser()
        {
            var userForgot = new User();
            var forgotPassTest1 = new ForgotPassword(userForgot, DateTime.Today, "hash test 1");

            var countForgotList = UserRecoveryRepositoryMock.RecoverList.Count;

            Assert.AreEqual(countForgotList, 2);

            UserRecoveryService.Add(forgotPassTest1);
            countForgotList = UserRecoveryRepositoryMock.RecoverList.Count;
            Assert.AreEqual(countForgotList, 3);

        }

        [TestMethod]
        public void AddAndFindRecoveryUser()
        {
            var code = "hash test 1";
            var userForgot = new User();
            var forgotPassExpected = new ForgotPassword(userForgot, DateTime.Today, code);
            
            UserRecoveryService.Add(forgotPassExpected);
            var forgotPassObtained = UserRecoveryService.FindByCode(code);

            Assert.AreEqual(forgotPassExpected.ToString(), forgotPassObtained.ToString());

        }

        [TestMethod]
        public void FindRecoveryUserWithInvalidCode()
        {
            
            var userForgot = new User();
            var forgotPassTest1 = new ForgotPassword(userForgot, DateTime.Today, "hash test 1");

            UserRecoveryService.Add(forgotPassTest1);
            var forgotPassObtained = UserRecoveryService.FindByCode("invalid hash");

            Assert.IsNull(forgotPassObtained);

        }

        [TestMethod]
        public void FindRecoveryUserWithEmptyCode()
        {

            var userForgot = new User();
            var forgotPassTest2 = new ForgotPassword(userForgot, DateTime.Today, "hash test 1");

            UserRecoveryService.Add(forgotPassTest2);
            var forgotPassObtained = UserRecoveryService.FindByCode("");

            Assert.IsNull(forgotPassObtained);

        }

        [TestMethod]
        public void RecoveryUserOutOfDate()
        {
            var userForgot = new User();
            var validHash = "hash valid, but out of date";
            var outDate = DateTime.Today.AddDays(100);
            var forgotPassTest3 = new ForgotPassword(userForgot, outDate, validHash);

            UserRecoveryService.Add(forgotPassTest3);
            var forgotPassObtained = UserRecoveryService.FindByCode(validHash);

            Assert.IsNull(forgotPassObtained);

        }

    }
}
