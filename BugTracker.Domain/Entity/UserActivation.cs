using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Entity
{
    public class UserActivation
    {
        public int IDUserActivation { get; private set; }
        public string HashCode { get; private set; }
        public DateTime RequestDate { get; private set; }
        public int IDUser { get; private set; }
        public User User { get; set; }

        public UserActivation() { }

        public UserActivation(string hashCode, int idUser, User user)
        {
            this.HashCode = hashCode;
            this.IDUser = idUser;
            this.User = user;
            this.RequestDate = DateTime.Today;
        }

        public UserActivation(string hashCode, int idUser, User user, DateTime date) : this (hashCode, idUser, user)
        {
            this.RequestDate = date;
        }

    }
}
