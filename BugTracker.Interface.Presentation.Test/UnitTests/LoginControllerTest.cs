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
    public class LoginControllerTest
    {

        private UserRepositoryMock UserRepositoryMock;
        private LoginController LoginController;
        private UserService UserService;

        [TestInitialize()]
        public void setController()
        {
            HttpContextMock.CreateMockContext();
            this.UserRepositoryMock = new UserRepositoryMock();
            this.UserService = new UserService(UserRepositoryMock);
            this.LoginController = new LoginController(UserService);
        }
        
        [TestMethod]
        public void LoginWithAcountNotConfirmed()
        {
            var userUnconfirmed = new User(3, "User Test 3", "teste@3", "test password", "default", "hash", null, false, false);
            UserService.Add(userUnconfirmed);

            var signInModel = new UserLoginViewModel();
            signInModel.Email = "teste@3";
            signInModel.Password = "test password";

            var viewResult = LoginController.SignIn(signInModel) as ViewResult;
            var errorModel = viewResult.ViewData.ModelState.FirstOrDefault(x => x.Key == "INVALID_USER");
            Assert.AreEqual(errorModel.Value.Errors.First().ErrorMessage, "Please active your account, an e-mail has been sent to your inbox");
        }
        

        [TestMethod]
        public void LoginWithAcountInexistent()
        {
            var signInModel = new UserLoginViewModel();
            signInModel.Email = "invalidEmail";
            signInModel.Password = "invalidPassword";

            var viewResult = LoginController.SignIn(signInModel) as ViewResult;
            var errorModel = viewResult.ViewData.ModelState.FirstOrDefault(x => x.Key == "INVALID_USER");
            Assert.AreEqual(errorModel.Value.Errors.First().ErrorMessage, "Email or password is invalid");
        }

        [TestMethod]
        public void LoginWithAcountExistentAndConfirmed()
        {
            var validUser = new User(1, "User Test 1", "teste@1", "test", "default", "hash", null, true, true);
            UserService.Add(validUser);

            var signInValidModel = new UserLoginViewModel();
            signInValidModel.Email = validUser.Email;
            signInValidModel.Password = "test";
            
            var viewResult = LoginController.SignIn(signInValidModel) as ViewResult;
            Assert.IsNull(viewResult);
        }
    }
}
