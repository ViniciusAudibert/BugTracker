using BugTracker.Domain.Entity;
using BugTracker.Domain.Interface.Service;
using Interface.Presentation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Interface.Presentation.Controllers
{
    public class ActivationController : Controller
    {
        private IActivationService activationService;

        private IUserService userService;

        public ActivationController()
        {
            this.activationService = ActivationServiceInjection.Create();
            this.userService = UserServiceInjection.Create();
        }

        public ActivationController(IActivationService activationService, IUserService userService)
        {
            this.activationService = activationService;
            this.userService = userService;
        }

        [HttpGet]
        public ActionResult Code(string code)
        {
            UserActivation activation = activationService.FindByCode(code);

            if (activation != null)
            {
                User user = activation.User;

                userService.ActiveAccount(user);

                TempData["Message"] = "Your account was successively activated";
            }
            else
            {
                TempData["Message"] = "This link is no longer avalible";
            }

            return RedirectToAction("Index", "Login");
        }
    }
}