
using BugTracker.Domain.Entity;
using BugTracker.Domain.Service;
using BugTracker.Interface.Presentation.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web;

namespace BugTracker.Domain.Test.ServiceTests
{
    [TestClass]
    public class ApplicationServiceTest
    {

        private ApplicationRepositoryMock ApplicationRepositoryMock;
        private ApplicationService ApplicationService;

        [TestInitialize()]
        public void SetRepository()
        {
            ApplicationRepositoryMock = new ApplicationRepositoryMock();
            ApplicationService = new ApplicationService(ApplicationRepositoryMock);
        }

        [TestMethod]
        public void FindByIdUser()
        {
            var appListExpected = ApplicationRepositoryMock.AppsList;
            var appListObtained = ApplicationService.FindByIDUser(1);

            Assert.AreEqual(appListExpected.ToString(), appListObtained.ToString());
        }

        [TestMethod]
        public void FindByIdUserIvalid()
        {
            var appListObtained = ApplicationService.FindByIDUser(999);

            Assert.IsTrue(appListObtained.Count == 0);
        }

        [TestMethod]
        public void FindByUrlEmpty()
        {
            var appObtained = ApplicationService.FindByUrl("");

            Assert.IsNull(appObtained);
        }

        [TestMethod]
        public void FindByInexistentUrl()
        {
            var appObtained = ApplicationService.FindByUrl("inexistent");

            Assert.IsNull(appObtained);
        }

        [TestMethod]
        public void FindByValidUrl()
        {
            var appObtained = ApplicationService.FindByUrl("www.test1");
            var appExpected = ApplicationRepositoryMock.FindByUrl("www.test1");

            Assert.AreEqual(appExpected, appObtained);
            
        }

        [TestMethod]
        public void FindByInexistentHashCodeAndValidUrl()
        {
            var inextitentHasCode = "invalid";
            var validUrl = "www.test1";
            var appObtained = ApplicationService.FindByUrlAndUserHashCode(validUrl, inextitentHasCode);

            Assert.IsNull(appObtained);

        }

        [TestMethod]
        public void FindByValidHashCodeAndInvalidUrl()
        {
            var inextitentHasCode = "test hash";
            var validUrl = "invalid url";
            var appObtained = ApplicationService.FindByUrlAndUserHashCode(validUrl, inextitentHasCode);

            Assert.IsNull(appObtained);
        }

        [TestMethod]
        public void FindByValidHashCodeAndValidUrl()
        {
            var validHash = "test hash";
            var validUrl = "www.test1";
            var appObtained = ApplicationService.FindByUrlAndUserHashCode(validUrl, validHash);
            var appExpected = ApplicationRepositoryMock.FindByUrlAndUserHashCode(validUrl, validHash);

            Assert.AreEqual(appObtained, appExpected);
        }

        [TestMethod]
        public void EditExistentApplication()
        {
            var userTest = new User("user test", "test@amil", "password", "default", "test hash", null, true, true);
            var appToEdit = new Application(1, "Changed Title", "app para teste", "www.test1", true, "default", "tag", 1, userTest);

            ApplicationService.Edit(appToEdit);
            var editedApp = ApplicationRepositoryMock.FindById(1);

            Assert.AreEqual(appToEdit.Title, editedApp.Title);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EditInexistentApplication()
        {
            var userTest = new User("user test", "test@amil", "password", "default", "test hash", null, true, true);
            var appToEdit = new Application(999, "Changed Title", "app para teste", "www.test1", true, "default", "tag", 1, userTest);

            ApplicationService.Edit(appToEdit);
            
        }

        [TestMethod]
        public void FindByValidId()
        {
            var appObtained = ApplicationService.FindById(1);
            var appExpected = ApplicationRepositoryMock.FindById(1);

            Assert.AreEqual(appExpected, appObtained);

        }

        [TestMethod]
        public void FindByValidName()
        {
            var appToSearch = new Application(3, "Test3", "app to teste", "www.test3", true, "default", "tag", 1, null);
            ApplicationRepositoryMock.Add(appToSearch);
            var appListObtained = ApplicationService.FindByName("Test3");
            
            Assert.IsTrue(appListObtained.Contains(appToSearch));
        }

        [TestMethod]
        public void FindByInexistentName()
        {;
            var appListObtained = ApplicationService.FindByName("Inexistent");

            Assert.IsTrue(appListObtained.Count == 0);
        }
    }
}
