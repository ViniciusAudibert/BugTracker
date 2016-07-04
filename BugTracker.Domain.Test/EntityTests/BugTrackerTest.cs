using BugTracker.Domain.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Test.EntityTests
{
    [TestClass]
    public class BugTrackerTest
    {
        [TestMethod]
        public void ValidateTagWithTwoValidTags()
        {
            var validTag1 = new BugTrackerTag("tag error");
            var validTag2 = new BugTrackerTag("warnning");
            var listTags = new List<BugTrackerTag>();
            listTags.Add(validTag1);
            listTags.Add(validTag2);

            var bugTrackerTest = new Entity.BugTracker(null, Entity.BugTrackerStatus.ERROR, "test description", DateTime.Today, listTags, null, null);

            Assert.IsTrue(bugTrackerTest.ValidateTags());
        }

        [TestMethod]
        public void ValidateTagWithTwoInvalidTags()
        {
            var invalidTag1 = new BugTrackerTag("tag with many characters, is not a valid tag");
            var invalidTag2 = new BugTrackerTag("another invalid tag to test validate tags");
            var listTags = new List<BugTrackerTag>();
            listTags.Add(invalidTag1);
            listTags.Add(invalidTag2);

            var bugTrackerTest = new Entity.BugTracker(null, Entity.BugTrackerStatus.ERROR, "test description", DateTime.Today, listTags, null, null);

            Assert.IsFalse(bugTrackerTest.ValidateTags());
        }

        [TestMethod]
        public void ValidateTagWithAValidaTagAndAInvalidTag()
        {
            var validTag = new BugTrackerTag("error tag");
            var invalidTag = new BugTrackerTag("this tag will return false in ValidateTag because has many characters");
            var listTags = new List<BugTrackerTag>();
            listTags.Add(validTag);
            listTags.Add(invalidTag);

            var bugTrackerTest = new Entity.BugTracker(null, Entity.BugTrackerStatus.ERROR, "test description", DateTime.Today, listTags, null, null);

            Assert.IsFalse(bugTrackerTest.ValidateTags());
        }


        [TestMethod]
        public void BugTrackerWithSpecialTagAndANormalTag()
        {
            var appWithSpecialTag = new Application(1, "app test", "app to test", "www.test", true, "default image", "special error", 0, new User());

            var specialTag = new BugTrackerTag("special error");
            var normalTag = new BugTrackerTag("normal info");

            var tagList = new List<BugTrackerTag>();
            tagList.Add(specialTag);
            tagList.Add(normalTag);

            var bugTrackerTest = new Entity.BugTracker(appWithSpecialTag, BugTrackerStatus.ERROR, "test", DateTime.Today, tagList, null, null);

            Assert.AreEqual(specialTag, bugTrackerTest.ContainsSpecialTag());

        }

        [TestMethod]
        public void BugTrackerWithOutSpecialTag()
        {
            var appWithSpecialTag = new Application(1, "app test", "app to test", "www.test", true, "default image", "a never used special tag", 0, new User());

            var specialTag = new BugTrackerTag("normal error");
            var normalTag = new BugTrackerTag("normal info");

            var tagList = new List<BugTrackerTag>();
            tagList.Add(specialTag);
            tagList.Add(normalTag);

            var bugTrackerTest = new Entity.BugTracker(appWithSpecialTag, BugTrackerStatus.ERROR, "test", DateTime.Today, tagList, null, null);

            Assert.IsNull(bugTrackerTest.ContainsSpecialTag());

        }
        
    }
}
