using BugTracker.Domain.Entity;
using BugTracker.Domain.Service;
using BugTracker.Interface.Presentation.Test.Mocks;
using Interface.Presentation.Controllers;
using Interface.Presentation.Models.User;
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
    public class HomeControllerTest
    {
        private UserRepositoryMock UserRepositoryMock;
        private HomeController HomeController;

        [TestInitialize()]
        public void setController()
        {
            HttpContextMock.CreateMockContext();
            UserRepositoryMock = new UserRepositoryMock();
            this.HomeController = new HomeController(new UserService(UserRepositoryMock));
        }

        //TODO: mock para serviço de email
        /*
        [TestMethod]
        public void RegisterNewUser()
        {
            var userToRegister = new RegisterUserViewModel();
            userToRegister.Email = "test@test";
            var countUserList = UserRepositoryMock.Users.Count;
            Assert.AreEqual(countUserList, 2);

            HomeController.NewRegister(userToRegister);
            countUserList = UserRepositoryMock.Users.Count;
            Assert.AreEqual(countUserList, 3);
        }
        */

        

        [TestMethod]
        public void RegisterExistentUser()
        {
            var userToRegister = new RegisterUserViewModel();
            userToRegister.Email = UserRepositoryMock.FindById(1).Email;

            var viewResult = HomeController.NewRegister(userToRegister) as ViewResult;
            var errorModel = viewResult.ViewData.ModelState.FirstOrDefault( x => x.Key == "INVALID_USER");
            Assert.AreEqual(errorModel.Key, "INVALID_USER");
        }

        [TestMethod]
        public void RegisterEmailInUse()
        {
            var userToRegister = new RegisterUserViewModel();
            userToRegister.Email = UserRepositoryMock.FindById(1).Email;

            var viewResult = HomeController.NewRegister(userToRegister) as ViewResult;
            var errorModel = viewResult.ViewData.ModelState.FirstOrDefault(x => x.Key == "INVALID_USER");
            Assert.AreEqual(errorModel.Value.Errors.First().ErrorMessage, "Email is already in use");
        }

        [TestMethod]
        public void RegisterEmailInUseWithOutPassWord()
        {
            var userToRegister = new RegisterUserViewModel();
            
            var userOnlyEmail = new User(3, "user test", "email@github", null, "default", "hash test", new List<Application>(), true, true);
            UserRepositoryMock.Add(userOnlyEmail);

            userToRegister.Email = UserRepositoryMock.FindById(3).Email;

            var viewResult = HomeController.NewRegister(userToRegister) as ViewResult;
            var errorModel = viewResult.ViewData.ModelState.FirstOrDefault(x => x.Key == "INVALID_USER");
            Assert.AreEqual(errorModel.Value.Errors.First().ErrorMessage, "Email is used with github, you can set a password in your perfil settings");
        }
        
        //TODO: mock para serviço de email
        /*
        [TestMethod]
        public void RegisterWithOutImage()
        {
            var userToRegister = new RegisterUserViewModel();
            var emailToFind = "email@test";
            userToRegister.Email = emailToFind;
            userToRegister.FileImage = null;

            HomeController.NewRegister(userToRegister);
            Assert.AreEqual(UserRepositoryMock.FindByEmail(emailToFind).Image, "default-perfil.jpg");
        }
        */
    }
}
