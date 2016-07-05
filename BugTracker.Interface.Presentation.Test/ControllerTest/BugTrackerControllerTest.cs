using BugTracker.Domain.Entity;
using BugTracker.Domain.Service;
using BugTracker.Interface.Presentation.Test.Mocks;
using Interface.Presentation.Controllers;
using Interface.Presentation.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BugTracker.Interface.Presentation.Test.UnitTests
{

    [TestClass]
    public class BugTrackerControllerTest
    {

        private ApplicationService ApplicationService;
        private BugTrackerService BugTrackerService;
        private ApplicationRepositoryMock ApplicationRepositoryMock;
        private BugTrackerRepositoryMock BugTrackerRepositoryMock;
        private BugTrackerController BugTrackerController;

        [TestInitialize()]
        public void setController()
        {
            this.ApplicationRepositoryMock = new ApplicationRepositoryMock();
            this.BugTrackerRepositoryMock = new BugTrackerRepositoryMock();
            this.ApplicationService = new ApplicationService(ApplicationRepositoryMock);
            this.BugTrackerService = new BugTrackerService(BugTrackerRepositoryMock);

            this.BugTrackerController = new BugTrackerController(BugTrackerService, ApplicationService);

        }

        [TestMethod]
        public void GetBugTrackerFilterToFindOneBugError()
        {
            var filter = new BugTrackerFilter();

            filter.Status = new List<BugTrackerStatus>();
            filter.Status.Add(BugTrackerStatus.ERROR);
            filter.Trace = "error test";

            var viewResult = BugTrackerController.GetBugTrackerPaginedByApp(filter) as PartialViewResult;
            var modelBugTrackersObtained = viewResult.ViewData.Model;
            var bugs = BugTrackerRepositoryMock.FindByApplicationPagined(filter);
            var modelBugTrackerExpected = bugs.FromModel();

            Assert.AreEqual(modelBugTrackersObtained.ToString(), modelBugTrackerExpected.ToString());

        }

        [TestMethod]
        public void GetBugTrackerFilterToFindOneBugWarnning()
        {
            var filter = new BugTrackerFilter();

            filter.Status = new List<BugTrackerStatus>();
            filter.Status.Add(BugTrackerStatus.WARNING);
            filter.Trace = "warnning test";

            var viewResult = BugTrackerController.GetBugTrackerPaginedByApp(filter) as PartialViewResult;
            var modelBugTrackersObtained = viewResult.ViewData.Model;
            var bugs = BugTrackerRepositoryMock.FindByApplicationPagined(filter);
            var modelBugTrackerExpected = bugs.FromModel();

            Assert.AreEqual(modelBugTrackersObtained.ToString(), modelBugTrackerExpected.ToString());

        }

        [TestMethod]
        public void GetBugTrackerFilterToFindAllBugsOfAppIdZero()
        {
            var filter = new BugTrackerFilter();
            filter.idApplication = 0;

            var viewResult = BugTrackerController.GetBugTrackerPaginedByApp(filter) as PartialViewResult;
            var modelBugTrackersObtained = viewResult.ViewData.Model;
            var bugs = BugTrackerRepositoryMock.FindByApplicationPagined(filter);
            var modelBugTrackerExpected = bugs.FromModel();

            Assert.AreEqual(modelBugTrackersObtained.ToString(), modelBugTrackerExpected.ToString());

        }

        [TestMethod]
        public void GetBugTrackerFilterVerifyViewName()
        {
            var filter = new BugTrackerFilter();
            var viewResult = BugTrackerController.GetBugTrackerPaginedByApp(filter) as PartialViewResult;

            Assert.AreEqual(viewResult.ViewName, "_bug-trackers-application");
        }

        [TestMethod]
        public void GetCountBugTrackerByValidApp()
        {
            var filter = new BugTrackerFilter();
            filter.idApplication = 0;

            var viewResult = BugTrackerController.GetCountBugTrackerByApp(filter);
            var dataObtained = viewResult.Data;
            var dataTracker = BugTrackerRepositoryMock.GetCountBugsByApp(filter);

            var dataExpected = new
            {
                data = dataTracker,
                status = Enum.GetNames(typeof(Domain.Entity.BugTrackerStatus))
            };

            Assert.AreEqual(dataExpected.ToString(), dataObtained.ToString());
        }

        [TestMethod]
        public void GetCountBugTrackerByInvalidApp()
        {
            var filter = new BugTrackerFilter();
            filter.idApplication = 999;

            var viewResult = BugTrackerController.GetCountBugTrackerByApp(filter);
            var dataObtained = viewResult.Data;
            var dataTracker = BugTrackerRepositoryMock.GetCountBugsByApp(filter);

            var dataExpected = new
            {
                data = dataTracker,
                status = Enum.GetNames(typeof(Domain.Entity.BugTrackerStatus))
            };

            Assert.AreEqual(dataExpected.ToString(), dataObtained.ToString());
        }
    }
}
