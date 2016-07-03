using BugTracker.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Interface.Repository
{
    public interface IActivationRepository
    {
        UserActivation FindByCode(string code);
        void Add(UserActivation activation);
        void Remove(int id);
    }
}
