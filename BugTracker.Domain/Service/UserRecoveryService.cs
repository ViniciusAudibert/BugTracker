using BugTracker.Domain.Entity;
using BugTracker.Domain.Interface.Repository;
using BugTracker.Domain.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Service
{
    public class UserRecoveryService : IUserRecoveryService
    {
        IUserRecoveryRepository userRecoveryRepository;

        public UserRecoveryService(IUserRecoveryRepository applicationRepository)
        {
            this.userRecoveryRepository = applicationRepository;
        }

        public void Add(ForgotPassword userRecovery)
        {
            userRecoveryRepository.Add(userRecovery);
        }

        public ForgotPassword FindByCode(string code)
        {
            var userRecovery = userRecoveryRepository.FindByCode(code);
            if (userRecovery != null && userRecovery.RequestDate.CompareTo(DateTime.Now) < 1)
                return userRecovery;
            return null;
        }
    }
}
