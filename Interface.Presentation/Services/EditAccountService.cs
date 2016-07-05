using BugTracker.Domain.Entity;
using BugTracker.Domain.Interface.Service;
using Interface.Presentation.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Interface.Presentation.Services
{
    public abstract class EditAcccountService
    {

        public static void EditUser(UserEditAccountViewModel model, IUserService userService)
        {
            var idUser = UserSessionService.LoggedUser.IDUser;

            var userFounded = userService.FindById(idUser);

            String fileName = model.Image;

            String oldPassword = userFounded.Password;

            if (model.File != null)
            {
                fileName = model.File.FileName;
            }

            if (model.OldPassword != null && model.NewPassword != null)
            {
                
                if (userService.ComparePassword(oldPassword, model.NewPassword))
                {
                    var editedAccount = new User(idUser, model.Name, model.Email, model.NewPassword,
                                         fileName, model.HashCode, null, true, true);

                    userService.Update(editedAccount);
                }

            }
            else
            {
                var editedAccount = new User(idUser, model.Name,
                                         model.Email, null,
                                         fileName, Guid.NewGuid().ToString() + new Random().Next(100), null, true, true);

                userService.Update(editedAccount);
            }
            
            UploadImageService.UploadUserImage(model.File);

        }

    }
}