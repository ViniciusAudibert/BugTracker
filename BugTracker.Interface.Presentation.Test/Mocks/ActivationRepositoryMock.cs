using BugTracker.Domain.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTracker.Domain.Entity;

namespace BugTracker.Interface.Presentation.Test.Mocks
{
    public class ActivationRepositoryMock : IActivationRepository
    {

        public List<UserActivation> ActavationList;
        public UserRepositoryMock UserRepositoryMock;

        public ActivationRepositoryMock()
        {
            UserRepositoryMock = new UserRepositoryMock();
            ActavationList = new List<UserActivation>();
            var user = UserRepositoryMock.FindById(1);
            var actvation1 = new UserActivation("hash test 1", 1, user);
            var actvation2 = new UserActivation("hash test 2", 2, user);

            ActavationList.Add(actvation1);
            ActavationList.Add(actvation2);

        }

        public void Add(UserActivation activation)
        {
            ActavationList.Add(activation);
        }

        public UserActivation FindByCode(string code)
        {
            var activation = ActavationList.FirstOrDefault(x => x.HashCode == code);
            if (activation != null && activation.RequestDate.CompareTo(DateTime.Now) < 1)
                return activation;
            return null;
        }

        public void Remove(int id)
        {
            var activationToDelte = ActavationList.FirstOrDefault(x => x.IDUserActivation == id);
            ActavationList.Remove(activationToDelte);
        }
    }
}
