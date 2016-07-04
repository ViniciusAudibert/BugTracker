using BugTracker.Domain.Service;
using BugTracker.Interface.Presentation.Test.Mocks;
using Interface.Presentation.Controllers;
using Interface.Presentation.Extensions;
using Interface.Presentation.Models.User;
using Interface.Presentation.Services;
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
    public class UserControllerTest
    {
        private UserRepositoryMock UserRepositoryMock;
        private UserService UserService;
        private UserController UserController;
        private ApplicationRepositoryMock ApplicationRepositoryMock;
        private ApplicationService ApplicationService;

        [TestInitialize]
        public void setController()
        {
            HttpContextMock.CreateMockContext();
            this.UserRepositoryMock = new UserRepositoryMock();
            this.ApplicationRepositoryMock = new ApplicationRepositoryMock();
            this.UserService = new UserService(UserRepositoryMock);
            this.ApplicationService = new ApplicationService(ApplicationRepositoryMock);
            this.UserController = new UserController(ApplicationService, UserService);
        }


        [TestMethod]
        public void AppListOfValidUser()
        {
            var userExistent = UserService.FindById(1);
            var loggedUser = new LoggedUserViewModel(userExistent);

            UserSessionService.CreateSession(loggedUser);
            var viewResult = UserController.Index() as ViewResult;
            var expectedModel = ApplicationService.FindAppAndBugsByAppId(1).toApplicationAndBugsViewModel();

            Assert.AreEqual(viewResult.ViewData.Model.ToString(), expectedModel.ToString());
        }

        [TestMethod]
        public void AppListOfInexistentUser()
        {
            var loggedUser = new LoggedUserViewModel(999,"inexistent", "invalid@test", "default", null, true);

            UserSessionService.CreateSession(loggedUser);
            var viewResult = UserController.Index() as ViewResult;

            var expectedModel = ApplicationService.FindAppAndBugsByAppId(999).toApplicationAndBugsViewModel();

            Assert.AreEqual(viewResult.ViewData.Model.ToString(), expectedModel.ToString());
        }

        
    }
}
