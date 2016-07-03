using System;
using System.Collections.Generic;

namespace BugTracker.Domain.Entity
{
    public class User
    {
        public int IDUser { get; private set; }
        public String Name { get; private set; }
        public String Email { get; private set; }
        public String Password { get; set; }
        public String Image { get; private set; }
        public String HashCode { get; private set; }
        public virtual ICollection<Application> Applications { get; private set; }
        public bool Active { get; set; }
        public bool AccountConfirmed { get; set; }
        public virtual ICollection<UserActivation> Activations { get; private set; }
        public virtual ICollection <ForgotPassword> Forgots { get; private set; }
        

        public User() { }

        public User(String name, String email, String Password, String Image, string hashCode, List<Application> applications, bool Active, bool AccountConfirmed)
        {
            this.Name = name;
            this.Email = email;
            this.Password = Password;
            this.Image = Image;
            this.Applications = applications;
            this.Active = Active;
            this.AccountConfirmed = AccountConfirmed;
            this.HashCode = hashCode;
        }

        public User(int id, String name, String email, String password, String Image, string hashCode, List<Application> applications, bool active, bool accountConfirmed) 
            : this(name, email, password, Image, hashCode, applications,  active, accountConfirmed)
        {
            this.IDUser = id;
        }
    }
}
