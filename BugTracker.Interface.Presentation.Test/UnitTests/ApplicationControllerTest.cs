using BugTracker.Domain.Entity;
using BugTracker.Domain.Service;
using BugTracker.Interface.Presentation.Test.Mocks;
using Interface.Presentation.Controllers;
using Interface.Presentation.Models;
using Interface.Presentation.Models.User;
using Interface.Presentation.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BugTracker.Interface.Presentation.Test
{
    [TestClass]
    public class ApplicationControllerTest
    {

        private ApplicationController ApplicationController;
        private ApplicationRepositoryMock ApplicationRepositoryMock;
        private UserRepositoryMock UserRepositoryMock;
        

        [TestInitialize()]
        public void SetController()
        {
            HttpContextMock.CreateMockContext();

            var loggedUSerTest = new LoggedUserViewModel(new User(1, "test", "email", "password", "image", "hash", new List<Application>(), true, true));
            UserSessionService.CreateSession(loggedUSerTest);
            
            UserRepositoryMock = new UserRepositoryMock();
            ApplicationRepositoryMock = new ApplicationRepositoryMock();
            ApplicationController = new ApplicationController(new ApplicationService(ApplicationRepositoryMock), new UserService(UserRepositoryMock));
        }

        [TestMethod]
        public void RegisterAppToNewApp()
        {
            var result = ApplicationController.RegisterApp(null) as ViewResult;
            Assert.AreEqual("register-app", result.ViewName);
        }

        [TestMethod]
        public void RegisterAppToEditApp()
        {
            var view = ApplicationController.RegisterApp(1) as ViewResult;
            var modelResult = (ApplicationModel)view.ViewData.Model;

            var app = ApplicationRepositoryMock.FindById(1);
            var modelExpected = new ApplicationModel();

            modelExpected.Id = app.IDApplication;
            modelExpected.Title = app.Title;
            modelExpected.Description = app.Description;
            modelExpected.Tag = app.SpecialTag;
            modelExpected.Url = app.Url;
            modelExpected.Icon = app.Image;

            Assert.AreEqual(modelResult.ToString(), modelExpected.ToString());

        }

        [TestMethod]
        public void NewEdiToAddNewApp()
        {
            var appToAdd = new ApplicationModel();
            var countAppList = ApplicationRepositoryMock.AppsList.Count;
            Assert.AreEqual( countAppList, 2);

            ApplicationController.NewEditApp(appToAdd);
            countAppList = ApplicationRepositoryMock.AppsList.Count;
            Assert.AreEqual(countAppList, 3);
            
        }

        
        [TestMethod]
        public void NewEdiToEditApp()
        {
            var appToEdit = new ApplicationModel();
            appToEdit.Id = 1;
            appToEdit.Title = "edited app";

            ApplicationController.NewEditApp(appToEdit);

            Assert.IsNotNull(ApplicationRepositoryMock.FindByName("edited app"));

        }
        
        [TestMethod]
        public void DetailsValidIdUserApp()
        {
            
            var model = ApplicationController.DetailsApp(1) as ViewResult;
            var app = model.ViewData.Model;

            Assert.AreEqual(ApplicationRepositoryMock.FindById(1), app);

        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DetailsIvalidIdUserApp()
        {
            var model = ApplicationController.DetailsApp(999) as ViewResult;
            var app = model.ViewData.Model;
        }

        [TestMethod]
        public void DeleteApp()
        {
            var appToDelete = new ApplicationModel();
            appToDelete.Id = 1;

            Assert.IsTrue(ApplicationRepositoryMock.FindById(1).Active);
            ApplicationController.DeleteApp(1);
            Assert.IsFalse(ApplicationRepositoryMock.FindById(1).Active);

        }
        
    }
}
