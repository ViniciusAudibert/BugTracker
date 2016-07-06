using BugTracker.Domain.Entity;
using BugTracker.Domain.Interface.Service;
using BugTracker.Domain.Service;
using Interface.Presentation.Filters;
using Interface.Presentation.Models;
using Interface.Presentation.Models.Application;
using Interface.Presentation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Interface.Presentation.Controllers
{
    public class ApplicationController : Controller
    {
        private IApplicationService applicationService;
        private IUserService userService;
        

        public ApplicationController()
        {
            applicationService = ApplicationServiceInjection.Create();
            userService = UserServiceInjection.Create();
        }

        public ApplicationController(ApplicationService appService, UserService userService)
        {
            this.applicationService = appService;
            this.userService = userService;
        }

        [UserToken]
        [HttpGet]
        public ActionResult RegisterApp(int? id)
        {
            if (id.HasValue)
            {
                var app = applicationService.FindById((int)id);
                var model = new ApplicationModel();
                model.Id = app.IDApplication;
                model.Title = app.Title;
                model.Url = app.Url;
                model.Description = app.Description;
                model.Icon = app.Image;

                model.Tag = app.SpecialTag;

                return View("register-app", model);
            }

            return View("register-app");
        }

        [HttpPost]
        [UserToken]
        [ValidateAntiForgeryToken]
        public ActionResult NewEditApp(ApplicationModel model)
        {
            if (ModelState.IsValid)
            {
                var idUser = UserSessionService.LoggedUser.IDUser;
                var user = userService.FindById(idUser);
                var application = applicationService.FindByIDUser(idUser);

                String fileName = model.File != null ? model.File.FileName : "application-default.png";


                if (model.Id.HasValue)
                {
                    if (model.File == null)
                    {
                        fileName = model.Icon;
                    }
                    else
                    {
                        fileName = UploadImageService.UploadApplicationImage(model.File);
                    }

                    var app = new Application(model.Id.Value, model.Title, model.Description,
                                            model.Url, true, fileName,
                                            model.Tag, idUser, user);
                    applicationService.Edit(app);
                    return RedirectToAction("index", "user");
                }
                else
                {
                    if (application.Count() < 5)
                    {
                        if (application.FirstOrDefault(a => a.Url.Equals(model.Url)) == null)
                        {
                            if (model.File != null)
                            {
                                fileName = UploadImageService.UploadApplicationImage(model.File);
                            }
                            var app = new Application(model.Title, model.Description,
                                                    model.Url, true, fileName,
                                                    model.Tag, idUser, user);
                            applicationService.Add(app);
                            return RedirectToAction("index", "user");
                        }
                        else
                        {
                            ModelState.AddModelError("INVALID_URL", "This  url already exist");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("INVALID_APPLICATION_NUMBER", "The limit of applications you can have is 5");
                    }

                }

            }

            return View("register-app", model);
        }

        [UserToken]
        [HttpGet]
        public ActionResult DeleteApp(int id)
        {
            var app = applicationService.FindById(id);
            if (app != null)
            {
                var user = userService.FindById(app.IDUser);

                var appToDelete = new Application(app.IDApplication, app.Title, app.Description, app.Url, false, app.Image, app.SpecialTag, app.IDUser, user);
                applicationService.Edit(appToDelete);
            }

            return RedirectToAction("Index", "User");
        }

        [UserToken]
        [HttpGet]
        public ActionResult DetailsApp(int id)
        {
            var app = applicationService.FindById(id);

            if (app.IDUser != UserSessionService.LoggedUser.IDUser)
            {
                return RedirectToAction("Index", "User");
            }

            ViewBag.Status = Enum.GetNames(typeof(BugTracker.Domain.Entity.BugTrackerStatus));

            return View("details", app);
        }
    }
}