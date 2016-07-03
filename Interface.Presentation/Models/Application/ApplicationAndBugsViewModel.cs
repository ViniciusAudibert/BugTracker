using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain = BugTracker.Domain.Entity;

namespace Interface.Presentation.Models.Application
{
    public class ApplicationAndBugsViewModel
    {
        public String AppName { get; set; }
        public int AppId { get; set; }
        public String AppImage { get; set; }
        public Domain.BugTracker Bug{ get; set; }
        public int TracksCountError { get; set; }
        public int TracksCountWarning { get; set; }
       

        public ApplicationAndBugsViewModel(int appId, String appName, String appImg, Domain.BugTracker bug, int countError, int countWarning)
        {
            this.AppName = appName;
            this.AppId = appId;
            this.AppImage = appImg;
            this.Bug = bug;
            this.TracksCountError = countError;
            this.TracksCountWarning = countWarning;
        }
    }
}