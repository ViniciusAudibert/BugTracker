using Interface.Presentation.Models.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using Domain = BugTracker.Domain.Entity;

namespace Interface.Presentation.Models.Application
{
    public class ApplicationViewModel
    {
        public int IDApplication { get; private set; }
        public String Title { get; private set; }
        public String Description { get; private set; }
        public String Url { get; private set; }
        public String Image { get; private set; }
        public String SpecialTag { get; private set; }
        public LoggedUserViewModel User { get; private set; }

        public ApplicationViewModel(int idApplication,String title,String description,String url,String image,String specialTag,LoggedUserViewModel user)
        {
            this.IDApplication = idApplication;
            this.Title = title;
            this.Description = description;
            this.Url = url;
            this.Image = image;
            this.SpecialTag = specialTag;
            this.User = user;
        }

        public ApplicationViewModel(Domain.Application app) : 
            this(app.IDApplication,app.Title,app.Description,app.Url,app.Image,app.SpecialTag,new LoggedUserViewModel(app.User)) {  }

        public static ICollection<ApplicationViewModel> CollectionToViewModel(ICollection<Domain.Application> list)
        {
            ICollection<ApplicationViewModel> appModel = new Collection<ApplicationViewModel>();

            //TODO: Fazer com linq
            foreach (Domain.Application app in list)
            {
                appModel.Add(new ApplicationViewModel(app));
            }

            return appModel;
        }
    }
}