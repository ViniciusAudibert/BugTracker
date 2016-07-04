using BugTracker.Domain.Entity;
using BugTracker.Domain.Interface.Service;
using Interface.Presentation.Filters;
using Interface.Presentation.Models.User;
using Interface.Presentation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Interface.Presentation.Controllers
{
    public class LoginController : Controller
    {
        IUserService userService;

        public LoginController(IUserService userService)
        {
            this.userService = userService;
        }

        public LoginController()
        {
            userService = UserServiceInjection.Create();
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (UserSessionService.IsLogged)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpGet]
        [UserToken]
        public ActionResult Loggout()
        {
            UserSessionService.Loggout();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(UserLoginViewModel userLoginViewModel)
        {
            if (ModelState.IsValid)
            {
                User userFounded =
                    userService.FindByAuthentication(
                            userLoginViewModel.Email, userLoginViewModel.Password
                        );

                if (userFounded != null)
                {
                    if (!userFounded.AccountConfirmed)
                    {
                        ModelState.AddModelError("INVALID_USER", "Please active your account, an e-mail has been sent to your inbox");

                        return View("Index", userLoginViewModel);
                    }
                    var userLoggedModel = new LoggedUserViewModel(userFounded);

                    UserSessionService.CreateSession(userLoggedModel);
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("INVALID_USER", "Email or password is invalid");
                }
            }

            return View("Index", userLoginViewModel);
        }
    }
}