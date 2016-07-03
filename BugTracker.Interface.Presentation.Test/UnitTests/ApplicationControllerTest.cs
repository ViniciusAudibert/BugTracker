using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interface.Presentation.Controllers;
using BugTracker.Domain.Service;
using BugTracker.Interface.Presentation.Test.Mocks;
using System.Web.Mvc;
using Interface.Presentation.Models;
using BugTracker.Domain.Entity;

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
            //TODO: fazer mock do httpcontext
            var appToAdd = new ApplicationModel();
            ApplicationController.NewEditApp(appToAdd);

            Assert.AreEqual(ApplicationRepositoryMock.AppsList.Count, 3);
            
        }

        [TestMethod]
        public void NewEdiToEditApp()
        {
            //TODO: fazer mock do httpcontext
            var appToEdit = new ApplicationModel();
            appToEdit.Id = 1;
            appToEdit.Title = "edited app";

            ApplicationController.NewEditApp(appToEdit);

            Assert.IsNotNull(ApplicationRepositoryMock.FindByName("edited app"));

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

        [TestMethod]
        public void DetailsValidIdUserApp()
        {
            //TODO: fazer mock do httpcontext
            var model = ApplicationController.DetailsApp(1) as ViewResult;
            var app = model.ViewData.Model;

            Assert.Equals(ApplicationRepositoryMock.FindById(1), app);

        }


    }
}
