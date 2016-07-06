using BugTracker.Domain.Entity;
using BugTracker.Domain.Interface.Service;
using BugTracker.Domain.Service;
using Interface.Presentation.Filters;
using Interface.Presentation.Models.Application;
using Interface.Presentation.Models.User;
using Interface.Presentation.Services;
using Interface.Presentation.Extensions;
using System;
using System.Web;
using System.Web.Mvc;

namespace Interface.Presentation.Controllers
{
    public class UserController : Controller
    {
        private IApplicationService applicationService;
        private IDownloadService downloadService;
        private IUserService userService;
        

        public UserController()
        {
            applicationService = ApplicationServiceInjection.Create();
            userService = UserServiceInjection.Create();
            downloadService = DownloadServiceInjection.Create();
        }

        public UserController(IApplicationService applicationService, IUserService userService)
        {
            this.applicationService = applicationService;
            this.userService = userService;
            
        }


        [UserToken]
        [HttpGet]
        public ActionResult Index()
        {
            var apps = 
                applicationService.FindAppAndBugsByAppId(UserSessionService.LoggedUser.IDUser)
                .ToApplicationAndBugsViewModel();

            return View(apps);
        }

        [UserToken]
        [HttpPost]
        public ActionResult Search(String name)
        {
            var model = applicationService.FindAppAndBugsByName(name, UserSessionService.LoggedUser.IDUser)
                .ToApplicationAndBugsViewModel(); ;

            ViewBag.FilterName = name;

            return View("Index", model);
        }

        [UserToken]
        [HttpGet]
        public ActionResult Account()
        {
            var user = userService.FindById(UserSessionService.LoggedUser.IDUser);
            var viewModel = new UserEditAccountViewModel();
            viewModel.Name = user.Name;
            viewModel.Email = user.Email;
            viewModel.Id = user.IDUser;
            viewModel.Image = user.Image;

            return View(viewModel);
        }

        [UserToken]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccount(UserEditAccountViewModel model)
        {

            EditAcccountService.EditUser(model, userService);
            UserSessionService.RefreshSession(model);

            return View("account", model);
            
        }

        [UserToken]
        [HttpPost]
        public ActionResult ChangePerfilImage(HttpPostedFileBase file)
        {
            if(file != null)
            {
                var imageName = UploadImageService.UploadUserImage(file);

                userService.UpdateImage(UserSessionService.LoggedUser.IDUser, imageName);

                UserSessionService.RefreshUserImageSession(imageName);
            }
            return RedirectToAction("Account");
        }

        [UserToken]
        [HttpGet]
        public ActionResult Download()
        {
            return View();
        }

        [UserToken]
        [HttpGet]
        public FileResult DownloadLibrary(string type)
        {
            User user = userService.FindById(UserSessionService.LoggedUser.IDUser);

            downloadService.SetPath(Server.MapPath("~/Library/"));
            downloadService.CreateFileForUser(user);

            return File(
                downloadService.GetFileByte(user),
                System.Net.Mime.MediaTypeNames.Application.Octet,
                downloadService.GetFileName()
            );
        }
    }
}