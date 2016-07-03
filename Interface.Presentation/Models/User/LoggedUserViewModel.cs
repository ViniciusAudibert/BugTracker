using Interface.Presentation.Models.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain = BugTracker.Domain.Entity;

namespace Interface.Presentation.Models.User
{
    public class LoggedUserViewModel
    {
        public int IDUser { get; private set; }
        public String Name { get; private set; }
        public String Email { get; private set; }
        public String Image { get; private set; }
        public ICollection<ApplicationViewModel> Applications { get; private set; }
        public bool AccountConfirmed { get; private set; }

        public LoggedUserViewModel(int idUser,String name,String email,String image,ICollection<ApplicationViewModel> applications,bool accountConfirmed)
        {
            this.IDUser = idUser;
            this.Name = name;
            this.Email = email;
            this.Image = image;
            this.Applications = applications;
            this.AccountConfirmed = accountConfirmed;
        }
        
        //TODO: adicionar o ICollection de aplication no user
        // já implementado --  ApplicationViewModel.CollectionToViewModel(u.Applications)
        public LoggedUserViewModel(Domain.User u) : 
            this(u.IDUser, u.Name, u.Email, u.Image, null, u.AccountConfirmed) {  }
    }
}