using BugTracker.Domain.Entity;
using BugTracker.Domain.Service;
using BugTracker.Interface.Presentation.Test.Mocks;
using Interface.Presentation.Controllers;
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
        public void GetBugTrackerByValidApp()
        {
            var filter = new BugTrackerFilter();
            filter.idApplication = 1;
            
            var viewResult = BugTrackerController.GetBugTrackerPaginedByApp(filter) as JsonResult;
            
            Assert.IsNull(viewResult);

        }

    }
}
