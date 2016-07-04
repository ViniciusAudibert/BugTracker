using BugTracker.Domain.Entity;
using BugTracker.Domain.Service;
using BugTracker.Interface.Presentation.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace BugTracker.Domain.Test.ServiceTests
{
    [TestClass]
    public class UserActvationServiceTest
    {
        private ActivationRepositoryMock ActvationRepositoryMock;
        private ActivationService ActivationService;

        [TestInitialize()]
        public void SetRepository()
        {
            ActvationRepositoryMock = new ActivationRepositoryMock();
            ActivationService = new ActivationService(ActvationRepositoryMock);
        }

        [TestMethod]
        public void AddAActivation()
        {
            var sizeListActivations = ActvationRepositoryMock.ActavationList.Count();
            Assert.AreEqual(sizeListActivations, 2);

            ActivationService.Add(new UserActivation());
            sizeListActivations = ActvationRepositoryMock.ActavationList.Count();
            Assert.AreEqual(sizeListActivations, 3);

        }

        [TestMethod]
        public void FindWithValidCodeInDate()
        {
            var actvationExpected = new UserActivation("hash test 1", 1, null, DateTime.Today);
            ActivationService.Add(actvationExpected);

            var actvationObtained = ActivationService.FindByCode("hash test 1");

            Assert.AreEqual(actvationExpected.ToString(), actvationObtained.ToString());

        }

        [TestMethod]
        public void FindWithValidCodeOutOfDate()
        {
            var outDate = DateTime.Today.AddDays(100);
            var actvationExpected = new UserActivation("hash test 3", 1, null, outDate);
            ActivationService.Add(actvationExpected);

            var actvationObtained = ActivationService.FindByCode("hash test 3");

            Assert.IsNull(actvationObtained);

        }

        [TestMethod]
        public void FindWithInvalidCodeInDate()
       { 
            var actvationObtained = ActivationService.FindByCode("invalid hash");

            Assert.IsNull(actvationObtained);

        }

    }
}
