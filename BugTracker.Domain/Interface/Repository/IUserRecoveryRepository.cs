using BugTracker.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Interface.Repository
{
    public interface IUserRecoveryRepository
    {
        void Add(ForgotPassword userRecovery);
        ForgotPassword FindByCode(string code);
    }
}
