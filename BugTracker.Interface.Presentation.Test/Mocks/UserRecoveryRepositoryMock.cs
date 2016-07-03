using BugTracker.Domain.Interface.Repository;
using System;
using System.Collections.Generic;
using BugTracker.Domain.Entity;
using Interface.Presentation.Models.UserRecovery;
using System.Linq;

namespace BugTracker.Interface.Presentation.Test.Mocks
{
    public class UserRecoveryRepositoryMock : IUserRecoveryRepository
    {

        public List<ForgotPassword> RecoverList;

        public UserRecoveryRepositoryMock()
        {

            RecoverList = new List<ForgotPassword>();

            var userRecover1 = new User();
            var userRecover2 = new User();
            var forgotTest1 = new ForgotPassword(userRecover1, DateTime.Today, "hash test 1");
            var forgotTest2 = new ForgotPassword(userRecover2, DateTime.Today, "hash test 2");

            RecoverList.Add(forgotTest1);
            RecoverList.Add(forgotTest2);

        }


        public void Add(ForgotPassword userRecovery)
        {
            RecoverList.Add(userRecovery);
        }

        public ForgotPassword FindByCode(string code)
        {
            var userRecovery = RecoverList.FirstOrDefault( x => x.HashCode == code);
            if (userRecovery != null && userRecovery.RequestDate.CompareTo(DateTime.Now) < 1)
                return userRecovery;
            return null;
        }
    }
}
