using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Entity
{
    public class Application
    {
        public int IDApplication { get; private set; }
        public String Title { get; private set; }
        public String Description { get; private set; }
        public String Url { get; private set; }
        public bool Active { get; private set; }
        public String Image { get; private set; }
        public String SpecialTag { get; private set; }
        public int IDUser { get; private set; }
        public virtual User User { get; private set; }
        public virtual List<BugTracker> BugTrackers { get; private set; }

        public Application() { }

        public Application(String title, String description, String url, bool active, String image, String tag, int IdUser, User user)
        {
            this.Title = title;
            this.Description = description;
            this.Url = url;
            this.Active = active;
            this.Image = image;
            this.SpecialTag = tag;
            this.IDUser = IdUser;
            this.User = user;
        }

        public Application(int id, String title, String description, String url, bool active, String image, String tag, int IdUser, User user)
            : this(title, description, url, active, image, tag, IdUser, user)
        {
            this.IDApplication = id;
        }
    }
}
