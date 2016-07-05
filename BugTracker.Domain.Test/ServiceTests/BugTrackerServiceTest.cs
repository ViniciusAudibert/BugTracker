using BugTracker.Domain.Entity;
using BugTracker.Domain.Exceptions;
using BugTracker.Domain.Interface.Service;
using BugTracker.Domain.Service;
using BugTracker.Interface.Presentation.Test.Mocks;
using Interface.Presentation.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BugTracker.Domain.Test.ServiceTests
{
    [TestClass]
    public class BugTrackerServiceTest
    {
        private BugTrackerRepositoryMock BugTrackerRepositoryMock;
        private BugTrackerService BugTrackerService;
        private IMailService EmailService;

        [TestInitialize()]
        public void SetRepository()
        {
            BugTrackerRepositoryMock = new BugTrackerRepositoryMock();
            EmailService = MailServiceInjection.Create();
            BugTrackerService = new BugTrackerService(BugTrackerRepositoryMock);
        }

        [TestMethod]
        public void AddBugTrackerWithValidTag()
        {
            var tagTest1 = new BugTrackerTag(1, "error tag", new Entity.BugTracker());
            var tagTest2 = new BugTrackerTag(2, "warnning tag", new Entity.BugTracker());
            var tagsList = new List<BugTrackerTag>();
            tagsList.Add(tagTest1);
            tagsList.Add(tagTest2);
            
            var countBugTracker = BugTrackerRepositoryMock.BugsList.Count;
            Assert.AreEqual(countBugTracker, 2);

            var bugTrackerToAdd = new Domain.Entity.BugTracker(new Application(), BugTrackerStatus.ERROR, "error test", DateTime.Today, tagsList, null, null);

            BugTrackerService.Add(bugTrackerToAdd);
            countBugTracker = BugTrackerRepositoryMock.BugsList.Count;
            Assert.AreEqual(countBugTracker, 3);

        }

        [TestMethod]
        [ExpectedException(typeof(TagVeryLargeException))]
        public void AddBugTrackerWithInvalidTag()
        {
            var tagTest1 = new BugTrackerTag("tag with many characters will trhrow a exception");
            var tagsList = new List<BugTrackerTag>();
            tagsList.Add(tagTest1);

            var bugTrackerToAdd = new Domain.Entity.BugTracker(null, BugTrackerStatus.ERROR, "error test", DateTime.Today, tagsList, null, null);

            BugTrackerService.Add(bugTrackerToAdd);
            
        }

        [TestMethod]
        public void FindBugByValidIdApp()
        {
            var appTest = new Application(1, "app test", "description test", "url test", true, "default", "tag master", 1, null);
            var bugToFind = new Domain.Entity.BugTracker(1, appTest, BugTrackerStatus.ERROR, "error test", DateTime.Today, new List<BugTrackerTag>(), null, null);
            bugToFind.IDApplication = appTest.IDApplication;

            BugTrackerService.Add(bugToFind);
            var bugsObtained = BugTrackerService.FindByIDApplication(1);

            Assert.AreEqual(bugToFind, bugsObtained.FirstOrDefault());

        }

        [TestMethod]
        public void FindBugByInvalidIdApp()
        {
            var invalidAppIndex = 999;
            var bugsObtained = BugTrackerService.FindByIDApplication(invalidAppIndex);

            Assert.IsTrue(bugsObtained.Count == 0);

        }

    }
}
