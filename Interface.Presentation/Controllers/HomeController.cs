using BugTracker.Domain.Entity;
using BugTracker.Domain.Interface.Service;
using Interface.Presentation.Mail_Body;
using Interface.Presentation.Models.User;
using Interface.Presentation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Interface.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private IUserService userService;

        public HomeController()
        {
            userService = UserServiceInjection.Create();
        }

        public HomeController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SignIn()
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Documentation()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewRegister(RegisterUserViewModel userModel)
        {
            if (ModelState.IsValid)
            {
                BugTracker.Domain.Entity.User userFound = userService.FindByEmail(userModel.Email);

                if (userFound != null)
                {
                    if (userFound.Password != null)
                    {
                        ModelState.AddModelError("INVALID_USER", "Email is already in use");
                        return View("Register");
                    }
                    else
                    {
                        ModelState.AddModelError("INVALID_USER", "Email is used with github, you can set a password in your perfil settings");
                        return View("Register");
                    }
                }
                else
                {
                    string fileName = "default-perfil.jpg";

                    if (userModel.FileImage != null)
                    {
                        fileName = UploadImageService.UploadUserImage(userModel.FileImage);
                    }

                    BugTracker.Domain.Entity.User user = new User(
                        userModel.Name,
                        userModel.Email,
                        userModel.Password,
                        fileName,
                        Guid.NewGuid().ToString() + new Random().Next(1000),
                        null,
                        true,
                        false);

                    user = userService.Add(user);

                    UserActivationMail.SendTo(user);
                }
            }
            else
            {
            return View("register");
            }
            return RedirectToAction("Index","Login");
        }
        
    }
}